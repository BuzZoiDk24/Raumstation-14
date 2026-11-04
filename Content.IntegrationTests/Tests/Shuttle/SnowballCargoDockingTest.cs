using System;
using System.Linq;
using System.Numerics;
using Content.IntegrationTests.Fixtures;
using Content.Server.Doors.Systems;
using Content.Server.GameTicking;
using Content.Shared.DeviceLinking.Systems;
using Content.Shared.Doors.Components;
using Content.Shared.Maps;
using Content.Shared.Power.EntitySystems;
using Robust.Shared.EntitySerialization;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;

namespace Content.IntegrationTests.Tests.Shuttle;

/// <summary>
/// Exercise the real cargo-side doors and saved device links from Snowball.
/// The separate southern docking corridor must not stand in for the cargo dock.
/// </summary>
public sealed class SnowballCargoDockingTest : GameTest
{
    private const string DoorStatusPort = "DoorStatus";
    private const string DockStatusPort = "DockStatus";

    public override PoolSettings PoolSettings => new() { Dirty = true };

    [Test]
    public async Task CargoInnerDoorsFollowDockingState()
    {
        var pair = Pair;
        var server = pair.Server;
        var entMan = server.EntMan;
        var doors = server.System<DoorSystem>();
        var links = server.System<DeviceLinkSystem>();
        var power = server.System<SharedPowerReceiverSystem>();
        EntityUid stationGrid = default;
        var dockDoors = new EntityUid[2];
        var innerDoors = new EntityUid[2];

        await server.WaitAssertion(() =>
        {
            ProtoId<GameMapPrototype> mapId = "Snowball";
            var map = server.ProtoMan.Index(mapId);
            var options = DeserializationOptions.Default with { InitializeMaps = true };
            stationGrid = server.System<ServerGameTicker>().LoadGameMap(map, out _, options).Single();

            for (var i = 0; i < 2; i++)
            {
                var y = 87.5f + 2 * i;
                dockDoors[i] = FindDoor("AirlockExternalGlassShuttleCargo", new Vector2(99.5f, y));
                innerDoors[i] = FindDoor("AirlockExternalGlass", new Vector2(94.5f, y));

                // Isolate the interlock from station power supply startup.
                power.SetNeedsPower(dockDoors[i], false);
                power.SetNeedsPower(innerDoors[i], false);
            }
        });

        await pair.RunSeconds(2);

        for (var i = 0; i < 2; i++)
        {
            var dock = dockDoors[i];
            var inner = innerDoors[i];
            await server.WaitAssertion(() => Assert.That(power.IsPowered(inner), Is.True));

            // Drive the same source ports as DoorSignalControlSystem and
            // DockingSignalControlSystem. Saved links, wireless delivery,
            // logic gates and the inner door's bolt handling all run normally.
            foreach (var (outerOpen, docked, bolted) in new[]
                     {
                         (false, false, false),
                         (true, false, true),
                         (true, true, false),
                         (true, false, true),
                         (false, true, true),
                         (true, true, false),
                     })
            {
                await server.WaitPost(() =>
                {
                    links.SendSignal(dock, DoorStatusPort, outerOpen);
                    links.SendSignal(dock, DockStatusPort, docked);
                });

                await pair.RunTicksSync(10);

                await server.WaitAssertion(() =>
                {
                    Assert.That(doors.IsBolted(inner), Is.EqualTo(bolted),
                        $"Cargo door at y={87.5f + 2 * i}: outerOpen={outerOpen}, docked={docked}");
                    Assert.That(doors.CanOpen(inner), Is.EqualTo(!bolted),
                        "The cargo-side inner door must be usable exactly when the interlock permits it.");
                });
            }

            await server.WaitAssertion(() => Assert.That(doors.TryOpen(inner), Is.True,
                "The cargo-side inner door must actually start opening after docking."));
        }

        EntityUid FindDoor(string prototype, Vector2 position)
        {
            var query = entMan.EntityQueryEnumerator<DoorComponent, TransformComponent, MetaDataComponent>();
            while (query.MoveNext(out var uid, out _, out var transform, out var metadata))
            {
                if (transform.GridUid == stationGrid &&
                    transform.LocalPosition == position &&
                    metadata.EntityPrototype?.ID == prototype)
                    return uid;
            }

            throw new InvalidOperationException($"Snowball cargo door {prototype} at {position} not found.");
        }
    }
}
