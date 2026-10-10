using System.Globalization;
using System.Linq;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanParrotAccentTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          id: GermanParrotTestRepeat
          components:
          - type: ParrotAccent
            longestWordRepeatChance: 1
            squawkPrefixChance: 0

        - type: entity
          id: GermanParrotTestPrefix
          components:
          - type: ParrotAccent
            longestWordRepeatChance: 0
            squawkPrefixChance: 1

        - type: entity
          id: GermanParrotTestSuffix
          components:
          - type: ParrotAccent
            longestWordRepeatChance: 0
            squawkPrefixChance: 0
        """;

    [TestCase("Der Schlüssel liegt hier.", "Schlüssel")]
    [TestCase("Größe", "Größe")]
    [TestCase("Die Überwachung läuft.", "Überwachung")]
    [TestCase("Bitte Äpfel", "Äpfel")]
    [TestCase("Äpfel", "Äpfel")]
    [TestCase("Öl und Ärger", "Ärger")]
    [TestCase("Der Fußboden glänzt.", "Fußboden")]
    [TestCase("Vorsicht, Säure!", "Vorsicht")]
    [TestCase("große Größe", "Größe")]
    [TestCase("Die Ärztin kommt.", "Ärztin")]
    public async Task RepeatedGermanWordsKeepTheirLetters(string input, string word)
    {
        await WithEntity("de-DE", "GermanParrotTestRepeat", (system, entity, loc) =>
            AssertRepeated(system, entity, loc, input, word));
    }

    [Test]
    public async Task ShortWordsStillUseTheNormalSquawk()
    {
        await WithEntity("de-DE", "GermanParrotTestRepeat", (system, entity, loc) =>
        {
            var allowed = Squawks(loc).Select(squawk => string.Concat("Öl! ", squawk));
            Assert.That(allowed, Does.Contain(system.Accentuate("Öl", entity)));
        });
    }

    [TestCase("GermanParrotTestPrefix", true)]
    [TestCase("GermanParrotTestSuffix", false)]
    public async Task PrefixAndSuffixKeepTheOriginalMessage(string prototype, bool prefix)
    {
        await WithEntity("de-DE", prototype, (system, entity, loc) =>
        {
            var allowed = Squawks(loc).Select(squawk => prefix
                ? string.Concat(squawk, " Hallo!")
                : string.Concat("Hallo! ", squawk));
            Assert.That(allowed, Does.Contain(system.Accentuate("Hallo!", entity)));
        });
    }

    [Test]
    public async Task CultureSwitchPreservesTheOriginalEnglishCleanup()
    {
        await WithEntity("de-DE", "GermanParrotTestRepeat", (system, entity, loc) =>
            AssertRepeated(system, entity, loc, "Der Schlüssel liegt hier.", "Schlüssel"));
        await WithEntity("en-US", "GermanParrotTestRepeat", (system, entity, loc) =>
            AssertRepeated(system, entity, loc, "Der Schlüssel liegt hier.", "Schlssel"));
        await WithEntity("de-DE", "GermanParrotTestRepeat", (system, entity, loc) =>
            AssertRepeated(system, entity, loc, "Der Schlüssel liegt hier.", "Schlüssel"));
    }

    [Test]
    public async Task MissingAccentEntityLeavesSpeechUnchanged()
    {
        await WithEntity("de-DE", "GermanParrotTestRepeat", (system, _, _) =>
            Assert.That(system.Accentuate("Der Schlüssel liegt hier."), Is.EqualTo("Der Schlüssel liegt hier.")));
    }

    private static string[] Squawks(ILocalizationManager loc)
    {
        return Enumerable.Range(1, 8)
            .Select(index => loc.GetString(string.Concat("accent-parrot-squawk-", index.ToString(CultureInfo.InvariantCulture))))
            .ToArray();
    }

    private static void AssertRepeated(ParrotAccentSystem system, Entity<ParrotAccentComponent> entity,
        ILocalizationManager loc, string input, string word)
    {
        var punctuated = input.EndsWith('!') || input.EndsWith('?') || input.EndsWith('.')
            ? input
            : string.Concat(input, "!");
        var allowed = Squawks(loc).Select(squawk => string.Concat(new[] { punctuated, " ", squawk, " ", word, "!" }));
        Assert.That(allowed, Does.Contain(system.Accentuate(input, entity)));
    }

    private async Task WithEntity(string culture, string prototype,
        Action<ParrotAccentSystem, Entity<ParrotAccentComponent>, ILocalizationManager> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var uid = entities.SpawnEntity(prototype, MapCoordinates.Nullspace);
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo(culture));
                var component = entities.GetComponent<ParrotAccentComponent>(uid);
                var system = Pair.Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<ParrotAccentSystem>();
                assertion(system, new Entity<ParrotAccentComponent>(uid, component), loc);
            }
            finally
            {
                entities.DeleteEntity(uid);
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
