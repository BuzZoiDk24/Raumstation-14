using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class PirateAccentSystem
{
    private const int GermanRuleCount = 279;
    private (Regex Pattern, string Replacement)[]? _germanRules;

    private string AccentuateGerman(string message)
    {
        if (_germanRules == null)
        {
            var rules = new List<(Regex Pattern, string Replacement, int SourceLength)>();
            for (var i = 1; i <= GermanRuleCount; i++)
            {
                var key = $"accent-pirate-german-{i}";
                var sourceText = Loc.GetString(key + ".source");
                var source = Regex.Escape(sourceText);
                var standalone = Loc.GetString(key + ".standalone") == "true";
                // Apostrophes are word boundaries too, so names and compounds survive.
                var pattern = standalone
                    ? $@"^(\s*){source}(?=[.!?]*\s*$)"
                    : $@"(?<![\w’']){source}(?![\w’'])";
                rules.Add((new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant),
                    Loc.GetString(key), sourceText.Length));
            }
            // Always process full phrases before their individual nouns.
            _germanRules = rules.OrderByDescending(rule => rule.SourceLength)
                .Select(rule => (rule.Pattern, rule.Replacement)).ToArray();
        }

        var masked = message;
        foreach (var (pattern, value) in _germanRules)
        {
            foreach (Match match in pattern.Matches(masked).Cast<Match>().Reverse())
            {
                var replacement = value;
                var word = match.Value.TrimStart();
                if (word.Any(char.IsLetter) && !word.Any(char.IsLower))
                    replacement = replacement.ToUpperInvariant();
                else if (word.Length > 0 &&
                         (char.IsUpper(word[0]) || IsGermanSentenceStart(message, match.Index)))
                {
                    // Keep this conversion separate: the compiler can undo char.ToString()
                    // inside a + expression and emit a sandbox-blocked span constructor.
                    var firstLetter = char.ToUpperInvariant(replacement[0]).ToString();
                    replacement = string.Concat(firstLetter, replacement.Substring(1));
                }

                // A standalone match includes leading whitespace, which must be kept.
                if (match.Groups.Count > 1)
                    replacement = match.Groups[1].Value + replacement;

                message = message.Remove(match.Index, match.Length).Insert(match.Index, replacement);
                masked = masked.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('_', replacement.Length));
            }
        }
        return message;
    }

    private static bool IsGermanSentenceStart(string message, int index)
    {
        // Ignore whitespace and surrounding quotes/brackets before the matched phrase.
        for (var i = index - 1; i >= 0; i--)
        {
            var character = message[i];
            if (char.IsWhiteSpace(character) || character is '"' or '\'' or '„' or '“'
                or '«' or '»' or '(' or ')' or '[' or ']' or '{' or '}')
                continue;

            return character is '.' or '!' or '?';
        }

        return true;
    }
}
