using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanIndefiniteArticleTest : GameTest
{
    [Test]
    public async Task ArticleMatrix()
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var localization = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = localization.DefaultCulture;
            try
            {
                localization.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                var cases = new[] { "nominative", "accusative", "dative", "genitive" };
                var rows = new (string Gender, string[] Indefinite, string[] Definite)[]
                {
                    ("masculine", new[] { "ein", "einen", "einem", "eines" }, new[] { "der", "den", "dem", "des" }),
                    ("feminine", new[] { "eine", "eine", "einer", "einer" }, new[] { "die", "die", "der", "der" }),
                    ("neuter", new[] { "ein", "ein", "einem", "eines" }, new[] { "das", "das", "dem", "des" }),
                    ("male", new[] { "ein", "einen", "einem", "eines" }, new[] { "der", "den", "dem", "des" }),
                    ("female", new[] { "eine", "eine", "einer", "einer" }, new[] { "die", "die", "der", "der" }),
                };
                var pluralDefinite = new[] { "die", "die", "den", "der" };
                Assert.Multiple(() =>
                {
                    foreach (var (gender, indefinite, definite) in rows)
                    {
                        for (var i = 0; i < cases.Length; i++)
                        {
                            var arguments = new (string, object)[]
                            {
                                ("gender", gender), ("case", cases[i]), ("number", "singular")
                            };
                            Assert.That(localization.GetString("test-de-indefinite-article", arguments),
                                Is.EqualTo(indefinite[i]), $"Indefinite: {gender}, {cases[i]}");
                            Assert.That(localization.GetString("test-de-default-article", arguments),
                                Is.EqualTo(definite[i]), $"Default: {gender}, {cases[i]}");
                            Assert.That(localization.GetString("test-de-explicit-definite-article", arguments),
                                Is.EqualTo(definite[i]), $"Explicit definite: {gender}, {cases[i]}");
                        }
                    }

                    for (var i = 0; i < cases.Length; i++)
                    {
                        var arguments = new (string, object)[]
                        {
                            ("gender", "masculine"), ("case", cases[i]), ("number", "plural")
                        };
                        Assert.That(localization.GetString("test-de-indefinite-article", arguments), Is.Empty);
                        Assert.That(localization.GetString("test-de-default-article", arguments),
                            Is.EqualTo(pluralDefinite[i]));
                    }

                    Assert.That(localization.GetString("test-de-indefinite-article",
                        ("gender", "proper"), ("case", "nominative"), ("number", "singular")), Is.Empty);
                    Assert.That(localization.GetString("test-de-indefinite-article",
                        ("gender", "masculine"), ("case", "invalid"), ("number", "singular")), Is.Empty);
                });
            }
            finally
            {
                if (previousCulture != null)
                    localization.SetCulture(previousCulture);
            }
        });
    }

    [TestCase("ClothingBackpack", "Rucksack", "einen")]
    [TestCase("MobMouse", "Maus", "eine")]
    public async Task HandExamineUsesLocalizedNounGender(string prototype, string name, string article)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var localization = Pair.Server.ResolveDependency<ILocalizationManager>();
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var previousCulture = localization.DefaultCulture;
            EntityUid? item = null;
            try
            {
                localization.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                item = entities.SpawnEntity(prototype, MapCoordinates.Nullspace);
                var result = localization.GetString("comp-hands-examine-wrapper",
                    ("item", name), ("itemEntity", item.Value));
                Assert.That(result, Is.EqualTo($"{article} [color=paleturquoise]{name}[/color]"));
            }
            finally
            {
                if (item != null)
                    entities.DeleteEntity(item.Value);
                if (previousCulture != null)
                    localization.SetCulture(previousCulture);
            }
        });
    }
}
