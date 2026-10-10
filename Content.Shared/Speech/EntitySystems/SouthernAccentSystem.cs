using System.Text.RegularExpressions;
using Content.Shared.Speech.Components;
using Content.Shared.Random.Helpers;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class SouthernAccentSystem : RelayAccentSystem<SouthernAccentComponent>
{
    private static readonly Regex RegexLowerIng = new(@"ing\b");
    private static readonly Regex RegexUpperIng = new(@"ING\b");
    private static readonly Regex RegexLowerAnd = new(@"\band\b");
    private static readonly Regex RegexUpperAnd = new(@"\bAND\b");
    private static readonly Regex RegexLowerDve = new(@"d've\b");
    private static readonly Regex RegexUpperDve = new(@"D'VE\b");

    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private ReplacementAccentSystem _replacement = default!;

    public override string Accentuate(string message, Entity<SouthernAccentComponent>? ent = null)
    {
        if (string.IsNullOrWhiteSpace(message))
            return message;

        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
        {
            var random = ent.HasValue
                ? SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value))
                : _random;
            return AccentuateBavarianGerman(message, random);
        }

        message = _replacement.ApplyReplacements(message, "southern", ent?.Owner);

        //They shoulda started runnin' an' hidin' from me!
        message = RegexLowerIng.Replace(message, "in'");
        message = RegexUpperIng.Replace(message, "IN'");
        message = RegexLowerAnd.Replace(message, "an'");
        message = RegexUpperAnd.Replace(message, "AN'");
        message = RegexLowerDve.Replace(message, "da");
        message = RegexUpperDve.Replace(message, "DA");
        return message;
    }
};
