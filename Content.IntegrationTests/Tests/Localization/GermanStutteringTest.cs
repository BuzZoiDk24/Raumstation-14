using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanStutteringTest : GameTest
{
    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          id: GermanStutterTestDouble
          components:
          - type: StutteringAccent
            matchRandomProb: 1
            fourRandomProb: 0
            threeRandomProb: 0
            cutRandomProb: 0

        - type: entity
          id: GermanStutterTestTriple
          components:
          - type: StutteringAccent
            matchRandomProb: 1
            fourRandomProb: 0
            threeRandomProb: 1
            cutRandomProb: 0

        - type: entity
          id: GermanStutterTestQuadruple
          components:
          - type: StutteringAccent
            matchRandomProb: 1
            fourRandomProb: 1
            threeRandomProb: 0
            cutRandomProb: 0

        - type: entity
          id: GermanStutterTestCut
          components:
          - type: StutteringAccent
            matchRandomProb: 1
            fourRandomProb: 0
            threeRandomProb: 0
            cutRandomProb: 1

        - type: entity
          id: GermanStutterTestNone
          components:
          - type: StutteringAccent
            matchRandomProb: 0
            fourRandomProb: 0
            threeRandomProb: 0
            cutRandomProb: 0
        """;

    [TestCase("sch", "sch-sch")]
    [TestCase("Sch", "Sch-Sch")]
    [TestCase("SCH", "SCH-SCH")]
    [TestCase("ch", "ch-ch")]
    [TestCase("Ch", "Ch-Ch")]
    [TestCase("CH", "CH-CH")]
    [TestCase("Fisch", "F-Fisch-sch")]
    [TestCase("ich", "ich-ch")]
    [TestCase("Schicht", "Sch-Schich-cht-t")]
    [TestCase("b", "b-b")]
    [TestCase("Z", "Z-Z")]
    [TestCase("aeiouäöü", "aeiouäöü")]
    [TestCase("AEIOUÄÖÜ", "AEIOUÄÖÜ")]
    [TestCase("123 !?.,", "123 !?.,")]
    [TestCase("", "")]
    [TestCase("   ", "   ")]
    [TestCase("s", "s-s")]
    [TestCase("c", "c-c")]
    [TestCase("h", "h-h")]
    [TestCase("SC", "S-SC-C")]
    public async Task GermanSoundsAreRepeatedAsUnits(string input, string expected)
    {
        await WithEntity("de-DE", "GermanStutterTestDouble", (system, entity) =>
            Assert.That(system.Accentuate(input, entity), Is.EqualTo(expected)));
    }

    [TestCase("GermanStutterTestTriple", "Sch-Sch-Sch ch-ch-ch")]
    [TestCase("GermanStutterTestQuadruple", "Sch-Sch-Sch-Sch ch-ch-ch-ch")]
    [TestCase("GermanStutterTestCut", " ")]
    [TestCase("GermanStutterTestNone", "Sch ch")]
    public async Task ExistingProbabilityBranchesApplyToCompleteSounds(string prototype, string expected)
    {
        await WithEntity("de-DE", prototype, (system, entity) =>
            Assert.That(system.Accentuate("Sch ch", entity), Is.EqualTo(expected)));
    }

    [Test]
    public async Task CultureSwitchPreservesTheEnglishRules()
    {
        await WithEntity("de-DE", "GermanStutterTestDouble", (system, entity) =>
            Assert.That(system.Accentuate("Sch", entity), Is.EqualTo("Sch-Sch")));
        await WithEntity("en-US", "GermanStutterTestDouble", (system, entity) =>
            Assert.That(system.Accentuate("Sch", entity), Is.EqualTo("S-Sc-ch-h")));
        await WithEntity("de-DE", "GermanStutterTestDouble", (system, entity) =>
            Assert.That(system.Accentuate("Sch", entity), Is.EqualTo("Sch-Sch")));
    }

    [Test]
    public async Task MissingAccentEntityLeavesSpeechUnchanged()
    {
        await WithEntity("de-DE", "GermanStutterTestDouble", (system, _) =>
            Assert.That(system.Accentuate("Die Schicht beginnt."), Is.EqualTo("Die Schicht beginnt.")));
    }

    private async Task WithEntity(string culture, string prototype,
        Action<StutteringSystem, Entity<StutteringAccentComponent>> assertion)
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
                var component = entities.GetComponent<StutteringAccentComponent>(uid);
                var system = Pair.Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<StutteringSystem>();
                assertion(system, new Entity<StutteringAccentComponent>(uid, component));
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
