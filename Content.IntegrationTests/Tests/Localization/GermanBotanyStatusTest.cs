using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanBotanyStatusTest : GameTest
{
    [TestCase("seeds-apple-name", "plural", "In der Pflanzschale wachsen [color=limegreen]Äpfel[/color].")]
    [TestCase("seeds-aloe-name", "singular", "In der Pflanzschale wächst [color=limegreen]Aloe[/color].")]
    [TestCase("seeds-wheat-name", "singular", "In der Pflanzschale wächst [color=limegreen]Weizen[/color].")]
    [TestCase("seeds-soybeans-name", "plural", "In der Pflanzschale wachsen [color=limegreen]Sojabohnen[/color].")]
    public async Task CropNameSelectsVerb(string plantId, string number, string expected)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                Assert.That(loc.TryGetString("botany-ui-plant-number", out var actualNumber,
                    ("plantId", plantId)), Is.True);
                Assert.That(actualNumber, Is.EqualTo(number));
                Assert.That(loc.GetString("botany-ui-tray-plant", ("plant", loc.GetString(plantId)),
                    ("plantNumber", actualNumber ?? "singular")), Is.EqualTo(expected));
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }
}
