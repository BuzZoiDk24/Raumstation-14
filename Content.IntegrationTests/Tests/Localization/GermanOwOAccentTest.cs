using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanOwOAccentTest : GameTest
{
    [TestCase("du", "dwu")]
    [TestCase("Du", "Dwu")]
    [TestCase("DU", "DWU")]
    [TestCase("dich", "dwich")]
    [TestCase("Dich", "Dwich")]
    [TestCase("DICH", "DWICH")]
    [TestCase("dir", "dwiw")]
    [TestCase("Dir", "Dwiw")]
    [TestCase("DIR", "DWIW")]
    [TestCase("Du, dich, dir.", "Dwu, dwich, dwiw.")]
    [TestCase("Hallo.", "Hawwo.")]
    [TestCase("Ich brauche Hilfe.", "Ich bwauche Hiwfe.")]
    [TestCase("Der rote Rucksack.", "Dew wote Wucksack.")]
    [TestCase("Durch die Tür.", "Duwch die Tüw.")]
    [TestCase("Verdirb nichts.", "Vewdiwb nichts.")]
    [TestCase("Dichtung", "Dichtung")]
    [TestCase("Die Richtung stimmt.", "Die Wichtung stimmt.")]
    [TestCase("Du hältst deinen Rucksack.", "Dwu häwtst deinen Wucksack.")]
    [TestCase("DU BRAUCHST HILFE.", "DWU BWAUCHST HIWFE.")]
    [TestCase("you", "you")]
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
            Assert.That(system.Accentuate("you"), Is.EqualTo("wu")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Du you"), Is.EqualTo("Dwu you")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("you"), Is.EqualTo("wu")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Du you"), Is.EqualTo("Dwu you")));
    }

    [Test]
    public async Task ExclamationMarksKeepTheOriginalFaces()
    {
        await WithCulture("de-DE", system =>
        {
            var allowed = new[]
            {
                "Hawwo (•`ω´•)", "Hawwo ;;w;;", "Hawwo owo",
                "Hawwo UwU", "Hawwo >w<", "Hawwo ^w^",
            };
            for (var i = 0; i < 8; i++)
                Assert.That(allowed, Does.Contain(system.Accentuate("Hallo!")));
        });
    }

    private async Task WithCulture(string culture, Action<OwOAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<OwOAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
