using Content.Server.Power.EntitySystems;
using Content.Shared.Administration.Components;
using Content.Shared.Power;
using Content.Shared.Power.Components;
using Content.Shared._Raumstation.CCVar;
using Robust.Shared.Configuration;

namespace Content.Server._Raumstation.Training;

/// <summary>
/// Adds optional training power to station infrastructure without changing its
/// normal self-recharge components. The setting can be changed during a round.
/// </summary>
public sealed partial class TrainingPowerSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _configuration = default!;
    [Dependency] private BatterySystem _battery = default!;

    private bool _enabled;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<StationInfiniteBatteryTargetComponent, RefreshChargeRateEvent>(OnRefreshChargeRate);
        // Refill after BatterySystem applies the normal starting charge.
        SubscribeLocalEvent<StationInfiniteBatteryTargetComponent, MapInitEvent>(OnMapInit,
            after: new[] { typeof(BatterySystem) });
        _configuration.OnValueChanged(TrainingCVars.InfiniteStationPower, OnPowerChanged, true);
    }

    public override void Shutdown()
    {
        _configuration.UnsubValueChanged(TrainingCVars.InfiniteStationPower, OnPowerChanged);
        base.Shutdown();
    }

    private void OnPowerChanged(bool enabled)
    {
        if (_enabled == enabled)
            return;

        _enabled = enabled;
        // Include paused infrastructure; future map-initialized entities are
        // handled by OnMapInit. Uninitialized prototypes must stay untouched.
        var query = AllEntityQuery<StationInfiniteBatteryTargetComponent, BatteryComponent, MetaDataComponent>();
        while (query.MoveNext(out var uid, out _, out var battery, out var metadata))
        {
            if (metadata.EntityLifeStage < EntityLifeStage.MapInitialized ||
                metadata.EntityLifeStage >= EntityLifeStage.Terminating)
                continue;

            ApplyPower((uid, battery));
        }
    }

    private void OnMapInit(Entity<StationInfiniteBatteryTargetComponent> ent, ref MapInitEvent args)
    {
        if (_enabled && TryComp<BatteryComponent>(ent, out var battery))
            ApplyPower((ent.Owner, battery));
    }

    private void ApplyPower(Entity<BatteryComponent> ent)
    {
        // Switching off preserves the current charge. Only the extra recharge
        // rate is removed, so ordinary generation and stored energy still work.
        _battery.RefreshChargeRate(ent.AsNullable());
        if (_enabled)
            _battery.SetCharge(ent.AsNullable(), ent.Comp.MaxCharge);
    }

    private void OnRefreshChargeRate(Entity<StationInfiniteBatteryTargetComponent> ent, ref RefreshChargeRateEvent args)
    {
        if (_enabled)
            args.NewChargeRate += args.MaxCharge;
    }
}
