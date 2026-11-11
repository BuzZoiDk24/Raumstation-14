using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Server.Cloning;
using Content.Shared.Preferences;
using Content.Shared.Speech;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Content.Shared.Speech.Prototypes;
using Content.Shared.Traits;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanAccentTraitsTest : GameTest
{
    private static readonly ProtoId<ReplacementAccentPrototype> LiarAccent = "liar";

    [Test]
    public async Task OnlyOneDialectButSpeechTraitsRemainCombinable()
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var prototypes = Pair.Server.ProtoMan;
            var profile = HumanoidCharacterProfile.DefaultWithSpecies()
                .WithTraitPreference("ScottishAccent", prototypes)
                .WithTraitPreference("Liar", prototypes);
            Assert.That(profile.TraitPreferences, Does.Contain((ProtoId<TraitPrototype>) "ScottishAccent"));
            Assert.That(profile.TraitPreferences, Does.Contain((ProtoId<TraitPrototype>) "Liar"));

            var rejected = profile.WithTraitPreference("GermanAccent", prototypes);
            Assert.That(rejected.TraitPreferences, Is.EquivalentTo(profile.TraitPreferences));

            var stuttering = HumanoidCharacterProfile.DefaultWithSpecies()
                .WithTraitPreference("GermanAccent", prototypes)
                .WithTraitPreference("SocialAnxiety", prototypes);
            Assert.That(stuttering.TraitPreferences, Does.Contain((ProtoId<TraitPrototype>) "SocialAnxiety"));
            Assert.That(stuttering.TraitPreferences, Does.Contain((ProtoId<TraitPrototype>) "GermanAccent"));

            ProtoId<TraitPrototype>[] legacy = ["GermanAccent", "FrenchAccent", "Liar"];
            Assert.That(profile.GetValidTraits(legacy, prototypes),
                Is.EquivalentTo(new ProtoId<TraitPrototype>[] { "GermanAccent", "Liar" }));
        });
    }

    [TestCase("liar")]
    [TestCase("scottish")]
    public async Task BerlinAndLiarApplyOnceInMeaningThenDialectOrder(string legacyAccent)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var entities = Pair.Server.EntMan;
            var systems = Pair.Server.ResolveDependency<IEntitySystemManager>();
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previousCulture = loc.DefaultCulture;
            var liar = Pair.Server.ProtoMan.Index(LiarAccent);
            var previousChance = liar.ReplacementChance;
            var speaker = entities.SpawnEntity(null, MapCoordinates.Nullspace);
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                liar.ReplacementChance = 1f;
                entities.AddComponent<ScottishAccentComponent>(speaker);
                entities.AddComponent<LiarAccentComponent>(speaker);
                // A legacy or equipment-granted copy must not reverse the lie a second time.
                systems.GetEntitySystem<ReplacementAccentSystem>().ApplyAccent(speaker, legacyAccent);
                var ev = new AccentGetEvent(speaker, "Ich habe nichts gesehen.");
                entities.EventBus.RaiseLocalEvent(speaker, ref ev);
                Assert.That(ev.Message, Is.EqualTo("Ick hab etwas jesehn."));
            }
            finally
            {
                entities.DeleteEntity(speaker);
                liar.ReplacementChance = previousChance;
                if (previousCulture != null)
                    loc.SetCulture(previousCulture);
            }
        });
    }

    [Test]
    public async Task CloningKeepsTraitsWithoutCopyingEquipmentAccents()
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var entities = Pair.Server.EntMan;
            var systems = Pair.Server.ResolveDependency<IEntitySystemManager>();
            var source = entities.SpawnEntity(null, MapCoordinates.Nullspace);
            var clone = entities.SpawnEntity(null, MapCoordinates.Nullspace);
            try
            {
                entities.AddComponent<ScottishAccentComponent>(source);
                entities.AddComponent<LiarAccentComponent>(source);
                systems.GetEntitySystem<ReplacementAccentSystem>().ApplyAccent(source, "cowboy");
                systems.GetEntitySystem<CloningSystem>().CloneComponents(source, clone, "CloningPod");
                Assert.That(entities.HasComponent<ScottishAccentComponent>(clone), Is.True);
                Assert.That(entities.HasComponent<LiarAccentComponent>(clone), Is.True);
                Assert.That(entities.HasComponent<ReplacementAccentComponent>(clone), Is.False);
            }
            finally
            {
                entities.DeleteEntity(source);
                entities.DeleteEntity(clone);
            }
        });
    }
}
