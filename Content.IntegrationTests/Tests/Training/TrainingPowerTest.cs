#nullable enable

using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Content.Server.Power.EntitySystems;
using Content.Shared._Raumstation.CCVar;
using Content.Shared.Power.Components;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Training;

[TestFixture]
public sealed class TrainingPowerTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          id: TrainingPowerTestBattery
          name: training battery
          description: A battery for the training-power test.
          components:
          - type: Battery
            maxCharge: 100
            startingCharge: 25
          - type: StationInfiniteBatteryTarget
        """;

    [TestCase(false)]
    [TestCase(true)]
    public async Task LiveTogglePreservesNormalRecharge(bool hasSelfRecharge)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var configuration = Pair.Server.ResolveDependency<IConfigurationManager>();
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var batteries = entities.System<BatterySystem>();
            var previous = configuration.GetCVar(TrainingCVars.InfiniteStationPower);
            EntityUid? existing = null;
            EntityUid? added = null;
            try
            {
                configuration.SetCVar(TrainingCVars.InfiniteStationPower, false);
                existing = SpawnInitialized(entities);
                var battery = entities.GetComponent<BatteryComponent>(existing.Value);
                BatterySelfRechargerComponent? recharger = null;
                var normalRate = 0f;
                if (hasSelfRecharge)
                {
                    recharger = entities.AddComponent<BatterySelfRechargerComponent>(existing.Value);
                    recharger.AutoRechargeRate = 7f;
                    recharger.AutoRechargePauseTime = TimeSpan.FromSeconds(3);
                    recharger.NextAutoRecharge = null;
                    normalRate = 7f;
                }
                Assert.That(batteries.RefreshChargeRate((existing.Value, battery)), Is.EqualTo(normalRate));

                configuration.SetCVar(TrainingCVars.InfiniteStationPower, true);
                Assert.That(batteries.GetCharge((existing.Value, battery)), Is.EqualTo(100f));
                Assert.That(batteries.RefreshChargeRate((existing.Value, battery)), Is.EqualTo(normalRate + 100f));
                // Adding another battery while enabled must receive training power too.
                added = SpawnInitialized(entities);
                var addedBattery = entities.GetComponent<BatteryComponent>(added.Value);
                Assert.That(batteries.GetCharge((added.Value, addedBattery)), Is.EqualTo(100f));
                Assert.That(batteries.RefreshChargeRate((added.Value, addedBattery)), Is.EqualTo(100f));

                batteries.SetCharge((existing.Value, battery), 40f);
                configuration.SetCVar(TrainingCVars.InfiniteStationPower, false);
                Assert.That(batteries.GetCharge((existing.Value, battery)), Is.EqualTo(40f));
                Assert.That(batteries.RefreshChargeRate((existing.Value, battery)), Is.EqualTo(normalRate));
                Assert.That(batteries.RefreshChargeRate((added.Value, addedBattery)), Is.Zero);
                Assert.That(entities.HasComponent<BatterySelfRechargerComponent>(existing.Value), Is.EqualTo(hasSelfRecharge));
                if (recharger != null)
                {
                    Assert.That(recharger.AutoRechargeRate, Is.EqualTo(7f));
                    Assert.That(recharger.AutoRechargePauseTime, Is.EqualTo(TimeSpan.FromSeconds(3)));
                    Assert.That(recharger.NextAutoRecharge, Is.Null);
                }

                configuration.SetCVar(TrainingCVars.InfiniteStationPower, true);
                Assert.That(batteries.RefreshChargeRate((existing.Value, battery)), Is.EqualTo(normalRate + 100f));
                configuration.SetCVar(TrainingCVars.InfiniteStationPower, false);
                Assert.That(batteries.RefreshChargeRate((existing.Value, battery)), Is.EqualTo(normalRate));
            }
            finally
            {
                if (existing != null)
                    entities.DeleteEntity(existing.Value);
                if (added != null)
                    entities.DeleteEntity(added.Value);
                configuration.SetCVar(TrainingCVars.InfiniteStationPower, previous);
            }
        });
    }

    private static EntityUid SpawnInitialized(IEntityManager entities)
    {
        var uid = entities.SpawnEntity("TrainingPowerTestBattery", MapCoordinates.Nullspace);
        var metadata = entities.GetComponent<MetaDataComponent>(uid);
        if (metadata.EntityLifeStage < EntityLifeStage.MapInitialized)
            entities.RunMapInit(uid, metadata);
        return uid;
    }
}
