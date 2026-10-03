using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanSentenceNameTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          parent: ClothingShoesGaloshes
          id: GermanSentenceNameTestChild
        """;

    [TestCase("ClothingShoesGaloshes", "Rutschfeste Gummistiefel", "die rutschfesten Gummistiefel", "rutschfeste Gummistiefel", "den rutschfesten Gummistiefeln")]
    [TestCase("ClothingUniformJumpsuitColorGrey", "Grauer Overall", "den grauen Overall", "einen grauen Overall", "dem grauen Overall")]
    [TestCase("ClothingUniformJumpskirtColorGrey", "Graues Uniformkleid", "das graue Uniformkleid", "ein graues Uniformkleid", "dem grauen Uniformkleid")]
    [TestCase("PortableFlasher", "Mobile Blendfalle", "die mobile Blendfalle", "eine mobile Blendfalle", "der mobilen Blendfalle")]
    public async Task FixedAdjectiveNames(string prototype, string display, string definite,
        string indefinite, string dative)
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
                item = entities.SpawnEntity(prototype, MapCoordinates.Nullspace);
                Assert.Multiple(() =>
                {
                    Assert.That(entities.GetComponent<MetaDataComponent>(item.Value).EntityName, Is.EqualTo(display));
                    Assert.That(loc.GetString("test-de-fixed-name-definite", ("entity", item.Value)), Is.EqualTo(definite));
                    Assert.That(loc.GetString("test-de-fixed-name-indefinite", ("entity", item.Value)).Trim(), Is.EqualTo(indefinite));
                    Assert.That(loc.GetString("test-de-fixed-name-dative", ("entity", item.Value)), Is.EqualTo(dative));
                    Assert.That(loc.GetString("pointing-system-point-at-other", ("other", item.Value)),
                        Is.EqualTo($"Du zeigst auf {definite}."));
                    Assert.That(loc.GetString("comp-hands-examine-wrapper", ("itemEntity", item.Value),
                        ("item", display)), Is.EqualTo(InsertHandColor(indefinite)));
                    Assert.That(loc.GetString("test-de-fixed-name-override", ("entity", item.Value),
                        ("name", "Unbekannt")), Is.EqualTo("Unbekannt"));
                    Assert.That(entities.GetComponent<MetaDataComponent>(item.Value).EntityName, Is.EqualTo(display));
                });
                // Renaming must not restore the localized prototype name.
                entities.System<MetaDataSystem>().SetEntityName(item.Value, "Rüdiger");
                Assert.That(loc.GetString("test-de-fixed-name-bare", ("entity", item.Value)), Is.EqualTo("Rüdiger"));
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

    private static string InsertHandColor(string phrase)
    {
        if (phrase.StartsWith("einen "))
            return "einen [color=paleturquoise]" + phrase[6..] + "[/color]";
        if (phrase.StartsWith("eine "))
            return "eine [color=paleturquoise]" + phrase[5..] + "[/color]";
        if (phrase.StartsWith("ein "))
            return "ein [color=paleturquoise]" + phrase[4..] + "[/color]";
        return "[color=paleturquoise]" + phrase + "[/color]";
    }

    [Test]
    public async Task InheritedAdjectiveMetadataDoesNotReplaceOverriddenName()
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
                item = entities.SpawnEntity("GermanSentenceNameTestChild", MapCoordinates.Nullspace);
                Assert.That(loc.GetString("test-de-fixed-name-bare", ("entity", item.Value)), Is.EqualTo("Spezialstiefel"));
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
}
