using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanGrammarRegressionTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          parent: Beaker
          id: GermanGrammarRegressionTestBeaker
        - type: entity
          parent: BaseItem
          id: GermanGrammarRegressionTestHolder
        """;

    [TestCase("ClothingEyesHudBeer", "die Bierbrille")]
    [TestCase("ClothingEyesHudFriedOnion", "die Zwiebelbrille")]
    [TestCase("ClothingEyesGlassesHiddenSecurity", "die Sonnenbrille")]
    public async Task GlassesUseFeminineArticles(string prototype, string expected)
    {
        await WithEntity(prototype, (loc, item) =>
            Assert.That(loc.GetString("test-de-regression-definite", ("entity", item)), Is.EqualTo(expected)));
    }

    [TestCase("ClothingBackpack", "Der Rucksack rutscht dir aus der Hand...", "Der Rucksack rutscht Anna aus der Hand...")]
    [TestCase("ClothingShoesGaloshes", "Die rutschfesten Gummistiefel rutschen dir aus der Hand...", "Die rutschfesten Gummistiefel rutschen Anna aus der Hand...")]
    public async Task SlippingItemsAgreeWithTheirNumber(string prototype, string self, string others)
    {
        await WithEntity(prototype, (loc, item) =>
        {
            Assert.That(loc.GetString("clumsy-grab-fail-message-user", ("item", item)), Is.EqualTo(self));
            Assert.That(loc.GetString("clumsy-grab-fail-message-others", ("item", item), ("holder", "Anna")), Is.EqualTo(others));
        });
    }

    [TestCase("ClothingBackpack", "Dies ist ein Foto von einem Rucksack.")]
    [TestCase("ClothingShoesGaloshes", "Dies ist ein Foto von rutschfesten Gummistiefeln.")]
    [TestCase("MobCorgiIan", "Dies ist ein Foto von Ian.")]
    public async Task PhotographsPreserveCaseAndSpacing(string prototype, string expected)
    {
        await WithEntity(prototype, (loc, item) =>
            Assert.That(loc.GetString("photograph-name-text", ("entity", item)), Is.EqualTo(expected)));
    }

    [TestCase("HandTeleporter", "Handteleporters")]
    [TestCase("ForensicScanner", "Spurenscanners")]
    [TestCase("AltarNanotrasen", "NanoTrasen-Altars")]
    [TestCase("ClothingOuterHardsuitVoidParamed", "Weltraumanzugs des Rettungsdienstes")]
    [TestCase("CaptainSabre", "Säbels des Kapitäns")]
    public async Task ObjectiveItemGenitives(string prototype, string expected)
    {
        var output = TestContext.Out;
        await WithLocalization(loc =>
        {
            var actual = loc.GetString("test-de-regression-genitive", ("prototype", prototype));
            output.WriteLine($"Genitiv {prototype}: erwartet [{expected}], erhalten [{actual}]");
            Assert.That(actual, Is.EqualTo(expected));
        });
    }

    [TestCase("masculine", "singular", "Rucksack", "Rucksack", "Der Rucksack würde")]
    [TestCase("neuter", "plural", "goldene Schlagringe", "goldenen Schlagringe", "Die goldenen Schlagringe würden")]
    [TestCase("proper", "singular", "Ian", "Ian", "Ian würde")]
    public async Task ThiefDescriptionsAgreeWithTheirNumber(string gender, string number, string name,
        string subject, string beginning)
    {
        await WithLocalization(loc =>
        {
            var args = new (string, object)[]
            {
                ("itemGender", gender), ("itemNumber", number), ("itemName", name), ("itemSubject", subject)
            };
            Assert.That(loc.GetString("objective-condition-thief-description", args),
                Is.EqualTo(beginning + " sich gut in meiner Sammlung machen!"));
            Assert.That(loc.GetString("objective-condition-thief-animal-description", args),
                Is.EqualTo(beginning + " sich gut in meiner Sammlung machen. Hauptsache lebendig!"));
        });
    }

    [TestCase("GermanGrammarRegressionTestBeaker", "Großer Becher", "ein großer Becher")]
    [TestCase("GermanGrammarRegressionTestHolder", "Beschädigte KI-Speichereinheit", "eine beschädigte KI-Speichereinheit")]
    public async Task InsertedItemNamesUseSentenceForms(string prototype, string display, string phrase)
    {
        await WithEntity(prototype, (loc, item) =>
        {
            Assert.That(loc.GetString("cryo-pod-examine", ("beakerEntity", item), ("beaker", display)),
                Is.EqualTo("Darin befindet sich " + phrase + "."));
            Assert.That(loc.GetString("station-ai-fixer-console-examination-station-ai-holder-present",
                ("holderEntity", item), ("holder", display)),
                Is.EqualTo("In der Konsole steckt " + phrase.Split(' ', 2)[0] + " [color=cyan]" + phrase.Split(' ', 2)[1] + "[/color]."));
            Assert.That(Pair.Server.ResolveDependency<IEntityManager>().GetComponent<MetaDataComponent>(item).EntityName,
                Is.EqualTo(display));
        });
    }

    public override async Task DoTeardown()
    {
        // Print the original result before pool disposal can add its own warning.
        var result = TestContext.CurrentContext.Result;
        TestContext.Out.WriteLine($"Ergebnis vor der Bereinigung: {result.Outcome}");
        foreach (var assertion in result.Assertions)
        {
            TestContext.Out.WriteLine(assertion.Message);
            TestContext.Out.WriteLine(assertion.StackTrace);
        }

        await base.DoTeardown();
    }

    private async Task WithLocalization(Action<ILocalizationManager> assertion)
    {
        var output = TestContext.Out;
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                assertion(loc);
            }
            catch (Exception error)
            {
                output.WriteLine("Fehler im Grammatiktest:");
                output.WriteLine(error.ToString());
                throw;
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }

    private async Task WithEntity(string prototype, Action<ILocalizationManager, EntityUid> assertion)
    {
        await WithLocalization(loc =>
        {
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var item = entities.SpawnEntity(prototype, MapCoordinates.Nullspace);
            try
            {
                assertion(loc, item);
            }
            finally
            {
                entities.DeleteEntity(item);
            }
        });
    }
}
