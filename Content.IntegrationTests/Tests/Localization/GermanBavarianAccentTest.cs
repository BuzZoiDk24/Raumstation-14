using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanBavarianAccentTest : GameTest
{
    [TestCase("Hallo!", "Servus!")]
    [TestCase("Guten Tag!", "Grüß Gott!")]
    [TestCase("Guten Morgen.", "Guadn Morgn.")]
    [TestCase("Tschüss!", "Servus!")]
    [TestCase("Auf Wiedersehen!", "Pfiat di!")]
    [TestCase("Danke!", "Dankschee!")]
    [TestCase("Guten Appetit!", "An Guadn!")]
    [TestCase("Ja!", "Jo!")]
    [TestCase("Nein!", "Na!")]
    [TestCase("Echt?", "Echt jetz?")]
    [TestCase("Was?", "Wos?")]
    [TestCase("Keine Ahnung.", "Koane Ahnung.")]
    [TestCase("Lass das!", "Lass des!")]
    [TestCase("Lass das Gerät stehen.", "Lass das Gerät stehen.")]
    [TestCase("Beeil dich!", "Mach hi!")]
    [TestCase("Mist!", "Zefix!")]
    [TestCase("Meine Güte!", "Jessas!")]
    [TestCase("Ich habe nichts gesehen.", "I hab nix gsehn.")]
    [TestCase("Ich habe das nicht gemacht.", "I hab das ned gmacht.")]
    [TestCase("Wir haben ein bisschen Zeit.", "Mia ham a bissl Zeit.")]
    [TestCase("Wir sind hier.", "Mia san hier.")]
    [TestCase("Wir müssen gehen.", "Mia miassn gehn.")]
    [TestCase("Wir kommen gleich.", "Mia kemma gleich.")]
    [TestCase("ich habe nichts gesagt", "I hab nix gsagt")]
    [TestCase("ICH HABE NICHTS GESEHEN!", "I HAB NIX GSEHN!")]
    [TestCase("Das ist nicht gut.", "Das is ned gut.")]
    [TestCase("Sie haben nichts gesehen.", "Sie ham nix gsehn.")]
    [TestCase("Er kommt gleich.", "Er kimmt gleich.")]
    [TestCase("Ich komme gleich.", "I kimm gleich.")]
    [TestCase("Ichthyologie und nichtlinear.", "Ichthyologie und nichtlinear.")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Mahlzeit!", "Mahlzeit!")]
    [TestCase("Sigma Zeta APE H2O Station Scanner.", "Sigma Zeta APE H2O Station Scanner.")]
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
            Assert.That(system.Accentuate("and"), Is.EqualTo("an'")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Ich habe nichts gesehen."), Is.EqualTo("I hab nix gsehn.")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("and"), Is.EqualTo("an'")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Ich habe nichts gesehen."), Is.EqualTo("I hab nix gsehn.")));
    }

    private async Task WithCulture(string culture, Action<SouthernAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<SouthernAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
