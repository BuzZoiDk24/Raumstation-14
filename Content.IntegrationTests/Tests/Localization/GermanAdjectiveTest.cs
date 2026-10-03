using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Content.Shared.NameIdentifier;
using Content.Shared.NameModifier.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanAdjectiveTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          id: GermanAdjectiveTestBackpack
          components:
          - type: NameIdentifier
            group: AdjectivePrefix
        - type: entity
          id: GermanAdjectiveTestMouse
          components:
          - type: NameIdentifier
            group: AdjectivePrefix
        - type: entity
          id: GermanAdjectiveTestTool
          components:
          - type: NameIdentifier
            group: AdjectivePrefix
        - type: entity
          id: GermanAdjectiveTestBoots
          components:
          - type: NameIdentifier
            group: AdjectivePrefix
        """;

    [Test]
    public async Task AdjectiveMatrix()
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                var cases = new[] { "nominative", "accusative", "dative", "genitive" };
                var genders = new[] { "masculine", "feminine", "neuter", "plural" };
                var rows = new (string Form, string[] Endings)[]
                {
                    ("strong", new[] { "er", "en", "em", "en", "e", "e", "er", "er",
                        "es", "es", "em", "en", "e", "e", "en", "er" }),
                    ("weak", new[] { "e", "en", "en", "en", "e", "e", "en", "en",
                        "e", "e", "en", "en", "en", "en", "en", "en" }),
                    ("mixed", new[] { "er", "en", "en", "en", "e", "e", "en", "en",
                        "es", "es", "en", "en", "en", "en", "en", "en" }),
                };
                Assert.Multiple(() =>
                {
                    foreach (var (form, endings) in rows)
                    {
                        for (var gender = 0; gender < genders.Length; gender++)
                        {
                            for (var grammaticalCase = 0; grammaticalCase < cases.Length; grammaticalCase++)
                            {
                                var arguments = new (string, object)[]
                                {
                                    ("stem", "staubig"), ("gender", genders[gender]), ("form", form),
                                    ("case", cases[grammaticalCase]),
                                    ("number", gender == 3 ? "plural" : "singular"),
                                };
                                Assert.That(loc.GetString("test-de-adjective", arguments),
                                    Is.EqualTo("staubig" + endings[gender * 4 + grammaticalCase]),
                                    $"{form}, {genders[gender]}, {cases[grammaticalCase]}");
                            }
                        }
                    }
                    Assert.That(loc.GetString("test-de-adjective-default"), Is.EqualTo("staubiger"));
                });
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }

    [TestCase("GermanAdjectiveTestBackpack", "Staubiger Rucksack", "den staubigen Rucksack", "einen staubigen Rucksack", "einem staubigen Rucksack", "des staubigen Rucksacks")]
    [TestCase("GermanAdjectiveTestMouse", "Staubige Maus", "die staubige Maus", "eine staubige Maus", "einer staubigen Maus", "der staubigen Maus")]
    [TestCase("GermanAdjectiveTestTool", "Staubiges Werkzeug", "das staubige Werkzeug", "ein staubiges Werkzeug", "einem staubigen Werkzeug", "des staubigen Werkzeugs")]
    [TestCase("GermanAdjectiveTestBoots", "Staubige Stiefel", "die staubigen Stiefel", "staubige Stiefel", "staubigen Stiefeln", "der staubigen Stiefel")]
    public async Task ContextualNames(string prototype, string display, string definite,
        string indefinite, string dative, string genitive)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var names = entities.System<NameModifierSystem>();
            var previousCulture = loc.DefaultCulture;
            EntityUid? item = null;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                item = entities.SpawnEntity(prototype, MapCoordinates.Nullspace);
                entities.GetComponent<NameIdentifierComponent>(item.Value).FullIdentifier = "staubig";
                names.RefreshNameModifiers(item.Value);
                Assert.Multiple(() =>
                {
                    Assert.That(entities.GetComponent<MetaDataComponent>(item.Value).EntityName, Is.EqualTo(display));
                    Assert.That(loc.GetString("test-de-name-definite", ("entity", item.Value)), Is.EqualTo(definite));
                    Assert.That(loc.GetString("test-de-name-indefinite", ("entity", item.Value)).Trim(), Is.EqualTo(indefinite));
                    Assert.That(loc.GetString("test-de-name-dative", ("entity", item.Value)).Trim(), Is.EqualTo(dative));
                    Assert.That(loc.GetString("test-de-name-genitive", ("entity", item.Value)), Is.EqualTo(genitive));
                    Assert.That(loc.GetString("test-de-name-override", ("entity", item.Value),
                        ("name", "Unbekannt")), Is.EqualTo("Unbekannt"));
                    // Grammatical formatting must not mutate the displayed name.
                    Assert.That(entities.GetComponent<MetaDataComponent>(item.Value).EntityName, Is.EqualTo(display));
                });
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
