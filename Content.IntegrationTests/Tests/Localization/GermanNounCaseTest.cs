using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanNounCaseTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          parent: ClothingBackpack
          id: GermanNounCaseTestVariant
        - type: entity
          parent: ClothingBackpack
          id: GermanNounCaseTestRenamedVariant
        """;

    [TestCase("ClothingBackpack", "genitive", "weak", "Rucksacks")]
    [TestCase("ClothingHandsChameleon", "dative", "weak", "Chamäleonhandschuhen")]
    [TestCase("ClothingShoesChameleonNoSlips", "dative", "weak", "Chamäleonschuhen mit Rutschschutz")]
    [TestCase("MobCorgiPuppy", "accusative", "weak", "Corgiwelpen")]
    [TestCase("MobCorgiPuppy", "dative", "weak", "Corgiwelpen")]
    [TestCase("MobCorgiPuppy", "genitive", "weak", "Corgiwelpen")]
    [TestCase("HealingToolbox", "genitive", "weak", "heilenden Werkzeugkastens")]
    [TestCase("MedkitAdvanced", "dative", "weak", "erweiterten Erste-Hilfe-Set")]
    [TestCase("GermanNounCaseTestVariant", "genitive", "weak", "Rucksacks")]
    [TestCase("GermanNounCaseTestRenamedVariant", "genitive", "weak", "Spezialbeutel")]
    [TestCase("MobCorgiIan", "genitive", "weak", "Ian")]
    [TestCase("FoodDonkpocketSpicyWarm", "nominative", "weak", "warme scharfe Donk-Pocket")]
    [TestCase("FoodDonkpocketSpicyWarm", "accusative", "mixed", "warme scharfe Donk-Pocket")]
    [TestCase("FoodDonkpocketSpicyWarm", "dative", "mixed", "warmen scharfen Donk-Pocket")]
    [TestCase("FoodDonkpocketSpicyWarm", "genitive", "strong", "warmer scharfer Donk-Pocket")]
    [TestCase("FoodMeatChickenFriedVox", "accusative", "mixed", "mysteriöses frittiertes Hähnchen")]
    [TestCase("FoodMeatChickenFriedVox", "dative", "strong", "mysteriösem frittiertem Hähnchen")]
    [TestCase("FoodMeatChickenFriedVox", "genitive", "weak", "mysteriösen frittierten Hähnchens")]
    [TestCase("FoodBreadFrenchToast", "dative", "weak", "armen Rittern")]
    [TestCase("FoodCocoaBeans", "dative", "weak", "Kakaobohnen")]
    [TestCase("FoodPizzaMeatSlice", "genitive", "weak", "Stücks Fleischpizza")]
    [TestCase("FoodMeatFish", "genitive", "weak", "rohen Karpfenfilets")]
    [TestCase("FoodEggBoiled", "dative", "strong", "gekochtem Ei")]
    public async Task PrototypeNameForms(string prototype, string grammaticalCase, string form, string expected)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                Assert.That(loc.GetString("test-de-noun-case-prototype", ("prototype", prototype),
                    ("case", grammaticalCase), ("form", form)), Is.EqualTo(expected));
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }

    [Test]
    public async Task EntityGenitivePreservesDisplayAndRename()
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var previousCulture = loc.DefaultCulture;
            EntityUid? item = null;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                item = entities.SpawnEntity("ClothingBackpack", MapCoordinates.Nullspace);
                Assert.That(loc.GetString("test-de-noun-case-entity", ("entity", item.Value)), Is.EqualTo("des Rucksacks"));
                Assert.That(entities.GetComponent<MetaDataComponent>(item.Value).EntityName, Is.EqualTo("Rucksack"));
                entities.System<MetaDataSystem>().SetEntityName(item.Value, "Rüdiger");
                Assert.That(loc.GetString("test-de-noun-case-renamed", ("entity", item.Value)), Is.EqualTo("Rüdiger"));
            }
            finally
            {
                if (item != null)
                    entities.DeleteEntity(item.Value);
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }

    [TestCase("ClothingUniformJumpsuitColorGrey", "masculine", "singular", "false", "Dieses Geschenk scheint einen grauen Overall zu enthalten.", "Ein Bausatz, aus dem sich ein grauer Overall zusammenbauen lässt.")]
    [TestCase("ClothingShoesGaloshes", "neuter", "plural", "false", "Dieses Geschenk scheint rutschfeste Gummistiefel zu enthalten.", "Ein Bausatz, aus dem sich rutschfeste Gummistiefel zusammenbauen lassen.")]
    [TestCase("MobCorgiIan", "male", "singular", "true", "Dieses Geschenk scheint Ian zu enthalten.", "")]
    public async Task GiftPrototypeNames(string prototype, string gender, string number, string proper, string expected, string expectedFlatpack)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                Assert.That(loc.GetString("gift-packin-contains", ("prototype", prototype),
                    ("gender", gender), ("number", number), ("proper", proper)), Is.EqualTo(expected));
                if (proper == "false")
                {
                    Assert.That(loc.GetString("flatpack-entity-description", ("prototype", prototype),
                        ("gender", gender), ("number", number)), Is.EqualTo(expectedFlatpack));
                }
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }
}
