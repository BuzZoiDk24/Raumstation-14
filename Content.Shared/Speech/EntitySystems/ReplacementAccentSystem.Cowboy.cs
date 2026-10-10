using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Robust.Shared.Random;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class ReplacementAccentSystem
{
    private const int GermanCowboyRuleCount = 159;
    private (Regex Pattern, string[] Variants)[]? _germanCowboyRules;
    private string AccentuateCowboyGerman(string message, IRobustRandom random)
    {
        if (_germanCowboyRules == null)
        {
            var rules = new List<(Regex Pattern, string[] Variants, bool Whole, int SourceLength)>();
            for (var i = 1; i <= GermanCowboyRuleCount; i++)
            {
                var key = $"accent-cowboy-german-{i}";
                var scope = Loc.GetString(key + ".scope");
                if (scope == "disabled")
                    continue;
                var sources = Loc.GetString(key + ".source").Split('|');
                var alternatives = string.Join("|", sources.Select(Regex.Escape));
                var whole = scope == "whole";
                var pattern = whole
                    ? $@"^(\s*)(?:{alternatives})(?=[.!?]*\s*$)"
                    : scope == "start"
                        ? $@"^(\s*)(?:{alternatives})(?![\w’'])"
                        : $@"(?<![\w’'])(?:{alternatives})(?![\w’'])";
                rules.Add((new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant),
                    Loc.GetString(key).Split('|'), whole, sources.Max(source => source.Length)));
            }
            // Longer complete expressions take priority.
            _germanCowboyRules = rules.OrderByDescending(rule => rule.Whole)
                .ThenByDescending(rule => rule.SourceLength)
                .Select(rule => (rule.Pattern, rule.Variants)).ToArray();
        }

        // Keep a mask of changed text so that German insertions are not accented again.
        var masked = message;
        foreach (var (pattern, variants) in _germanCowboyRules)
        {
            foreach (Match match in pattern.Matches(masked).Cast<Match>().Reverse())
            {
                var replacement = FormatCowboyGermanCase(random.Pick(variants),
                    match.Value.TrimStart(), IsCowboyGermanSentenceStart(message, match.Index));
                if (match.Groups.Count > 1)
                    replacement = string.Concat(match.Groups[1].Value, replacement);
                message = message.Remove(match.Index, match.Length).Insert(match.Index, replacement);
                masked = masked.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('_', replacement.Length));
            }
        }

        return message;
    }

    private static string FormatCowboyGermanCase(string replacement, string source, bool sentenceStart)
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

    private static bool IsCowboyGermanSentenceStart(string message, int index)
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
