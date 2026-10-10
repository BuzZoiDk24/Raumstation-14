using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanAustrianAccentTest : GameTest
{
    [TestCase("Hallo!", "Servus!")]
    [TestCase("Guten Tag.", "Grüß Gott.")]
    [TestCase("Tschüss!", "Baba!")]
    [TestCase("Guten Appetit!", "Mahlzeit!")]
    [TestCase("Mahlzeit!", "Mahlzeit!")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Mir geht es gut.", "Mir geht’s leiwand.")]
    [TestCase("Alles klar!", "Passt scho!")]
    [TestCase("Das kann nicht wahr sein!", "Geh bitte!")]
    [TestCase("Alter!", "Oida!")]
    [TestCase("Echt?", "Echt jetzt?")]
    [TestCase("Das war nur Spaß.", "Des war nur a Schmäh.")]
    [TestCase("Ich habe nichts gesehen.", "I hab nix gesehen.")]
    [TestCase("Ich sehe dich nicht.", "I sehe di ned.")]
    [TestCase("Das ist ein bisschen seltsam.", "Das is a bissl seltsam.")]
    [TestCase("Sie ist meine Freundin.", "Sie is meine Freundin.")]
    [TestCase("Mein Freund ist hier.", "Mein Freund is hier.")]
    [TestCase("Die Aprikosen sind im Schrank.", "Die Marillen sind im Kasten.")]
    [TestCase("Im Januar gehe ich ins Krankenhaus.", "Im Jänner gehe i ins Spital.")]
    [TestCase("Mit einer Tüte und dem Brötchen.", "Mit einem Sackerl und der Semmel.")]
    [TestCase("Ich esse ein Brötchen.", "I esse eine Semmel.")]
    [TestCase("Ich esse die Brötchen.", "I esse die Semmeln.")]
    [TestCase("Ich esse Brötchen.", "I esse Brötchen.")]
    [TestCase("Mit der Sahne.", "Mit dem Obers.")]
    [TestCase("Das ist egal.", "Das is wurscht.")]
    [TestCase("ICH HABE NICHTS GESEHEN.", "I HAB NIX GESEHEN.")]
    [TestCase("Ich gebe die Zahl ein.", "I gebe die Zahl ein.")]
    [TestCase("Nichtschwimmer", "Nichtschwimmer")]
    [TestCase("Sie sagt: Hallo.", "Sie sagt: Hallo.")]
    [TestCase("Hallo. Ich habe nichts.", "Hallo. I hab nix.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("Ich scherze nur.", "I scherze nur.")]
    [TestCase("Wir scherzen nur.", "Wir scherzen nur.")]
    [TestCase("Scherze nicht mit mir!", "Scherze ned mit mir!")]
    [TestCase("Der Scherz war gut.", "Der Schmäh war gut.")]
    [TestCase("Ich mache einen Scherz.", "I mache einen Schmäh.")]
    [TestCase("Das sind gute Scherze.", "Das sind gute Schmähs.")]
    [TestCase("Die Scherze waren gut.", "Die Schmähs waren gut.")]
    [TestCase("Mit den Scherzen.", "Mit den Schmähs.")]
    [TestCase("Der Inhalt der Scherze.", "Der Inhalt der Schmähs.")]
    [TestCase("Scherzen ist nicht verboten.", "Scherzen is ned verboten.")]
    [TestCase("WIR SCHERZEN NUR.", "WIR SCHERZEN NUR.")]
    [TestCase("DIE SCHERZE WAREN GUT.", "DIE SCHMÄHS WAREN GUT.")]
    public async Task GermanSpeechRemainsReadable(string input, string expected)
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate(input), Is.EqualTo(expected)));
    }

    [Test]
    public async Task OtherLanguagesRemainUnchanged()
    {
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("Hallo! Ich habe nichts gesehen."),
                Is.EqualTo("Hallo! Ich habe nichts gesehen.")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Hallo!"), Is.EqualTo("Servus!")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("Hallo!"), Is.EqualTo("Hallo!")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Hallo!"), Is.EqualTo("Servus!")));
    }

    private async Task WithCulture(string culture, Action<AustrianAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<AustrianAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
