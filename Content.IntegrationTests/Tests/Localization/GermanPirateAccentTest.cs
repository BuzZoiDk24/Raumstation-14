using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanPirateAccentTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          id: GermanPirateAccentTestSpeaker
          components:
          - type: PirateAccent
            yarrChance: 1
        """;

    [TestCase("Hallo! Ja, die Kisten gehören dem Kapitän.", "Ahoi! Aye, die Truhen gehören dem Käpt’n.")]
    [TestCase("Guten Morgen! Nein, ich habe keine Credits.", "Ahoi! Nee, ich habe keine Dublonen.")]
    [TestCase("Achtung! Autsch!", "Obacht! Arrgh!")]
    [TestCase("Du Idiot! Ihr Idioten!", "Du Landratte! Ihr Landratten!")]
    [TestCase("Mein Freund sucht seinen Freund und hilft einem Freund.", "Mein Kamerad sucht seinen Kameraden und hilft einem Kameraden.")]
    [TestCase("Die Freundin spricht mit ihren Freunden.", "Die Kameradin spricht mit ihren Kameraden.")]
    [TestCase("Der Freund meines Freundes kommt.", "Der Kamerad meines Kameraden kommt.")]
    [TestCase("Ich suche meinen Schatz. Mit dem Schatz kaufe ich ein Gewehr.", "Ich suche meine Beute. Mit der Beute kaufe ich eine Muskete.")]
    [TestCase("Der Wert des Schatzes und meines Gewehrs ist hoch.", "Der Wert der Beute und meiner Muskete ist hoch.")]
    [TestCase("Mit meinem Gewehr bewache ich die Schätze.", "Mit meiner Muskete bewache ich die Beute.")]
    [TestCase("Die Gewehre liegen bei den Schätzen.", "Die Musketen liegen bei der Beute.")]
    [TestCase("Mit meinen Schätzen kaufe ich Credits.", "Mit meiner Beute kaufe ich Dublonen.")]
    [TestCase("Viele Schätze und wenige Schätze.", "Viel Beute und wenig Beute.")]
    [TestCase("Mit diesen Schätzen und allen Schätzen.", "Mit dieser Beute und der gesamten Beute.")]
    [TestCase("schätze", "Beute")]
    [TestCase("Die Munition ist knapp. Mit der Munition laden wir nach.", "Das Schießpulver ist knapp. Mit dem Schießpulver laden wir nach.")]
    [TestCase("Wegen der Munition bleiben wir hier.", "Wegen des Schießpulvers bleiben wir hier.")]
    [TestCase("Mit eurer Munition und eurem Gewehr.", "Mit eurem Schießpulver und eurer Muskete.")]
    [TestCase("Wir sehen die Kisten und die Kisten.", "Wir sehen die Truhen und die Truhen.")]
    [TestCase("Ich bin dein Freund. Du bist hier. Das ist gut. Wir sind bereit.", "Ich bin dein Kamerad. Du bist hier. Das ist gut. Wir sind bereit.")]
    [TestCase("Der Freundeskreis kennt die Schatzkarte und das Gewehrfeuer.", "Der Freundeskreis kennt die Schatzkarte und das Gewehrfeuer.")]
    [TestCase("Kapitänin und Kapitäne.", "Kapitänin und Kapitäne.")]
    [TestCase("Mein bester Freund findet einen großen Schatz.", "Mein bester Freund findet einen großen Schatz.")]
    [TestCase("Der Verbrauch der Munition steigt.", "Der Verbrauch der Munition steigt.")]
    [TestCase("JA! HALLO! MEIN SCHATZ!", "AYE! AHOI! MEINE BEUTE!")]
    [TestCase("  Schatz!", "  Beute!")]
    [TestCase("Freund!", "Kamerad!")]
    [TestCase("Gewehr?", "Muskete?")]
    [TestCase("Munition.", "Schießpulver.")]
    [TestCase("mein schatz", "Meine Beute")]
    [TestCase("dein gewehr", "Deine Muskete")]
    [TestCase("   mein schatz!", "   Meine Beute!")]
    [TestCase("ich suche mein schatz", "ich suche meine Beute")]
    [TestCase("hallo! mein schatz ist hier.", "Ahoi! Meine Beute ist hier.")]
    [TestCase("hier ist die truhe. dein gewehr liegt darin.", "hier ist die truhe. Deine Muskete liegt darin.")]
    [TestCase("wo? mein schatz!", "wo? Meine Beute!")]
    [TestCase("„mein schatz“", "„Meine Beute“")]
    [TestCase("ich sage: „mein schatz“", "ich sage: „meine Beute“")]
    [TestCase("mein schatz und mein schatz", "Meine Beute und meine Beute")]
    [TestCase("MEIN SCHATZ!", "MEINE BEUTE!")]
    [TestCase("Mein Kollege hilft dem Kollegen und den Kollegen.", "Mein Schiffskamerad hilft dem Schiffskameraden und den Schiffskameraden.")]
    [TestCase("Meine Kollegin spricht mit ihren Kolleginnen.", "Meine Schiffskameradin spricht mit ihren Schiffskameradinnen.")]
    [TestCase("kollege!", "Schiffskamerad!")]
    [TestCase("meine kollegin", "Meine Schiffskameradin")]
    [TestCase("mein kollege", "Mein Schiffskamerad")]
    [TestCase("ich frage meine kollegin", "ich frage meine Schiffskameradin")]
    [TestCase("Mit meinem Kumpel suche ich meinen Kumpel.", "Mit meinem Kameraden suche ich meinen Kameraden.")]
    [TestCase("Die Kumpels helfen den Kumpeln.", "Die Kameraden helfen den Kameraden.")]
    [TestCase("Der Rat meines Kumpels.", "Der Rat meines Kameraden.")]
    [TestCase("KOLLEGIN UND KOLLEGEN", "SCHIFFSKAMERADIN UND SCHIFFSKAMERADEN")]
    [TestCase("Das Kollegium und der Kumpeltyp.", "Das Kollegium und der Kumpeltyp.")]
    [TestCase("Guten Tag, mein Kollege.", "Ahoi, mein Schiffskamerad.")]
    [TestCase("guten tag!", "Ahoi!")]
    [TestCase("GUTEN TAG!", "AHOI!")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("!!!", "!!!")]
    public async Task GermanWordsAndCases(string input, string expected)
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate(input), Is.EqualTo(expected)));
    }

    [Test]
    public async Task PrefixPreservesGermanNounCapitals()
    {
        await WithCulture("de-DE", system =>
        {
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var uid = entities.SpawnEntity("GermanPirateAccentTestSpeaker", MapCoordinates.Nullspace);
            try
            {
                var component = entities.GetComponent<PirateAccentComponent>(uid);
                Assert.That(system.Accentuate("Kisten stehen im Raum.", (uid, component)),
                    Does.Match(@"^(Arrr!|Harrr!) Truhen stehen im Raum\.$"));
                Assert.That(system.Accentuate("KISTEN!", (uid, component)),
                    Does.Match(@"^(ARRR!|HARRR!) TRUHEN!$"));
                Assert.That(system.Accentuate("", (uid, component)), Is.Empty);
            }
            finally
            {
                entities.DeleteEntity(uid);
            }
        });
    }

    [Test]
    public async Task EnglishPirateStillWorks()
    {
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("you are my friend"), Is.EqualTo("ya arrr me heartie")));
    }

    [Test]
    public async Task CultureSwitchKeepsBothWordLists()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Hallo, mein Freund!"), Is.EqualTo("Ahoi, mein Kamerad!")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("hello"), Is.EqualTo("ahoy")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Hallo"), Is.EqualTo("Ahoi")));
    }

    private async Task WithCulture(string culture, Action<PirateAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<PirateAccentSystem>());
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }
}
