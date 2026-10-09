using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanFrenchAccentTest : GameTest
{
    [TestCase("Hallo!", "Bonjour!|Salut!")]
    [TestCase("Guten Tag!", "Bonjour!")]
    [TestCase("guten morgen", "Bonjour")]
    [TestCase("Guten Abend.", "Bonsoir.")]
    [TestCase("Bis später!", "À plus tard!|À tout à l’heure!")]
    [TestCase("Tschüss!", "Salut!|À bientôt!")]
    [TestCase("Vielen Dank!", "Merci beaucoup!|Un grand merci!")]
    [TestCase("Dankeschön.", "Merci.|Merci bien.")]
    [TestCase("Entschuldigung!", "Pardon!|Excusez-moi!")]
    [TestCase("bitte?", "Pardon?|Comment?")]
    [TestCase("Bitte hilf mir.", "Bitte ’ilf mir.")]
    [TestCase("Das ist eine Bitte.", "Das ist eine Bitte.")]
    [TestCase("Ich habe die Maschine nicht repariert.", "Isch ’abe die Maschine nischt repariert.")]
    [TestCase("Du kannst mich und dich nicht hören.", "Du kannst misch und disch nischt ’ören.")]
    [TestCase("Ich möchte euch nichts sagen.", "Isch möschte eusch nischts sagen.")]
    [TestCase("Leider.", "Hélas.")]
    [TestCase("Mein Kollege und meine Kollegin.", "Mon collègue und ma collègue.")]
    [TestCase("Meine Kolleginnen kommen.", "Mes collègues kommen.")]
    [TestCase("mein Freund", "Mon ami|Mon cher ami")]
    [TestCase("meine Freundin", "Mon amie|Ma chère amie")]
    [TestCase("Meine Freunde kommen.", "Mes amis kommen.")]
    [TestCase("Mit meinem Freund und meiner Freundin.", "Mit meinem Freund und meiner Freundin.")]
    [TestCase("Die Hand hält den Hammer.", "Die ’And ’ält den ’Ammer.")]
    [TestCase("HUD HPLC H2O bleiben hier.", "HUD HPLC H2O bleiben ’ier.")]
    [TestCase("Das Buch, der Schichtplan und die Nachricht.", "Das Buch, der Schichtplan und die Nachricht.")]
    [TestCase("Ichthyologie und nichtlinear sind Fachwörter.", "Ichthyologie und nichtlinear sind Fachwörter.")]
    [TestCase("JA! NEIN!", "OUI! NON!")]
    [TestCase("ICH HABE HUNGER!", "ISCH ’ABE ’UNGER!")]
    [TestCase("hallo! ich habe nichts.", "Bonjour! Isch ’abe nischts.|Salut! Isch ’abe nischts.")]
    [TestCase("mein Gott", "Mon Dieu|Oh là là")]
    [TestCase("Achtung!", "Attention!")]
    [TestCase("Natürlich!", "Mais oui!|Bien sûr!")]
    [TestCase("Alles klar!", "D’accord!|Entendu!")]
    [TestCase("Gut gemacht!", "Bravo!|Très bien!")]
    [TestCase("Das wächst natürlich.", "Das wächst natürlich.")]
    [TestCase("Bringe die Akten in Ordnung.", "Bringe die Akten in Ordnung.")]
    [TestCase("Das hast du gut gemacht.", "Das ’ast du gut gemacht.")]
    [TestCase("Auf geht’s!", "Allons-y!|C’est parti!")]
    [TestCase("Warte mal!", "Attends!|Un instant!")]
    [TestCase("Kein Problem.", "Pas de problème.|Pas de souci.")]
    [TestCase("Das Brot ist fertig.", "Das Baguette ist fertig.")]
    [TestCase("Der Duft des Brotes.", "Der Duft des Baguettes.")]
    [TestCase("Ich nehme Brot und Kaffee.", "Isch nehme Baguette und Café.")]
    [TestCase("Die Brote liegen neben den Broten.", "Die Baguettes liegen neben den Baguettes.")]
    [TestCase("Der Käse und der Wein.", "Der Fromage und der Vin.")]
    [TestCase("Der Geruch des Käses und des Weins.", "Der Geruch des Fromages und des Vins.")]
    [TestCase("Die Weine und die Käse.", "Die Vins und die Fromages.")]
    [TestCase("Mit meinen Weinen und meinen Käsen.", "Mit meinen Vins und meinen Fromages.")]
    [TestCase("Ich esse den Käse.", "Isch esse den Fromage.")]
    [TestCase("Ich muss weinen und weine nicht.", "Isch muss weinen und weine nischt.")]
    [TestCase("Prost!", "Santé!")]
    [TestCase("Zum Wohl!", "Santé!")]
    [TestCase("Mahlzeit!", "Bon appétit!")]
    [TestCase("Eine warme Mahlzeit.", "Eine warme Mahlzeit.")]
    [TestCase("Zum Wohl der Station.", "Zum Wohl der Station.")]
    [TestCase("Guten Appetit!", "Bon appétit!")]
    [TestCase("Bis morgen!", "À demain!")]
    [TestCase("Einverstanden!", "D’accord!")]
    [TestCase("Unglaublich!", "Incroyable!")]
    [TestCase("Kaffeepulver und Käseplatte.", "Kaffeepulver und Käseplatte.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("!!!", "!!!")]
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
            Assert.That(system.Accentuate("hello there!"), Is.EqualTo("'ello 'zere !")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Ich habe nichts."), Is.EqualTo("Isch ’abe nischts.")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("hello there!"), Is.EqualTo("'ello 'zere !")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Ich habe nichts."), Is.EqualTo("Isch ’abe nischts.")));
    }

    private async Task WithCulture(string culture, Action<FrenchAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<FrenchAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
