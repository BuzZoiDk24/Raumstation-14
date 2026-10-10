using System.Text;
using Content.Shared.Speech.Components;
using Content.Shared.Random.Helpers;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class RussianAccentSystem : RelayAccentSystem<RussianAccentComponent>
{
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private ReplacementAccentSystem _replacement = default!;

    public override string Accentuate(string message, Entity<RussianAccentComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            var random = ent.HasValue
                ? SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value))
                : _random;
            return AccentuateRussianGerman(message, random);
        }

        var accentedMessage = new StringBuilder(_replacement.ApplyReplacements(message, "russian", ent?.Owner));

        for (var i = 0; i < accentedMessage.Length; i++)
        {
            var c = accentedMessage[i];

            accentedMessage[i] = c switch
            {
                'A' => 'Д',
                'b' => 'в',
                'N' => 'И',
                'n' => 'и',
                'K' => 'К',
                'k' => 'к',
                'm' => 'м',
                'h' => 'н',
                't' => 'т',
                'R' => 'Я',
                'r' => 'я',
                'Y' => 'У',
                'W' => 'Ш',
                'w' => 'ш',
                _ => accentedMessage[i]
            };
        }

        return accentedMessage.ToString();
    }
}
