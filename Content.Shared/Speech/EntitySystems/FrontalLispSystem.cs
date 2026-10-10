using System.Text.RegularExpressions;
using Content.Shared.Speech.Components;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class FrontalLispSystem : RelayAccentSystem<FrontalLispComponent>
{
    // @formatter:off
    private static readonly Regex RegexUpperTh = new("[T]+[Ss]+|[S]+[Cc]+(?=[IiEeYy]+)|[C]+(?=[IiEeYy]+)|[P][Ss]+|([S]+[Tt]+|[T]+)(?=[Ii]+[Oo]+[Uu]*[Nn]*)|[C]+[Hh]+(?=[Ii]*[Ee]*)|[Z]+|[S]+|[X]+(?=[Ee]+)");
    private static readonly Regex RegexLowerTh = new("[t]+[s]+|[s]+[c]+(?=[iey]+)|[c]+(?=[iey]+)|[p][s]+|([s]+[t]+|[t]+)(?=[i]+[o]+[u]*[n]*)|[c]+[h]+(?=[i]*[e]*)|[z]+|[s]+|[x]+(?=[e]+)");
    private static readonly Regex RegexUpperEcks = new("[E]+[Xx]+[Cc]*|[X]+");
    private static readonly Regex RegexLowerEcks = new("[e]+[x]+[c]*|[x]+");
    // @formatter:on

    // Preserve German sch and chs; replace complete runs without splitting them.
    private static readonly Regex GermanLowerS = new(@"(?<![sSßẞ])(?<![cC][hH])[sß]+(?![sSßẞ]|[cC][hH])");
    private static readonly Regex GermanUpperS = new(@"(?<![sSßẞ])(?<![cC][hH])[Sẞ]+(?![sSßẞ]|[cC][hH])");

    public override string Accentuate(string message, Entity<FrontalLispComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            var allUpper = true;
            foreach (var character in message)
            {
                if (!char.IsLower(character))
                    continue;

                allUpper = false;
                break;
            }

            message = GermanLowerS.Replace(message, "th");
            return GermanUpperS.Replace(message, allUpper ? "TH" : "Th");
        }

        // handles ts, sc(i|e|y), c(i|e|y), ps, st(io(u|n)), ch(i|e), z, s
        message = RegexUpperTh.Replace(message, "TH");
        message = RegexLowerTh.Replace(message, "th");
        // handles ex(c), x
        message = RegexUpperEcks.Replace(message, "EKTH");
        message = RegexLowerEcks.Replace(message, "ekth");
        return message;
    }
}
