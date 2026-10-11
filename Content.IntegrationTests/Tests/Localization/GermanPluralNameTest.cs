using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanPluralNameTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          parent: ClothingBackpack
          id: GermanPluralInheritedTest
        - type: entity
          parent: ClothingBackpack
          id: GermanPluralDifferentNameTest
        """;

    [TestCase("LightBulb", 0, "0 Glühbirnen")]
    [TestCase("LightBulb", 1, "1 Glühbirne")]
    [TestCase("LightBulb", 5, "5 Glühbirnen")]
    [TestCase("LightTube", 5, "5 Leuchtstoffröhren")]
    [TestCase("ClothingBackpack", 2, "2 Rucksäcke")]
    [TestCase("ClothingUniformJumpsuitColorGrey", 1, "1 grauer Overall")]
    [TestCase("ClothingUniformJumpsuitColorGrey", 2, "2 graue Overalls")]
    [TestCase("ClothingShoesGaloshes", 2, "2 rutschfeste Gummistiefel")]
    [TestCase("MobCorgiIan", 2, "Ian × 2")]
    [TestCase("GermanPluralInheritedTest", 2, "2 Rucksäcke")]
    [TestCase("GermanPluralDifferentNameTest", 2, "Spezialbeutel × 2")]
    public async Task CountedNames(string prototype, int count, string expected)
    {
        await WithCulture("de-DE", loc =>
        {
            Assert.That(loc.GetString("test-de-counted-name", ("prototype", prototype),
                ("count", count)), Is.EqualTo(expected));
            Assert.That(loc.GetString("comp-entity-provider-entity-listing", ("prototype", prototype),
                ("amount", count), ("name", "unused")), Is.EqualTo("[color=gray]" + expected + "[/color]"));
        });
    }

    [TestCase("ClothingBackpack", "dative", "weak", "Rucksäcken")]
    [TestCase("ClothingBackpack", "genitive", "weak", "Rucksäcke")]
    [TestCase("ClothingUniformJumpsuitColorGrey", "nominative", "weak", "grauen Overalls")]
    [TestCase("ClothingUniformJumpsuitColorGrey", "accusative", "strong", "graue Overalls")]
    [TestCase("ClothingUniformJumpsuitColorGrey", "dative", "weak", "grauen Overalls")]
    public async Task PluralCaseAndAdjective(string prototype, string grammaticalCase, string form, string expected)
    {
        await WithCulture("de-DE", loc => Assert.That(loc.GetString("test-de-plural-name",
            ("prototype", prototype), ("case", grammaticalCase), ("form", form)), Is.EqualTo(expected)));
    }

    [Test]
    public async Task EntityNamesKeepTheirDisplayAndRename()
    {
        await WithCulture("de-DE", loc =>
        {
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var item = entities.SpawnEntity("ClothingBackpack", MapCoordinates.Nullspace);
            try
            {
                Assert.That(loc.GetString("test-de-plural-entity", ("entity", item)), Is.EqualTo("den Rucksäcken"));
                Assert.That(entities.GetComponent<MetaDataComponent>(item).EntityName, Is.EqualTo("Rucksack"));
                entities.System<MetaDataSystem>().SetEntityName(item, "Rüdiger");
                Assert.That(loc.GetString("test-de-plural-name", ("prototype", "ClothingBackpack"),
                    ("case", "nominative"), ("form", "strong")), Is.EqualTo("Rucksäcke"));
                // A renamed entity must not acquire the prototype's plural name.
                Assert.That(loc.GetString("test-de-plural-entity-name", ("entity", item)), Is.EqualTo("Rüdiger"));
            }
            finally
            {
                entities.DeleteEntity(item);
            }
        });
    }

    [Test]
    public async Task EnglishListingIsUnchanged()
    {
        await WithCulture("en-US", loc => Assert.That(loc.GetString("comp-entity-provider-entity-listing",
            ("name", "light bulb"), ("amount", 5), ("prototype", "LightBulb")),
            Is.EqualTo("[color=yellow]5[/color] [color=gray]light bulbs[/color]")));
    }

    private async Task WithCulture(string culture, Action<ILocalizationManager> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                assertion(loc);
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
