using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanRussianAccentTest : GameTest
{
    [TestCase("Hallo!", "Privet!")]
    [TestCase("hallo", "Privet")]
    [TestCase("HALLO!", "PRIVET!")]
    [TestCase("Guten Tag!", "Dobry den!")]
    [TestCase("Guten Morgen!", "Dobroje utro!")]
    [TestCase("Guten Abend!", "Dobry wetscher!")]
    [TestCase("Tschüss!", "Poka!")]
    [TestCase("Auf Wiedersehen!", "Do swidanija!")]
    [TestCase("Danke!", "Spasibo!")]
    [TestCase("Vielen Dank.", "Bolschoje spasibo.")]
    [TestCase("Ja!", "Da!")]
    [TestCase("Nein!", "Njet!")]
    [TestCase("Ja, aber ich komme später.", "Ja, aber ich komme später.")]
    [TestCase("Da ist die Maschine.", "Da ist die Maschine.")]
    [TestCase("Gut!", "Choroscho!")]
    [TestCase("Alles klar!", "Choroscho!")]
    [TestCase("Los geht’s!", "Dawai!|Na los, dawai!")]
    [TestCase("Natürlich!", "Da, natürlich!|Da, da!")]
    [TestCase("Das wächst natürlich.", "Das wächst natürlich.")]
    [TestCase("Mein Gott!", "Bosche moi!")]
    [TestCase("Oh nein!", "Och, njet!")]
    [TestCase("Autsch!", "Ai!")]
    [TestCase("Moment mal!", "Moment, moment!")]
    [TestCase("Echt?", "Im Ernst?|Wirklich?")]
    [TestCase("Beeil dich!", "Dawai, schnell!")]
    [TestCase("Achtung!", "Vorsicht, vorsicht!")]
    [TestCase("Aus Achtung vor dem Kapitän.", "Aus Achtung vor dem Kapitän.")]
    [TestCase("Das ist gut.", "Das gut.")]
    [TestCase("Das ist kaputt!", "Das kaputt!")]
    [TestCase("Das ist gefährlich.", "Das gefährlich.")]
    [TestCase("Das ist unmöglich.", "Das unmöglich.")]
    [TestCase("Ich bin bereit!", "Ich bereit!")]
    [TestCase("Wir sind bereit.", "Wir bereit.")]
    [TestCase("Das ist kein Problem.", "Kein Problem.|Kein Problem, da.")]
    [TestCase("Das ist gut, aber es fehlt Strom.", "Das ist gut, aber es fehlt Strom.")]
    [TestCase("Ich bin bereit, sobald du kommst.", "Ich bin bereit, sobald du kommst.")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Mein Freund und meine Freundin.", "Mein Freund und meine Freundin.")]
    [TestCase("Mein Kollege ist hier.", "Mein Kollege ist hier.")]
    [TestCase("Der Scanner zeigt Sigma, Zeta und 100 Prozent.", "Der Scanner zeigt Sigma, Zeta und 100 Prozent.")]
    [TestCase("H2O APE NT-123.", "H2O APE NT-123.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    public async Task GermanSpeechRemainsReadable(string input, string expected)
    {
        await WithCulture("de-DE", system =>
        {
            var variants = expected.Split('|');
            for (var i = 0; i < 8; i++)
                Assert.That(variants, Does.Contain(system.Accentuate(input)));
        });
    }

    [Test]
    public async Task EnglishAccentStillUsesItsOriginalRules()
    {
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("k"), Is.EqualTo("к")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Ja!"), Is.EqualTo("Da!")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("k"), Is.EqualTo("к")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Ja!"), Is.EqualTo("Da!")));
    }

    private async Task WithCulture(string culture, Action<RussianAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<RussianAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
