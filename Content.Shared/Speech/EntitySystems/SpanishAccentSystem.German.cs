using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Robust.Shared.Random;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class SpanishAccentSystem
{
    private const int GermanRuleCount = 54;
    private (Regex Pattern, string[] Variants)[]? _germanSpanishRules;
    private static readonly Regex GermanWordStartingS = new(@"(?<![\w’'])s(?!ch|h)[cptkbfglmnrvwz]\p{L}*(?![\w’'])",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private string AccentuateGerman(string message, IRobustRandom random)
    {
        if (_germanSpanishRules == null)
        {
            var rules = new List<(Regex Pattern, string[] Variants, bool Whole, int SourceLength)>();
            for (var i = 1; i <= GermanRuleCount; i++)
            {
                var key = $"accent-spanish-german-{i}";
                var scope = Loc.GetString(key + ".scope");
                if (scope == "disabled")
                    continue;
                var sources = Loc.GetString(key + ".source").Split('|');
                var alternatives = string.Join("|", sources.Select(Regex.Escape));
                var whole = scope == "whole";
                var pattern = whole
                    ? $@"^(\s*)(?:{alternatives})(?=[.!?]*\s*$)"
                    : $@"(?<![\w’'])(?:{alternatives})(?![\w’'])";
                rules.Add((new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant),
                    Loc.GetString(key).Split('|'), whole, sources.Max(source => source.Length)));
            }
            // Whole expressions first; then prefer "Bitte?" over "Bitte".
            _germanSpanishRules = rules.OrderByDescending(rule => rule.Whole)
                .ThenByDescending(rule => rule.SourceLength)
                .Select(rule => (rule.Pattern, rule.Variants)).ToArray();
        }

        // Keep a mask of changed text so that Spanish insertions are not accented again.
        var masked = message;
        foreach (var (pattern, variants) in _germanSpanishRules)
        {
            foreach (Match match in pattern.Matches(masked).Cast<Match>().Reverse())
            {
                var replacement = FormatGermanSpanishCase(random.Pick(variants),
                    match.Value.TrimStart(), IsSpanishGermanSentenceStart(message, match.Index));
                if (match.Groups.Count > 1)
                    replacement = string.Concat(match.Groups[1].Value, replacement);
                message = message.Remove(match.Index, match.Length).Insert(match.Index, replacement);
                masked = masked.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('_', replacement.Length));
            }
        }

        foreach (Match match in GermanWordStartingS.Matches(masked).Cast<Match>().Reverse())
        {
            // Preserve all-uppercase identifiers such as STOP, SMES and station codes.
            if (!match.Value.Any(char.IsLower))
                continue;
            var word = string.Concat("e", match.Value.Substring(0, 1).ToLowerInvariant(),
                match.Value.Substring(1));
            var replacement = FormatGermanSpanishCase(word,
                match.Value, IsSpanishGermanSentenceStart(message, match.Index));
            message = message.Remove(match.Index, match.Length).Insert(match.Index, replacement);
        }
        return message;
    }

    private static string FormatGermanSpanishCase(string replacement, string source, bool sentenceStart)
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

    private static bool IsSpanishGermanSentenceStart(string message, int index)
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
