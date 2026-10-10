using System.Text.RegularExpressions;
using System.Text;
using Content.Shared.Speech.Components;
using Content.Shared.Random.Helpers;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class SpanishAccentSystem : RelayAccentSystem<SpanishAccentComponent>
{
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private IGameTiming _timing = default!;

    // for words in all lowercase (multiple "s" are allowed)
    private static readonly Regex RegexLower = new(@"(?<!\w)(s+h*[bcdfgjklmnpqrtvwxz])");
    // For Capitalized Words (Station -> Estation; Capital "S" Is Replaced Directly, Multiple "S" Are Allowed)
    private static readonly Regex RegexCaps = new(@"(?<!\w)S(s*h*[bcdfgjklmnpqrtvwxz])");
    // FOR WORDS IN ALL UPPERCASE (ONLY ONE "S" IS ALLOWED, ASSUMING IT'S NOT AN ACRONYM)
    private static readonly Regex RegexUpper = new(@"(?<!\w)(SH*[BCDFGJKLMNPQRTVWXZ])");

    public override string Accentuate(string message, Entity<SpanishAccentComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            var random = ent.HasValue
                ? SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value))
                : _random;
            return ReplacePunctuation(AccentuateGerman(message, random));
        }

        // Insert E before every S that is followed by a consonant that makes a distinct sound
        // (H is excluded because [sh] is a single sound)
        message = InsertS(message);
        // If a sentence ends with ?, insert a reverse ? at the beginning of the sentence
        message = ReplacePunctuation(message);
        return message;
    }

    private string InsertS(string message)
    {
        // Replace every new Word that starts with s/S and a consonant
        message = RegexLower.Replace(message, "e$1");
        message = RegexCaps.Replace(message, "Es$1");
        message = RegexUpper.Replace(message, "E$1");
        return message;
    }

    private string ReplacePunctuation(string message)
    {
        var sentences = AccentSystem.SentenceRegex.Split(message);
        var msg = new StringBuilder();
        foreach (var s in sentences)
        {
            var toInsert = new StringBuilder();
            for (var i = s.Length - 1; i >= 0 && "?!‽".Contains(s[i]); i--)
            {
                toInsert.Append(s[i] switch
                {
                    '?' => '¿',
                    '!' => '¡',
                    '‽' => '⸘',
                    _ => ' '
                });
            }

            if (toInsert.Length == 0)
                msg.Append(s);
            else
            {
                var start = s.Length - s.TrimStart().Length;
                // Whole-message translations can already contain Spanish punctuation.
                if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de"
                    && s.Substring(start).StartsWith(toInsert.ToString(), StringComparison.Ordinal))
                    msg.Append(s);
                else
                    msg.Append(s.Insert(start, toInsert.ToString()));
            }
        }

        return msg.ToString();
    }
}
