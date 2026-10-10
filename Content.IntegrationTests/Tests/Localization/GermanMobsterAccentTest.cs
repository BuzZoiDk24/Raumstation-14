using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanMobsterAccentTest : GameTest
{
    [TestCase("Hallo!", "Hey!|Na!")]
    [TestCase("Alles klar.", "Geht klar.")]
    [TestCase("Kein Problem.", "Kein Ding.")]
    [TestCase("Verstanden?", "Klar soweit?|Verstanden?")]
    [TestCase("Verschwinde!", "Mach ’ne Fliege!")]
    [TestCase("Halt den Mund!", "Halt die Klappe!")]
    [TestCase("Gut gemacht!", "Sauber!")]
    [TestCase("Keine Ahnung.", "Keinen Schimmer.")]
    [TestCase("Mein Chef kommt.", "Mein Boss kommt.")]
    [TestCase("Die Chefs kommen.", "Die Chefs kommen.")]
    [TestCase("Das Büro des Chefs.", "Das Büro des Chefs.")]
    [TestCase("Ich habe das Geld.", "Ich habe die Kohle.")]
    [TestCase("Mein Geld ist weg.", "Meine Kohle ist weg.")]
    [TestCase("Mit meinem Geld.", "Mit meiner Kohle.")]
    [TestCase("Wegen des Geldes.", "Wegen der Kohle.")]
    [TestCase("Ich brauche etwas Geld.", "Ich brauche etwas Kohle.")]
    [TestCase("Ich habe kein Geld.", "Ich habe kein Geld.")]
    [TestCase("Geldautomat und Bargeld.", "Geldautomat und Bargeld.")]
    [TestCase("Geld", "Kohle")]
    [TestCase("ICH HABE DAS GELD!", "ICH HABE DIE KOHLE!")]
    [TestCase("Er wurde umgebracht.", "Er wurde kaltgemacht.")]
    [TestCase("Ich will ihn nicht umbringen.", "Ich will ihn nicht kaltmachen.")]
    [TestCase("Um ihn umzubringen.", "Um ihn kaltzumachen.")]
    [TestCase("Das ist kein Problem mit dem Generator.", "Das ist kein Problem mit dem Generator.")]
    [TestCase("Das wächst natürlich.", "Das wächst natürlich.")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Mein Freund und meine Freundin.", "Mein Freund und meine Freundin.")]
    [TestCase("Sigma Zeta APE H2O Station Scanner.", "Sigma Zeta APE H2O Station Scanner.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    public async Task GermanSpeechRemainsReadable(string input, string expected)
    {
        await WithCulture("de-DE", system =>
        {
            var variants = expected.Split('|');
            for (var i = 0; i < 8; i++)
                AssertGermanOutput(system.Accentuate(input), variants);
        });
    }

    [Test]
    public async Task EnglishAccentStillUsesItsOriginalRules()
    {
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("king"), Does.Match(@"^(?:King|Nyehh, king)(?:, yeah!|, boss says!)?$")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            AssertGermanOutput(system.Accentuate("Kein Problem."), new[] { "Kein Ding." }));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("king"), Does.Match(@"^(?:King|Nyehh, king)(?:, yeah!|, boss says!)?$")));
        await WithCulture("de-DE", system =>
            AssertGermanOutput(system.Accentuate("Kein Problem."), new[] { "Kein Ding." }));
    }

    private static void AssertGermanOutput(string actual, string[] variants)
    {
        if (variants.All(string.IsNullOrWhiteSpace))
        {
            Assert.That(variants, Does.Contain(actual));
            return;
        }
        foreach (var variant in variants)
        {
            var plain = variant.TrimEnd('.', '!', '?');
            var pattern = @"^(?:Njehh! |NJEHH! )?(?:" + Regex.Escape(variant)
                + "|" + Regex.Escape(plain)
                + @"(?:, jawohl!|, sagt der Boss!|, JAWOHL!|, SAGT DER BOSS!))$";
            if (new Regex(pattern, RegexOptions.CultureInvariant).IsMatch(actual))
                return;
        }
        Assert.Fail($"Unexpected gangster output: {actual}");
    }

    private async Task WithCulture(string culture, Action<MobsterAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<MobsterAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
