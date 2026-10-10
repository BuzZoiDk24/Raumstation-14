using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanFrontalLispTest : GameTest
{
    [TestCase("Das ist heiß.", "Dath itht heith.")]
    [TestCase("Ich weiß es nicht.", "Ich weith eth nicht.")]
    [TestCase("Wasser bitte.", "Wather bitte.")]
    [TestCase("Die Schicht beginnt.", "Die Schicht beginnt.")]
    [TestCase("SCHICHT", "SCHICHT")]
    [TestCase("Schüssel", "Schüthel")]
    [TestCase("Fuchs", "Fuchs")]
    [TestCase("wechseln", "wechseln")]
    [TestCase("sechs", "thechs")]
    [TestCase("Zeit und Zeta.", "Zeit und Zeta.")]
    [TestCase("Taxi und Box.", "Taxi und Box.")]
    [TestCase("Füße", "Füthe")]
    [TestCase("FÜẞE", "FÜTHE")]
    [TestCase("DAS IST HEISS!", "DATH ITHT HEITH!")]
    [TestCase("Straße", "Thtrathe")]
    [TestCase("Sie hält das Werkzeug.", "Thie hält dath Werkzeug.")]
    [TestCase("Das Werkzeug ist im Rucksack.", "Dath Werkzeug itht im Ruckthack.")]
    [TestCase("Sigma und Epsilon.", "Thigma und Epthilon.")]
    [TestCase("H2O APE.", "H2O APE.")]
    [TestCase("Sicherheit", "Thicherheit")]
    [TestCase("SICHERHEIT", "THICHERHEIT")]
    [TestCase("Sie sagt es.", "Thie thagt eth.")]
    [TestCase("ss", "th")]
    [TestCase("SS", "TH")]
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
            Assert.That(system.Accentuate("Zeit"), Is.EqualTo("THeit")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Schicht und Fuchs."), Is.EqualTo("Schicht und Fuchs.")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("Zeit"), Is.EqualTo("THeit")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Schicht und Fuchs."), Is.EqualTo("Schicht und Fuchs.")));
    }

    private async Task WithCulture(string culture, Action<FrontalLispSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<FrontalLispSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
