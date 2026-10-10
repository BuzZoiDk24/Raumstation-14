using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanSpanishAccentTest : GameTest
{
    [TestCase("Hallo!", "¡Hola!|¡Buenas!")]
    [TestCase("Guten Tag!", "¡Buenos días!")]
    [TestCase("Vielen Dank.", "Muchas gracias.|Mil gracias.")]
    [TestCase("bitte?", "¿Cómo?|¿Perdón?")]
    [TestCase("BITTE?", "¿CÓMO?|¿PERDÓN?")]
    [TestCase("Bitte!", "¡Bitte!")]
    [TestCase("Bitte?", "¿Cómo?|¿Perdón?")]
    [TestCase("Wie geht es dir?", "¿Wie geht es dir?")]
    [TestCase("Die Station hat Stress.", "Die Estation hat Estress.")]
    [TestCase("Der Scanner ist kaputt.", "Der Escanner ist kaputt.")]
    [TestCase("mein kollege", "Mi compañero")]
    [TestCase("Meine Kollegin kommt.", "Mi compañera kommt.")]
    [TestCase("Meine Kolleginnen kommen.", "Mis compañeras kommen.")]
    [TestCase("Bitte hilf mir.", "Bitte hilf mir.")]
    [TestCase("Eine Bitte.", "Eine Bitte.")]
    [TestCase("Bitte", "Bitte")]
    [TestCase("bitte", "bitte")]
    [TestCase("Bitte.", "Bitte.")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Bitte sehr!", "¡Bitte sehr!")]
    [TestCase("Keine Ursache.", "De nada.")]
    [TestCase("Nichts zu danken.", "De nada.")]
    [TestCase("Gern geschehen!", "¡De nada!")]
    [TestCase("Schicht, Schule und Schnee.", "Schicht, Schule und Schnee.")]
    [TestCase("Sigma Zeta APE SMES H2O S-123 bleiben.", "Sigma Zeta APE SMES H2O S-123 bleiben.")]
    [TestCase("Alles klar!", "¡Vale!|¡De acuerdo!")]
    [TestCase("Das wächst natürlich.", "Das wächst natürlich.")]
    [TestCase("Bringe die Akten in Ordnung.", "Bringe die Akten in Ordnung.")]
    [TestCase("Mit meinem Kollegen.", "Mit meinem Kollegen.")]
    [TestCase("NEIN!", "¡NO!")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("Oh nein!", "¡Ay, no!")]
    [TestCase("Ach nein!", "¡Ay, no!")]
    [TestCase("Aua!", "¡Ay!")]
    [TestCase("Autsch!", "¡Ay!")]
    [TestCase("Hilfe!", "¡Ayuda!")]
    [TestCase("Was für ein Glück!", "¡Qué suerte!")]
    [TestCase("Zum Glück!", "¡Qué suerte!")]
    [TestCase("Hoffentlich!", "¡Ojalá!")]
    [TestCase("Wirklich?", "¿En serio?")]
    [TestCase("Echt?", "¿En serio?")]
    [TestCase("Im Ernst?", "¿En serio?")]
    [TestCase("Keine Ahnung!", "¡Ni idea!")]
    [TestCase("Einen Moment!", "¡Un momento!")]
    [TestCase("Moment mal!", "¡Un momento!")]
    [TestCase("Beeil dich!", "¡Date prisa!")]
    [TestCase("Mach schnell!", "¡Date prisa!")]
    [TestCase("Hör auf!", "¡Basta!")]
    [TestCase("Lass das!", "¡Basta!")]
    [TestCase("Wie schade!", "¡Qué pena!")]
    [TestCase("So ein Pech!", "¡Qué mala suerte!")]
    [TestCase("oh nein!", "¡Ay, no!")]
    [TestCase("OH NEIN!", "¡AY, NO!")]
    [TestCase("Ich brauche Hilfe beim Generator.", "Ich brauche Hilfe beim Generator.")]
    [TestCase("Ich hoffe, zum Glück ist nichts passiert.", "Ich hoffe, zum Glück ist nichts passiert.")]
    [TestCase("Das ist wirklich interessant.", "Das ist wirklich interessant.")]
    [TestCase("Hör auf den Kapitän.", "Hör auf den Kapitän.")]
    [TestCase("Mach schnell die Tür zu.", "Mach schnell die Tür zu.")]
    [TestCase("Mahlzeit!", "¡Mahlzeit!")]
    [TestCase("Guten Appetit!", "¡Buen provecho!")]
    [TestCase("Achtung!", "¡Cuidado!")]
    [TestCase("Vorsicht!", "¡Cuidado!")]
    [TestCase("Aus Achtung vor dem Kapitän.", "Aus Achtung vor dem Kapitän.")]
    [TestCase("Mit Vorsicht behandeln.", "Mit Vorsicht behandeln.")]
    [TestCase("Bitte schön vorsichtig sein.", "Bitte schön vorsichtig sein.")]
    [TestCase("Mein Freund kommt.", "Mein Freund kommt.")]
    [TestCase("Meine Freundin kommt.", "Meine Freundin kommt.")]
    [TestCase("mein Freund", "mein Freund")]
    [TestCase("meine Freundin", "meine Freundin")]
    [TestCase("Mein Kumpel kommt.", "Mi amigo kommt.")]
    [TestCase("Meine Freunde kommen.", "Mis amigos kommen.")]
    [TestCase("Meine Freundinnen kommen.", "Mis amigas kommen.")]
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
            Assert.That(system.Accentuate("Station!"), Is.EqualTo("¡Estation!")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheCorrectSoundRules()
    {
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Die Station."), Is.EqualTo("Die Estation.")));
        await WithCulture("en-US", system =>
            Assert.That(system.Accentuate("Station!"), Is.EqualTo("¡Estation!")));
        await WithCulture("de-DE", system =>
            Assert.That(system.Accentuate("Die Station."), Is.EqualTo("Die Estation.")));
    }

    private async Task WithCulture(string culture, Action<SpanishAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>()
                    .GetEntitySystem<SpanishAccentSystem>());
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
