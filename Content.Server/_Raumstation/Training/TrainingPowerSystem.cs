using Content.Server.Power.EntitySystems;
using Content.Shared.Administration.Components;
using Content.Shared.Power.Components;
using Content.Shared._Raumstation.CCVar;
using Robust.Shared.Configuration;

namespace Content.Server._Raumstation.Training;

/// <summary>
/// Applies the existing infinite-battery admin trick to station infrastructure
/// automatically, including infrastructure spawned later in the round.
/// </summary>
public sealed partial class TrainingPowerSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _configuration = default!;
    [Dependency] private BatterySystem _battery = default!;

    public override void Initialize()
    {
        base.Initialize();

        // BatterySystem initializes the starting charge on MapInit. Refill after
        // that so the normal starting charge cannot overwrite the training fill.
        SubscribeLocalEvent<StationInfiniteBatteryTargetComponent, MapInitEvent>(OnMapInit,
            after: new[] { typeof(BatterySystem) });
    }

    private void OnMapInit(Entity<StationInfiniteBatteryTargetComponent> ent, ref MapInitEvent args)
    {
        if (!_configuration.GetCVar(TrainingCVars.InfiniteStationPower) ||
            !TryComp<BatteryComponent>(ent, out var battery))
            return;

        var recharger = EnsureComp<BatterySelfRechargerComponent>(ent);
        recharger.AutoRechargeRate = battery.MaxCharge;
        recharger.AutoRechargePauseTime = TimeSpan.Zero;
        recharger.NextAutoRecharge = null;
        Dirty(ent, recharger);

        _battery.SetCharge((ent.Owner, battery), battery.MaxCharge);
        _battery.RefreshChargeRate((ent.Owner, battery));
    }
}
