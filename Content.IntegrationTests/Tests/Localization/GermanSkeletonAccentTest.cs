using System.Globalization;
using System.Linq;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanSkeletonAccentTest : GameTest
{
    [TestCase("Ich habe Angst.", "Mir schlottern die Knochen.")]
    [TestCase("ich hab angst!", "Mir schlottern die Knochen!")]
    [TestCase("ICH HABE ANGST!", "MIR SCHLOTTERN DIE KNOCHEN!")]
    [TestCase("Ich bin erschrocken.", "Der Schreck sitzt mir in den Knochen.")]
    [TestCase("Ich habe mich erschrocken!", "Der Schreck sitzt mir in den Knochen!")]
    [TestCase("Das hat mich erschreckt.", "Der Schreck sitzt mir in den Knochen.")]
    [TestCase("Scheiße!", "KLAPPER KLAPPER!")]
    [TestCase("Scheisse!", "KLAPPER KLAPPER!")]
    [TestCase("Verdammt!", "KLAPPER KLAPPER!")]
    [TestCase("Ganz sicher!", "Knochensicher!")]
    [TestCase("Absolut.", "Knochensicher.")]
    [TestCase("Auf jeden Fall!", "Knochensicher!")]
    [TestCase("Definitiv!", "Knochensicher!")]
    [TestCase("Ich bin müde.", "Ich bin knochenmüde.")]
    [TestCase("Ich bin erschöpft!", "Ich bin knochenmüde!")]
    [TestCase("Ich habe Angst vor der Anomalie.", "Ich habe Angst vor der Anomalie.")]
    [TestCase("Sie hat Angst.", "Sie hat Angst.")]
    [TestCase("Er ist erschrocken.", "Er ist erschrocken.")]
    [TestCase("Ich bin müde, aber ich bleibe hier.", "Ich bin müde, aber ich bleibe hier.")]
    [TestCase("Das ist absolut gefährlich.", "Das ist absolut gefährlich.")]
    [TestCase("Der Knochen liegt neben dem Rucksack.", "Der Knochen liegt neben dem Rucksack.")]
    [TestCase("Sigma Zeta APE H2O.", "Sigma Zeta APE H2O.")]
    [TestCase("tone", "tone")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    public async Task GermanSpeechRemainsReadable(string input, string expected)
    {
        await WithCulture("de-DE", system =>
        {
            var variants = string.IsNullOrWhiteSpace(input)
                ? new[] { expected }
                : new[] { expected, string.Concat(expected, " ACK ACK!") };
            for (var i = 0; i < 8; i++)
                Assert.That(variants, Does.Contain(system.Accentuate(input)));
        });
    }

    [Test]
    public async Task EnglishAccentStillUsesItsOriginalRules()
    {
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("tone"), Does.Match(@"^bone(?: ACK ACK!)?$")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(new[] { "Knochensicher!", "Knochensicher! ACK ACK!" }, Does.Contain(system.Accentuate("Absolut!"))));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("tone"), Does.Match(@"^bone(?: ACK ACK!)?$")));
        await WithCulture("de-DE", system =>
            Assert.That(new[] { "Knochensicher!", "Knochensicher! ACK ACK!" }, Does.Contain(system.Accentuate("Absolut!"))));
    }

    private async Task WithCulture(string culture, Action<SkeletonAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<SkeletonAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
