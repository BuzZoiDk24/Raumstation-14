using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Localizations;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class LocalizedListFormatTest : GameTest
{
    [TestCase("de-DE", "A und B", "A, B und C", "A oder B", "A, B oder C", "A und/oder B", "A, B und/oder C")]
    [TestCase("en-US", "A and B", "A, B, and C", "A or B", "A, B, or C", "A and/or B", "A, B, and/or C")]
    public async Task ConjunctionsKeepTheirLocalizedPunctuation(
        string culture, string andPair, string andMany, string orPair,
        string orMany, string andOrPair, string andOrMany)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                Assert.Multiple(() =>
                {
                    Assert.That(ContentLocalizationManager.FormatList(["A", "B"]), Is.EqualTo(andPair));
                    Assert.That(ContentLocalizationManager.FormatList(["A", "B", "C"]), Is.EqualTo(andMany));
                    Assert.That(ContentLocalizationManager.FormatListToOr(["A", "B"]), Is.EqualTo(orPair));
                    Assert.That(ContentLocalizationManager.FormatListToOr(["A", "B", "C"]), Is.EqualTo(orMany));
                    Assert.That(ContentLocalizationManager.FormatListToAndOr([]), Is.Empty);
                    Assert.That(ContentLocalizationManager.FormatListToAndOr(["A"]), Is.EqualTo("A"));
                    Assert.That(ContentLocalizationManager.FormatListToAndOr(["A", "B"]), Is.EqualTo(andOrPair));
                    Assert.That(ContentLocalizationManager.FormatListToAndOr(["A", "B", "C"]), Is.EqualTo(andOrMany));
                });
            }
            finally
            {
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
