using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanLizardAccentTest : GameTest
{
    [TestCase("Das ist heiß.", "Dasss issst heisss.")]
    [TestCase("Sicherheit", "SSSicherheit")]
    [TestCase("Wasser", "Wassser")]
    [TestCase("Schicht", "Schicht")]
    [TestCase("SCHICHT", "SCHICHT")]
    [TestCase("Schüssel", "Schüsssel")]
    [TestCase("Fuchs", "Fuchs")]
    [TestCase("wechseln", "wechseln")]
    [TestCase("WACHS", "WACHS")]
    [TestCase("sechs", "sssechs")]
    [TestCase("Füße", "Füssse")]
    [TestCase("FÜẞE", "FÜSSSE")]
    [TestCase("Straße", "SSStrassse")]
    [TestCase("DAS IST HEISS!", "DASSS ISSST HEISSS!")]
    [TestCase("Das Werkzeug ist im Rucksack.", "Dasss Werkzeug issst im Rucksssack.")]
    [TestCase("Sie hält das Werkzeug.", "SSSie hält dasss Werkzeug.")]
    [TestCase("Sigma und Zeta.", "SSSigma und Zeta.")]
    [TestCase("Taxi", "Takssi")]
    [TestCase("Box", "Bokss")]
    [TestCase("X-Ray", "ECKS-Ray")]
    [TestCase("H2O APE.", "H2O APE.")]
    [TestCase("sss", "sss")]
    [TestCase("SSS", "SSS")]
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
            Assert.That(system.Accentuate("Schicht"), Is.EqualTo("SSSchicht")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Schicht und Fuchs."), Is.EqualTo("Schicht und Fuchs.")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("Schicht"), Is.EqualTo("SSSchicht")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Schicht und Fuchs."), Is.EqualTo("Schicht und Fuchs.")));
    }

    private async Task WithCulture(string culture, Action<LizardAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<LizardAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
