using Robust.Shared.Configuration;

namespace Content.Shared._Raumstation.CCVar;

[CVarDefs]
public sealed class TrainingCVars
{
    /// <summary>
    /// Refill station batteries and add continuous training recharge while enabled.
    /// Can be switched during a round; disabling preserves stored charge and
    /// removes only the additional training recharge.
    /// </summary>
    public static readonly CVarDef<bool> InfiniteStationPower =
        CVarDef.Create("training.infinite_station_power", false, CVar.SERVERONLY);
}
