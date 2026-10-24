using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Robust.Shared.Localization;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanNumberFormatTest : GameTest
{
    [TestCase(0, 0, "0")]
    [TestCase(10, 0, "10")]
    [TestCase(20, 0, "20")]
    [TestCase(100, 0, "100")]
    [TestCase(1000, 0, "1.000")]
    [TestCase(-10, 0, "-10")]
    [TestCase(9.99, 0, "10")]
    [TestCase(0, 2, "0")]
    [TestCase(10, 2, "10")]
    [TestCase(12.5, 3, "12,5")]
    public async Task FixedNumbers(double value, int decimals, string expected)
    {
        await WithGermanCulture(loc => Assert.That(loc.GetString("test-de-natural-fixed",
            ("value", value), ("decimals", decimals)), Is.EqualTo(expected)));
    }

    [TestCase(0, 0, "0%")]
    [TestCase(0.1, 0, "10%")]
    [TestCase(0.5, 0, "50%")]
    [TestCase(1, 0, "100%")]
    [TestCase(0, 2, "0%")]
    [TestCase(0.5, 2, "50%")]
    [TestCase(0.125, 2, "12,5%")]
    public async Task Percentages(double value, int decimals, string expected)
    {
        await WithGermanCulture(loc => Assert.That(loc.GetString("test-de-natural-percent",
            ("value", value), ("decimals", decimals)), Is.EqualTo(expected)));
    }

    [TestCase(0, "Haftzeit zurücksetzen (0 % verbüßt)")]
    [TestCase(10, "Haftzeit zurücksetzen (10 % verbüßt)")]
    [TestCase(50, "Haftzeit zurücksetzen (50 % verbüßt)")]
    [TestCase(90, "Haftzeit zurücksetzen (90 % verbüßt)")]
    public async Task PrisonLockerDisplay(double percent, string expected)
    {
        await WithGermanCulture(loc => Assert.That(loc.GetString("genpop-locker-action-reset-sentence",
            ("percent", percent)), Is.EqualTo(expected)));
    }

    private async Task WithGermanCulture(Action<ILocalizationManager> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                assertion(loc);
            }
            finally
            {
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }
}
