using System.Collections.Frozen;
using System.Text.RegularExpressions;
using Content.Shared.Random.Helpers;
using Content.Shared.Speech.Components;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class OwOAccentSystem : RelayAccentSystem<OwOAccentComponent>
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private IRobustRandom _random = default!;

    private static readonly IReadOnlyList<string> Faces =
    [
        " (•`ω´•)", " ;;w;;", " owo", " UwU", " >w<", " ^w^",
    ];

    private static readonly FrozenDictionary<string, string> SpecialWords =
        new Dictionary<string, string>
        {
            { "you", "wu" },
        }.ToFrozenDictionary();

    private static readonly Regex GermanDu = new(@"\bdu\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    private static readonly Regex GermanDich = new(@"\bdich\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
    private static readonly Regex GermanDir = new(@"\bdir\b", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    public override string Accentuate(string message, Entity<OwOAccentComponent>? ent = null)
    {
        var random = ent.HasValue
            ? SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value))
            : _random;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            message = GermanDu.Replace(message, match => FormatGermanPronoun(match.Value, "dwu"));
            message = GermanDich.Replace(message, match => FormatGermanPronoun(match.Value, "dwich"));
            message = GermanDir.Replace(message, match => FormatGermanPronoun(match.Value, "dwiw"));
        }
        else
        {
            foreach (var (word, repl) in SpecialWords)
            {
                message = message.Replace(word, repl);
            }
        }

        return message.Replace("!", random.Pick(Faces))
            .Replace("r", "w")
            .Replace("R", "W")
            .Replace("l", "w")
            .Replace("L", "W");
    }

    private static string FormatGermanPronoun(string original, string replacement)
    {
        if (original == original.ToUpperInvariant())
            return replacement.ToUpperInvariant();

        if (char.IsUpper(original[0]))
            return string.Concat(replacement.Substring(0, 1).ToUpperInvariant(), replacement.Substring(1));

        return replacement;
    }
}
