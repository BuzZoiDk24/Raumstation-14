using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Content.Shared.Speech.Prototypes;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Prototypes;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanLiarAccentTest : GameTest
{
    private static readonly ProtoId<ReplacementAccentPrototype> LiarAccent = "liar";

    [TestCase("Ja!", "Nein!")]
    [TestCase("Nein!", "Ja!")]
    [TestCase("jup", "Nein")]
    [TestCase("Nö.", "Ja.")]
    [TestCase("JA!", "NEIN!")]
    [TestCase("Das ist ja praktisch.", "Das ist ja praktisch.")]
    [TestCase("Ja, ich komme später.", "Ja, ich komme später.")]
    [TestCase("Das ist wahr.", "Das ist falsch.")]
    [TestCase("Das ist falsch.", "Das ist wahr.")]
    [TestCase("Sie ist schuldig.", "Sie ist unschuldig.")]
    [TestCase("Er ist unschuldig.", "Er ist schuldig.")]
    [TestCase("Der schuldige Kapitän.", "Der unschuldige Kapitän.")]
    [TestCase("Die unschuldige Kapitänin.", "Die schuldige Kapitänin.")]
    [TestCase("Er ist tot.", "Er ist lebendig.")]
    [TestCase("Sie ist lebendig.", "Sie ist tot.")]
    [TestCase("Die tote Maus.", "Die lebendige Maus.")]
    [TestCase("Ein totes Tier.", "Ein lebendiges Tier.")]
    [TestCase("Mit einem lebendigen Tier.", "Mit einem toten Tier.")]
    [TestCase("Mir geht es gut.", "Mir geht es schlecht.")]
    [TestCase("Ein guter Plan.", "Ein schlechter Plan.")]
    [TestCase("Die schlechten Nachrichten.", "Die guten Nachrichten.")]
    [TestCase("Ich habe nichts gesehen.", "Ich habe etwas gesehen.")]
    [TestCase("Ich habe etwas gesehen.", "Ich habe nichts gesehen.")]
    [TestCase("Niemand war hier.", "Jemand war hier.")]
    [TestCase("Jemand war hier.", "Niemand war hier.")]
    [TestCase("Ich habe jemanden gesehen.", "Ich habe niemanden gesehen.")]
    [TestCase("Ich helfe niemandem.", "Ich helfe jemandem.")]
    [TestCase("Niemandes Aufgabe.", "Jemandes Aufgabe.")]
    [TestCase("Ich kann helfen.", "Ich kann nicht helfen.")]
    [TestCase("Ich kann nicht helfen.", "Ich kann helfen.")]
    [TestCase("Ich werde kommen!", "Ich werde nicht kommen!")]
    [TestCase("Ich sollte nicht gehen.", "Ich sollte gehen.")]
    [TestCase("Ich war dort.", "Ich war nicht dort.")]
    [TestCase("Ich bin nicht hier.", "Ich bin hier.")]
    [TestCase("Sie ist hier.", "Sie ist nicht hier.")]
    [TestCase("Es ist nicht hier.", "Es ist hier.")]
    [TestCase("Wir sind hier.", "Wir sind nicht hier.")]
    [TestCase("Ich liebe dich.", "Ich hasse dich.")]
    [TestCase("Ich hasse euch.", "Ich liebe euch.")]
    [TestCase("Meine liebe Kollegin.", "Meine liebe Kollegin.")]
    [TestCase("Ich kann helfen, wenn du willst.", "Ich kann helfen, wenn du willst.")]
    [TestCase("Er ist hier im Labor.", "Er ist hier im Labor.")]
    [TestCase("NICHTS IST WAHR!", "NICHTS IST FALSCH!")]
    [TestCase("Ich bin etwas müde.", "Ich bin etwas müde.")]
    [TestCase("Nichts als Probleme.", "Nichts als Probleme.")]
    [TestCase("Die Gummistiefel sind rutschfest.", "Die Gummistiefel sind rutschfest.")]
    [TestCase("Nichtsahnend", "Nichtsahnend")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    public async Task GermanOppositesRemainGrammatical(string input, string expected)
    {
        await WithCulture("de-DE", 1f, system =>
            Assert.That(system.ApplyReplacements(input, "liar"), Is.EqualTo(expected)));
    }

    [Test]
    public async Task ZeroReplacementChanceLeavesSpeechUnchanged()
    {
        await WithCulture("de-DE", 0f, system =>
            Assert.That(system.ApplyReplacements("Sie ist schuldig.", "liar"), Is.EqualTo("Sie ist schuldig.")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheEnglishRules()
    {
        await WithCulture("de-DE", 1f, system =>
            Assert.That(system.ApplyReplacements("Ja!", "liar"), Is.EqualTo("Nein!")));
        await WithCulture("en-US", 1f, system =>
            Assert.That(system.ApplyReplacements("Yes!", "liar"), Is.EqualTo("No!")));
        await WithCulture("de-DE", 1f, system =>
            Assert.That(system.ApplyReplacements("Ja!", "liar"), Is.EqualTo("Nein!")));
    }

    private async Task WithCulture(string culture, float chance, Action<ReplacementAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            var prototypes = Pair.Server.ResolveDependency<IPrototypeManager>();
            var prototype = prototypes.Index(LiarAccent);
            var previousChance = prototype.ReplacementChance;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                prototype.ReplacementChance = chance;
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<ReplacementAccentSystem>());
            }
            finally
            {
                prototype.ReplacementChance = previousChance;
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }
}
