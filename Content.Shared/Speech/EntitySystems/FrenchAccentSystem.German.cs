using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Robust.Shared.Random;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class FrenchAccentSystem
{
    private const int GermanRuleCount = 144;
    private (Regex Pattern, string[] Variants)[]? _germanFrenchRules;
    private static readonly Regex GermanWordStartingH = new(@"(?<![\w’'])h\p{L}+(?![\w’'])",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private string AccentuateGerman(string message, IRobustRandom random)
    {
        if (_germanFrenchRules == null)
        {
            var rules = new List<(Regex, string[])>();
            for (var i = 1; i <= GermanRuleCount; i++)
            {
                var key = $"accent-french-german-{i}";
                var alternatives = string.Join("|", Loc.GetString(key + ".source")
                    .Split('|').Select(Regex.Escape));
                var whole = Loc.GetString(key + ".scope") == "whole";
                var pattern = whole
                    ? $@"^(\s*)(?:{alternatives})(?=[.!?]*\s*$)"
                    : $@"(?<![\w’'])(?:{alternatives})(?![\w’'])";
                rules.Add((new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant),
                    Loc.GetString(key).Split('|')));
            }
            _germanFrenchRules = rules.ToArray();
        }

        // Keep a mask of changed text so that French insertions are not accented again.
        var masked = message;
        foreach (var (pattern, variants) in _germanFrenchRules)
        {
            foreach (Match match in pattern.Matches(masked).Cast<Match>().Reverse())
            {
                var replacement = FormatGermanFrenchCase(random.Pick(variants),
                    match.Value.TrimStart(), IsFrenchGermanSentenceStart(message, match.Index));
                if (match.Groups.Count > 1)
                    replacement = string.Concat(match.Groups[1].Value, replacement);
                message = message.Remove(match.Index, match.Length).Insert(match.Index, replacement);
                masked = masked.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('_', replacement.Length));
            }
        }

        foreach (Match match in GermanWordStartingH.Matches(masked).Cast<Match>().Reverse())
        {
            // Keep abbreviations such as HUD and HPLC intact.
            if (match.Value.Length <= 4 && !match.Value.Any(char.IsLower))
                continue;
            var replacement = FormatGermanFrenchCase(string.Concat("’", match.Value.Substring(1)),
                match.Value, IsFrenchGermanSentenceStart(message, match.Index));
            message = message.Remove(match.Index, match.Length).Insert(match.Index, replacement);
        }
        return message;
    }

    private static string FormatGermanFrenchCase(string replacement, string source, bool sentenceStart)
    {
        if (source.Any(char.IsLetter) && !source.Any(char.IsLower))
            return replacement.ToUpperInvariant();
        if (!sentenceStart && (source.Length == 0 || !char.IsUpper(source[0])))
            return replacement;

        // Explicit string operations avoid compiler-generated span constructors.
        for (var i = 0; i < replacement.Length; i++)
        {
            if (!char.IsLetter(replacement[i]))
                continue;
            return string.Concat(replacement.Substring(0, i),
                replacement.Substring(i, 1).ToUpperInvariant(), replacement.Substring(i + 1));
        }
        return replacement;
    }

    private static bool IsFrenchGermanSentenceStart(string message, int index)
    {
        for (var i = index - 1; i >= 0; i--)
        {
            var c = message[i];
            if (char.IsWhiteSpace(c) || c == '"' || c == '\'' || c == '„' || c == '“'
                || c == '«' || c == '»' || c == '(' || c == ')' || c == '[' || c == ']')
                continue;
            return c == '.' || c == '!' || c == '?';
        }
        return true;
    }
}
