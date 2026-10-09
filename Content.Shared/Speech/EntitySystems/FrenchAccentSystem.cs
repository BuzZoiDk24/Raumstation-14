using System.Text.RegularExpressions;
using Content.Shared.Speech.Components;
using Content.Shared.Random.Helpers;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

/// <summary>
/// System that gives the speaker a faux-French accent.
/// </summary>
public sealed partial class FrenchAccentSystem : RelayAccentSystem<FrenchAccentComponent>
{
    [Dependency] private ReplacementAccentSystem _replacement = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private IGameTiming _timing = default!;

    private static readonly Regex RegexTh = new("th", RegexOptions.IgnoreCase);
    private static readonly Regex RegexStartH = new(@"(?<!\w)h", RegexOptions.IgnoreCase);
    private static readonly Regex RegexSpacePunctuation = new(@"(?<=\w\w)[!?;:](?!\w)", RegexOptions.IgnoreCase);

    public override string Accentuate(string message, Entity<FrenchAccentComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            var random = ent.HasValue
                ? SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value))
                : _random;
            return AccentuateGerman(message, random);
        }

        var msg = message;

        msg = _replacement.ApplyReplacements(msg, "french", ent?.Owner);

        // replaces h with ' at the start of words.
        msg = RegexStartH.Replace(msg, "'");

        // spaces out ! ? : and ;.
        msg = RegexSpacePunctuation.Replace(msg, " $&");

        // replaces th with 'z or 's depending on the case
        foreach (Match match in RegexTh.Matches(msg))
        {
            var uppercase = msg.Substring(match.Index, 2).Contains("TH");
            var z = uppercase ? "Z" : "z";
            var s = uppercase ? "S" : "s";
            var idxLetter = match.Index + 2;

            // If th is alone, just do 'z
            if (msg.Length <= idxLetter)
            {
                msg = msg.Substring(0, match.Index) + "'" + z;
            }
            else
            {
                var c = "aeiouy".Contains(msg.Substring(idxLetter, 1).ToLower()) ? z : s;
                msg = msg.Substring(0, match.Index) + "'" + c + msg.Substring(idxLetter);
            }
        }

        return msg;
    }
}
