using System.Text.RegularExpressions;
using Content.Shared.Speech.Components;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class LizardAccentSystem : RelayAccentSystem<LizardAccentComponent>
{
    private static readonly Regex RegexLowerS = new("s+");
    private static readonly Regex RegexUpperS = new("S+");
    private static readonly Regex RegexInternalX = new(@"(\w)x");
    private static readonly Regex RegexLowerEndX = new(@"\bx([\-|r|R]|\b)");
    private static readonly Regex RegexUpperEndX = new(@"\bX([\-|r|R]|\b)");

    // Do not split a run of s/ß, or change the distinct German sch/chs sounds.
    private static readonly Regex GermanLowerS = new(@"(?<![sSßẞ])(?<![cC][hH])[sß]+(?![sSßẞ]|[cC][hH])");
    private static readonly Regex GermanUpperS = new(@"(?<![sSßẞ])(?<![cC][hH])[Sẞ]+(?![sSßẞ]|[cC][hH])");

    public override string Accentuate(string message, Entity<LizardAccentComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            message = GermanLowerS.Replace(message, "sss");
            message = GermanUpperS.Replace(message, "SSS");
            // Keep the original x sound rules; sch and chs are not x spellings.
            message = RegexInternalX.Replace(message, "$1kss");
            message = RegexLowerEndX.Replace(message, "ecks$1");
            return RegexUpperEndX.Replace(message, "ECKS$1");
        }

        // hissss
        message = RegexLowerS.Replace(message, "sss");
        // hiSSS
        message = RegexUpperS.Replace(message, "SSS");
        // ekssit
        message = RegexInternalX.Replace(message, "$1kss");
        // ecks
        message = RegexLowerEndX.Replace(message, "ecks$1");
        // eckS
        message = RegexUpperEndX.Replace(message, "ECKS$1");

        return message;
    }
}
