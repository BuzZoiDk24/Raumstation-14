using System.Text.RegularExpressions;

namespace Content.Shared.Speech.EntitySystems;

/// <summary>
/// Boundaries for German phrase rules. Whole expressions must fill a sentence,
/// while greetings may be followed by a name. Punctuation is left in the message.
/// </summary>
internal static class GermanAccentRules
{
    private static readonly Regex WordCharacters = new(@"\w", RegexOptions.CultureInvariant);

    // Keep sentence separators and quotes visible to rules for adjacent sentences.
    public static string Mask(string replacement) => WordCharacters.Replace(replacement, "_");

    public static string SentenceStartPattern(string alternatives)
    {
        return $@"(?:^|(?<=[.!?]))([\s""„“«»]*)(?:{alternatives})(?![\w’'])";
    }

    public static string SentencePattern(string alternatives)
    {
        // Some localized source expressions already include their question mark.
        return $@"(?:^|(?<=[.!?]))([\s""„“«»]*)(?:{alternatives})(?=[.!?]|[""“»](?:[.!?]|\s|$)|\s*$|(?<=[.!?])(?:\s|[""“»]))";
    }
}
