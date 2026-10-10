using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanSwissAccentTest : GameTest
{
    [TestCase("Hallo!", "Grüezi!")]
    [TestCase("Hi!", "Hoi!")]
    [TestCase("Guten Morgen.", "Guete Morge.")]
    [TestCase("Guten Abend!", "Guete Abig!")]
    [TestCase("Auf Wiedersehen!", "Uf Wiederluege!")]
    [TestCase("Tschüss!", "Tschüss!")]
    [TestCase("Danke.", "Merci.")]
    [TestCase("Vielen Dank!", "Merci vielmol!")]
    [TestCase("vielen dank", "Merci vielmol")]
    [TestCase("VIELEN DANK!", "MERCI VIELMOL!")]
    [TestCase("Gern geschehen.", "Gern gscheh.")]
    [TestCase("Guten Appetit!", "En Guete!")]
    [TestCase("Nein!", "Nei!")]
    [TestCase("Ich komme nicht.", "Ich komme nid.")]
    [TestCase("Ich habe nichts gesehen.", "Ich ha nüt gseh.")]
    [TestCase("Ich brauche ein bisschen Hilfe.", "Ich brauche es bitzli Hilfe.")]
    [TestCase("Jetzt geht es los.", "Jetzt geht es los.")]
    [TestCase("Echt?", "Ächt jetzt?")]
    [TestCase("Wirklich?", "Würkli?")]
    [TestCase("Das ist echt gefährlich.", "Das isch echt gefährlich.")]
    [TestCase("Keine Ahnung.", "Kei Ahnig.")]
    [TestCase("Beeil dich!", "Mach vorwärts!")]
    [TestCase("Hör auf!", "Hör uf!")]
    [TestCase("Lass das!", "Lah das si!")]
    [TestCase("Lass das Gerät stehen.", "Lass das Gerät stehen.")]
    [TestCase("Schade!", "Schad!")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Mahlzeit!", "Mahlzeit!")]
    [TestCase("Schatz Freundin Station Sigma Zeta APE H2O.", "Schatz Freundin Station Sigma Zeta APE H2O.")]
    [TestCase("Nichtleiter und nichtsahnend.", "Nichtleiter und nichtsahnend.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("Ich habe jedenfalls nichts gesehen.", "Ich ha emel nüt gseh.")]
    [TestCase("Ich habe zumindest nichts gesehen.", "Ich ha emel nüt gseh.")]
    [TestCase("Jedenfalls bin ich hier.", "Emel bin ich hier.")]
    [TestCase("zumindest bin ich hier.", "Emel bin ich hier.")]
    [TestCase("Wohl kaum!", "Äuä!")]
    [TestCase("Sicher nicht!", "Äuä!")]
    [TestCase("Von wegen!", "Äuä!")]
    [TestCase("von wegen!", "Äuä!")]
    [TestCase("VON WEGEN!", "ÄUÄ!")]
    [TestCase("Das ist wohl kaum möglich.", "Das isch wohl kaum möglich.")]
    [TestCase("Ich komme sicher nicht.", "Ich komme sicher nid.")]
    [TestCase("Er spricht von wegenlosen Feldern.", "Er spricht von wegenlosen Feldern.")]
    [TestCase("Das ist nicht gut.", "Das isch nid gut.")]
    [TestCase("Ich habe das gesagt.", "Ich ha das gseit.")]
    [TestCase("Ich habe das gemacht.", "Ich ha das gmacht.")]
    [TestCase("Wir haben nichts gemacht.", "Wir haben nüt gmacht.")]
    [TestCase("Wir können und müssen gehen.", "Wir können und müssen gehen.")]
    [TestCase("ist", "Isch")]
    [TestCase("IST", "ISCH")]
    [TestCase("habe", "Ha")]
    [TestCase("HABE", "HA")]
    [TestCase("Er hat es gesehen.", "Er hat es gseh.")]
    [TestCase("Eine Liste mit Istwerten.", "Eine Liste mit Istwerten.")]
    [TestCase("Sigma Zeta APE H2O.", "Sigma Zeta APE H2O.")]
    [TestCase("Das ist kein Problem!", "Kei Sach!")]
    [TestCase("Kein Problem!", "Kei Sach!")]
    [TestCase("Kein Thema!", "Kei Sach!")]
    [TestCase("Alles klar!", "Isch guet!")]
    [TestCase("In Ordnung!", "Isch guet!")]
    [TestCase("Passt schon!", "Isch guet!")]
    [TestCase("Das passt!", "Das passt scho!")]
    [TestCase("Das passt schon!", "Das passt scho!")]
    [TestCase("Keine Sorge!", "Mach der kei Sorge!")]
    [TestCase("Mach dir keine Sorgen!", "Mach der kei Sorge!")]
    [TestCase("Kein Stress!", "Nur nid hudle!")]
    [TestCase("Nur keine Hektik!", "Nur nid hudle!")]
    [TestCase("Nicht hetzen!", "Nur nid hudle!")]
    [TestCase("Lass dir Zeit!", "Lah der Ziit!")]
    [TestCase("Mach langsam!", "Mach langsam, nur nid hudle!")]
    [TestCase("So ein Mist!", "So en Chabis!")]
    [TestCase("So ein Quatsch!", "So en Chabis!")]
    [TestCase("So ein Unsinn!", "So en Chabis!")]
    [TestCase("Ach du meine Güte!", "Potz tuusig!")]
    [TestCase("Meine Güte!", "Potz tuusig!")]
    [TestCase("Donnerwetter!", "Potz tuusig!")]
    [TestCase("Potzblitz!", "Potz tuusig!")]
    [TestCase("Das kann nicht wahr sein!", "Das git’s doch nid!")]
    [TestCase("Das gibt es doch nicht!", "Das git’s doch nid!")]
    [TestCase("Das ist doch Unsinn!", "Das isch doch Chabis!")]
    [TestCase("Das ist doch Quatsch!", "Das isch doch Chabis!")]
    [TestCase("Das ist verrückt!", "Das isch ja gspunne!")]
    [TestCase("Das ist ja verrückt!", "Das isch ja gspunne!")]
    [TestCase("Spinnst du!", "Hesch en Egge ab!")]
    [TestCase("Bist du verrückt!", "Hesch en Egge ab!")]
    [TestCase("Du kannst nicht alles haben!", "De Füfer und s Weggli git’s nid!")]
    [TestCase("Du willst alles haben!", "Du wetsch de Füfer und s Weggli!")]
    [TestCase("Jetzt haben wir den Salat!", "Jetzt hämmer de Salat!")]
    [TestCase("Das ging daneben!", "Das isch i d Hose gange!")]
    [TestCase("Das ist schiefgegangen!", "Das isch i d Hose gange!")]
    [TestCase("Das sieht nicht gut aus!", "Das gseht nid guet us!")]
    [TestCase("Das sieht schlecht aus!", "Das gseht nid guet us!")]
    [TestCase("Das kommt nicht gut!", "Das chunnt nid guet!")]
    [TestCase("Das ist mir egal!", "Das isch mir gliich!")]
    [TestCase("Mir ist das egal!", "Das isch mir gliich!")]
    [TestCase("Ich bin bereit!", "Ich bi parat!")]
    [TestCase("Bist du bereit!", "Bisch parat!")]
    [TestCase("Los geht’s!", "Uf gaht’s!")]
    [TestCase("Los geht's!", "Uf gaht’s!")]
    [TestCase("Auf geht’s!", "Uf gaht’s!")]
    [TestCase("Auf geht's!", "Uf gaht’s!")]
    [TestCase("Gehen wir!", "Gömmer!")]
    [TestCase("Lass uns gehen!", "Gömmer!")]
    [TestCase("Schauen wir mal!", "Luege mer mal!")]
    [TestCase("Mal sehen!", "Luege mer mal!")]
    [TestCase("Das schauen wir uns an!", "Das luege mer a!")]
    [TestCase("Ich verstehe nur Bahnhof!", "Ich verstah nur Bahnhof!")]
    [TestCase("Hast du Tomaten auf den Augen!", "Hesch Tomate uf de Auge!")]
    [TestCase("Ich habe die Nase voll!", "Ich ha d Nase voll!")]
    [TestCase("Mir reicht es!", "Jetzt längt’s!")]
    [TestCase("Jetzt reicht es!", "Jetzt längt’s!")]
    [TestCase("Ich bin müde!", "Ich bi müed!")]
    [TestCase("Ich habe Hunger!", "Ich ha Hunger!")]
    [TestCase("Ich habe Durst!", "Ich ha Durscht!")]
    [TestCase("Das ist lecker!", "Das isch fein!")]
    [TestCase("Das schmeckt gut!", "Das isch fein!")]
    [TestCase("Das ist zu teuer!", "Das choschtet es halbs Vermöge!")]
    [TestCase("Viel Glück!", "Viel Glück, toi toi toi!")]
    [TestCase("Viel Erfolg!", "Viel Glück, toi toi toi!")]
    [TestCase("Gut gemacht!", "Das hesch guet gmacht!")]
    [TestCase("Das hast du gut gemacht!", "Das hesch guet gmacht!")]
    [TestCase("Das klappt schon!", "Das bringe mer scho hi!")]
    [TestCase("Das schaffen wir!", "Das bringe mer scho hi!")]
    [TestCase("Das ist super!", "Das isch lässig!")]
    [TestCase("Das ist toll!", "Das isch lässig!")]
    [TestCase("Das ist klasse!", "Das isch lässig!")]
    [TestCase("Ich mache nur Spaß!", "Isch nur en Spass!")]
    [TestCase("Ich mache nur Spass!", "Isch nur en Spass!")]
    [TestCase("War nur Spaß!", "Isch nur en Spass!")]
    [TestCase("War nur Spass!", "Isch nur en Spass!")]
    [TestCase("DU KANNST NICHT ALLES HABEN!", "DE FÜFER UND S WEGGLI GIT’S NID!")]
    [TestCase("Ich fahre mit dem Fahrrad.", "Ich fahre mit dem Velo.")]
    [TestCase("Die Schokolade ist hier.", "Die Schoggi isch hier.")]
    [TestCase("Die Fahrräder sind vor den Krankenhäusern.", "Die Velos sind vor den Spitälern.")]
    [TestCase("Meine Freundin ist hier.", "Meine Freundin isch hier.")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Das Unsinnige bleibt unsinnig.", "Das Unsinnige bleibt unsinnig.")]
    [TestCase("Das Brötchen ist hier.", "Das Weggli isch hier.")]
    [TestCase("Ich esse ein Brötchen.", "Ich esse ein Weggli.")]
    [TestCase("Die Brötchen sind frisch.", "Die Weggli sind frisch.")]
    [TestCase("Mit den Brötchen.", "Mit den Weggli.")]
    [TestCase("Der Duft des Brötchens.", "Der Duft des Wegglis.")]
    [TestCase("BRÖTCHEN!", "WEGGLI!")]
    [TestCase("Brötchenkrümel", "Brötchenkrümel")]
    [TestCase("Der Käse ist hier.", "Der Chäs isch hier.")]
    [TestCase("Ich esse Käse.", "Ich esse Chäs.")]
    [TestCase("Der Duft des Käses.", "Der Duft des Chäs.")]
    [TestCase("So ein Käse!", "So ein Chäs!")]
    [TestCase("KÄSE!", "CHÄS!")]
    [TestCase("Käsekuchen", "Käsekuchen")]
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
            Assert.That(system.Accentuate("this"), Is.EqualTo("zhis")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Nicht jetzt."), Is.EqualTo("Nid jetzt.")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("this"), Is.EqualTo("zhis")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Nicht jetzt."), Is.EqualTo("Nid jetzt.")));
    }

    private async Task WithCulture(string culture, Action<GermanAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<GermanAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
