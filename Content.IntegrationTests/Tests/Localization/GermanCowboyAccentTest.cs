using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.EntitySystems;
using Content.Shared.Speech.Prototypes;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Prototypes;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanCowboyAccentTest : GameTest
{
    private static readonly ProtoId<ReplacementAccentPrototype> CowboyAccent = "cowboy";

    [TestCase("Hallo!", "Howdy!")]
    [TestCase("Guten Tag.", "Howdy.")]
    [TestCase("hi", "Howdy")]
    [TestCase("HI!", "HOWDY!")]
    [TestCase("Tschüss!", "Bis dann, Partner!")]
    [TestCase("Vielen Dank.", "Bin dir verbunden.")]
    [TestCase("Danke euch!", "Bin euch verbunden!")]
    [TestCase("Was zur Hölle?", "Was in drei Teufels Namen?")]
    [TestCase("Bitte", "Bitte")]
    [TestCase("Bitte?", "Bitte?")]
    [TestCase("Bitte schön.", "Bitte schön.")]
    [TestCase("Bitte hilf mir!", "Sei so gut und hilf mir!")]
    [TestCase("Bitte helfen Sie mir.", "Seien Sie so gut und helfen Sie mir.")]
    [TestCase("Meine Freundin wartet.", "Meine Freundin wartet.")]
    [TestCase("Mein Freund wartet.", "Mein Freund wartet.")]
    [TestCase("Meine Kollegin wartet.", "Meine Partnerin wartet.")]
    [TestCase("Der Kollege kommt.", "Der Partner kommt.")]
    [TestCase("Ich helfe einem Kollegen.", "Ich helfe einem Partner.")]
    [TestCase("Mit den Kollegen.", "Mit den Partnern.")]
    [TestCase("Ich suche den Kollegen.", "Ich suche den Kollegen.")]
    [TestCase("Die Kumpels kommen.", "Die Partner kommen.")]
    [TestCase("Mit den Kumpels.", "Mit den Partnern.")]
    [TestCase("Der Kaffee ist heiß.", "Der Bohnensaft ist heiß.")]
    [TestCase("Der Geschmack des Kaffees.", "Der Geschmack des Bohnensafts.")]
    [TestCase("Zwei Kaffees bitte.", "Zwei Kaffees bitte.")]
    [TestCase("Das Gefängnis ist voll.", "Das Kittchen ist voll.")]
    [TestCase("Die Tür des Gefängnisses.", "Die Tür des Kittchens.")]
    [TestCase("Der Revolver.", "Der Sechsschüsser.")]
    [TestCase("Mit den Revolvern.", "Mit den Sechsschüssern.")]
    [TestCase("Der Anfänger kommt.", "Das Greenhorn kommt.")]
    [TestCase("Ich sehe einen Neuling.", "Ich sehe ein Greenhorn.")]
    [TestCase("Mit einem Anfänger.", "Mit einem Greenhorn.")]
    [TestCase("Die Anfänger kommen.", "Die Greenhorns kommen.")]
    [TestCase("Mit den Anfängern.", "Mit den Greenhorns.")]
    [TestCase("Eine Anfängerin kommt.", "Ein Greenhorn kommt.")]
    [TestCase("Mit einer Anfängerin.", "Mit einem Greenhorn.")]
    [TestCase("Der Verräter flieht.", "Der Gesetzlose flieht.")]
    [TestCase("Ich sehe einen Verräter.", "Ich sehe einen Gesetzlosen.")]
    [TestCase("Mit einem Verräter.", "Mit einem Gesetzlosen.")]
    [TestCase("Die Tasche des Verräters.", "Die Tasche des Gesetzlosen.")]
    [TestCase("Die Verräterin flieht.", "Die Gesetzlose flieht.")]
    [TestCase("Mit einer Verräterin.", "Mit einer Gesetzlosen.")]
    [TestCase("Die Verräter kommen.", "Die Gesetzlosen kommen.")]
    [TestCase("Die Verräterinnen kommen.", "Die Gesetzlosen kommen.")]
    [TestCase("Mit den Verrätern.", "Mit den Gesetzlosen.")]
    [TestCase("Eine Waffe liegt hier.", "Ein Schießeisen liegt hier.")]
    [TestCase("Mit der Waffe.", "Mit dem Schießeisen.")]
    [TestCase("Wegen einer Waffe.", "Wegen eines Schießeisens.")]
    [TestCase("Meine Waffe ist geladen.", "Mein Schießeisen ist geladen.")]
    [TestCase("Die Waffen sind geladen.", "Die Schießeisen sind geladen.")]
    [TestCase("Die Batterie und die Zelle.", "Die Batterie und die Zelle.")]
    [TestCase("Die Bombe explodiert.", "Die Dynamitstange explodiert.")]
    [TestCase("Die Cyborgs kommen.", "Die Blechmänner kommen.")]
    [TestCase("Die Zelle meines Cyborgs.", "Die Zelle meines Cyborgs.")]
    [TestCase("Die Namen der Verräter.", "Die Namen der Verräter.")]
    [TestCase("Die Namen der Anfänger.", "Die Namen der Anfänger.")]
    [TestCase("Mit den Cyborgs.", "Mit den Blechmännern.")]
    [TestCase("Die Zelle des Cyborgs.", "Die Zelle des Blechmanns.")]
    [TestCase("Er wurde festgenommen.", "Er wurde eingefangen.")]
    [TestCase("Die Handschellen.", "Die Fesseln.")]
    [TestCase("Das Essen ist fertig.", "Das Futter ist fertig.")]
    [TestCase("Wir essen jetzt.", "Wir essen jetzt.")]
    [TestCase("Das sind meine Schuhe.", "Das sind meine Stiefel.")]
    [TestCase("Mit den Schuhen.", "Mit den Stiefeln.")]
    [TestCase("Ich habe Hunger.", "Mir knurrt der Magen.")]
    [TestCase("Ich bin wütend!", "Mir platzt der Kragen!")]
    [TestCase("Ich bin müde.", "Ich bin hundemüde.")]
    [TestCase("Ich bin sehr müde.", "Ich bin mächtig hundemüde.")]
    [TestCase("Die müden Kollegen.", "Die hundemüden Kollegen.")]
    [TestCase("Ich habe geschrien.", "Ich habe gebrüllt.")]
    [TestCase("Lauf!", "Mach dich vom Acker!")]
    [TestCase("Lauft!", "Macht euch vom Acker!")]
    [TestCase("Auf der Toilette.", "Auf dem Plumpsklo.")]
    [TestCase("Zur Toilette.", "Zum Plumpsklo.")]
    [TestCase("Meine Toilette.", "Meine Toilette.")]
    [TestCase("Der Kaffeeautomat.", "Der Kaffeeautomat.")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    public async Task GermanWesternSpeechRemainsGrammatical(string input, string expected)
    {
        await WithCulture("de-DE", 1f, system =>
            Assert.That(system.ApplyReplacements(input, "cowboy"), Is.EqualTo(expected)));
    }

    [Test]
    public async Task ZeroReplacementChanceLeavesSpeechUnchanged()
    {
        await WithCulture("de-DE", 0f, system =>
            Assert.That(system.ApplyReplacements("Hallo!", "cowboy"), Is.EqualTo("Hallo!")));
    }

    [Test]
    public async Task CultureSwitchKeepsTheEnglishRules()
    {
        await WithCulture("de-DE", 1f, system =>
            Assert.That(system.ApplyReplacements("Hallo!", "cowboy"), Is.EqualTo("Howdy!")));
        await WithCulture("en-US", 1f, system =>
            Assert.That(system.ApplyReplacements("Hello!", "cowboy"), Is.EqualTo("Howdy!")));
        await WithCulture("de-DE", 1f, system =>
            Assert.That(system.ApplyReplacements("Hallo!", "cowboy"), Is.EqualTo("Howdy!")));
    }

    private async Task WithCulture(string culture, float chance, Action<ReplacementAccentSystem> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            var prototypes = Pair.Server.ResolveDependency<IPrototypeManager>();
            var prototype = prototypes.Index(CowboyAccent);
            var previousChance = prototype.ReplacementChance;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                prototype.ReplacementChance = chance;
                assertion(Pair.Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<ReplacementAccentSystem>());
            }
            finally
            {
                prototype.ReplacementChance = previousChance;
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }
}
