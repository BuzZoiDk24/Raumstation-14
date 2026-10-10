using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanBerlinAccentTest : GameTest
{
    [TestCase("Hallo!", "Tach!|Na!")]
    [TestCase("Guten Tag!", "Tach!")]
    [TestCase("Guten Morgen!", "Morjen!")]
    [TestCase("Vielen Dank!", "Danke dir!")]
    [TestCase("Ja!", "Jo!")]
    [TestCase("Nein!", "Nee!")]
    [TestCase("Keine Ahnung.", "Keene Ahnung.")]
    [TestCase("Echt?", "Echt jetz?")]
    [TestCase("Lass das!", "Lass det!")]
    [TestCase("Beeil dich!", "Mach hinne!")]
    [TestCase("Meine Güte!", "Meine Jüte!")]
    [TestCase("Gut!", "Jut!|Jut, jut!")]
    [TestCase("Ich habe nichts gesehen.", "Ick hab nüscht jesehn.")]
    [TestCase("Ich habe das nicht gemacht.", "Ick hab det nich jemacht.")]
    [TestCase("Was ist das?", "Wat is det?")]
    [TestCase("Das geht nicht.", "Det jeht nich.")]
    [TestCase("Ich brauche ein bisschen Hilfe.", "Ick brauche ’n bisschen Hilfe.")]
    [TestCase("Er sieht mich und dich.", "Er sieht mir und dir.")]
    [TestCase("ich habe nichts gesagt", "Ick hab nüscht jesagt")]
    [TestCase("ICH HABE NICHTS GESEHEN!", "ICK HAB NÜSCHT JESEHN!")]
    [TestCase("Der Generator ist kaputt.", "Der Generator is kaputt.")]
    [TestCase("Sigma Zeta APE H2O.", "Sigma Zeta APE H2O.")]
    [TestCase("Ichthyologie und nichtlinear.", "Ichthyologie und nichtlinear.")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Mahlzeit!", "Mahlzeit!")]
    [TestCase("Mein Freund und meine Freundin.", "Mein Freund und meine Freundin.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    public async Task GermanSpeechRemainsReadable(string input, string expected)
    {
        await WithCulture("de-DE", system =>
        {
            var variants = expected.Split('|');
            for (var i = 0; i < 8; i++)
                Assert.That(variants, Does.Contain(system.ApplyReplacements(input, "scottish")));
        });
    }

    [Test]
    public async Task EnglishAccentStillUsesItsOriginalRules()
    {
        await WithCulture("en-US", system =>
            Assert.That(system.ApplyReplacements("do", "scottish"), Is.EqualTo("dae")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.ApplyReplacements("Ich habe nichts gesehen.", "scottish"), Is.EqualTo("Ick hab nüscht jesehn.")));
        await WithCulture("en-US", system =>
            Assert.That(system.ApplyReplacements("do", "scottish"), Is.EqualTo("dae")));
        await WithCulture("de-DE", system =>
            Assert.That(system.ApplyReplacements("Ich habe nichts gesehen.", "scottish"), Is.EqualTo("Ick hab nüscht jesehn.")));
    }

    private async Task WithCulture(string culture, Action<ReplacementAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<ReplacementAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
