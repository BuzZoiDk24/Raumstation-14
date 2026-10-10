using Content.Shared.Random.Helpers;
using Content.Shared.Speech.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class AustrianAccentSystem : RelayAccentSystem<AustrianAccentComponent>
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private IRobustRandom _random = default!;

    public override string Accentuate(string message, Entity<AustrianAccentComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message) || Loc.DefaultCulture?.TwoLetterISOLanguageName != "de")
            return message;

        var random = ent.HasValue
            ? SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value))
            : _random;
        return AccentuateAustrianGerman(message, random);
    }
}
