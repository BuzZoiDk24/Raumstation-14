using Robust.Shared.Configuration;

namespace Content.Shared._Raumstation.CCVar;

[CVarDefs]
public sealed class TrainingCVars
{
    /// <summary>
    /// Automatically refill station batteries when entities enter a map and let
    /// them recharge continuously. Read on map initialization, not a live toggle.
    /// </summary>
    public static readonly CVarDef<bool> InfiniteStationPower =
        CVarDef.Create("training.infinite_station_power", false, CVar.SERVERONLY);
}
