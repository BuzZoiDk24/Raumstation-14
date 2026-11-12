#nullable enable
using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.IntegrationTests.Fixtures.Attributes;
using Content.Server.Objectives;
using Content.Server.Objectives.Systems;
using Content.Shared.Anomaly.Components;
using Content.Shared.Mind;
using Content.Shared.Mind.Components;
using Content.Shared.Objectives.Components;
using Content.Shared.Roles;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Objectives;

[TestFixture]
public sealed class SupercriticalAnomaliesObjectiveTest : GameTest
{
    private const string ObjectiveId = "SupercritAnomaliesObjective";

    [TestPrototypes]
    private const string Prototypes = """
        - type: entity
          id: UnmodifiedNumberObjectiveTest
          components:
          - type: NumberObjective
            min: 2
            max: 4
        """;

    [TestCase("Scientist", 3)]
    [TestCase("Passenger", 1)]
    [TestCase("StationEngineer", 1)]
    [TestCase("MedicalDoctor", 1)]
    [TestCase("Clown", 1)]
    [TestCase("Bartender", 1)]
    [TestCase(null, 1)]
    public async Task ActualJobDeterminesTargetTitleAndCompletion(string? job, int expected)
    {
        await WithTraitor(job, mind =>
        {
            var entities = Pair.Server.EntMan;
            var systems = Pair.Server.ResolveDependency<IEntitySystemManager>();
            var objectives = systems.GetEntitySystem<ObjectivesSystem>();
            var numbers = systems.GetEntitySystem<NumberObjectiveSystem>();
            var objective = objectives.TryCreateObjective(mind.Owner, mind.Comp, ObjectiveId);
            Assert.That(objective, Is.Not.Null);
            try
            {
                Assert.That(numbers.GetTarget(objective!.Value), Is.EqualTo(expected));
                var title = entities.GetComponent<MetaDataComponent>(objective.Value).EntityName;
                Assert.That(title, Is.EqualTo(expected == 1
                    ? "Löse bei einer Anomalie eine überkritische Reaktion aus."
                    : "Löse bei 3 Anomalien eine überkritische Reaktion aus."));
                Assert.That(objectives.GetProgress(objective.Value, mind), Is.EqualTo(0f));

                // The assigned count is fixed; later job changes must not reroll it.
                systems.GetEntitySystem<SharedRoleSystem>().MindAddJobRole(mind.Owner,
                    silent: true, jobPrototype: expected == 3 ? "Passenger" : "Scientist");
                Assert.That(numbers.GetTarget(objective.Value), Is.EqualTo(expected));

                var normalShutdown = new AnomalyShutdownEvent(mind.Comp.OwnedEntity!.Value, false);
                entities.EventBus.RaiseLocalEvent(mind.Comp.OwnedEntity.Value, ref normalShutdown, true);
                Assert.That(objectives.GetProgress(objective.Value, mind), Is.EqualTo(0f));

                for (var i = 1; i <= expected; i++)
                {
                    var supercritical = new AnomalyShutdownEvent(mind.Comp.OwnedEntity.Value, true);
                    entities.EventBus.RaiseLocalEvent(mind.Comp.OwnedEntity.Value, ref supercritical, true);
                    Assert.That(objectives.GetProgress(objective.Value, mind),
                        Is.EqualTo((float) i / expected).Within(0.0001f));
                }
                Assert.That(objectives.IsCompleted(objective.Value, mind), Is.True);
            }
            finally
            {
                if (objective.HasValue)
                    entities.DeleteEntity(objective.Value);
            }
        });
    }

    [Test]
    public async Task NumberObjectivesWithoutJobOverridesRetainTheirRange()
    {
        await WithTraitor("Scientist", mind =>
        {
            var entities = Pair.Server.EntMan;
            var numbers = Pair.Server.ResolveDependency<IEntitySystemManager>()
                .GetEntitySystem<NumberObjectiveSystem>();
            var objective = entities.SpawnEntity("UnmodifiedNumberObjectiveTest", MapCoordinates.Nullspace);
            try
            {
                for (var i = 0; i < 16; i++)
                {
                    var assigned = new ObjectiveAssignedEvent(mind.Owner, mind.Comp);
                    entities.EventBus.RaiseLocalEvent(objective, ref assigned);
                    Assert.That(numbers.GetTarget(objective), Is.InRange(2, 4));
                }
            }
            finally
            {
                entities.DeleteEntity(objective);
            }
        });
    }

    private async Task WithTraitor(string? job, Action<Entity<MindComponent>> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var entities = Pair.Server.EntMan;
            var systems = Pair.Server.ResolveDependency<IEntitySystemManager>();
            var minds = systems.GetEntitySystem<SharedMindSystem>();
            var roles = systems.GetEntitySystem<SharedRoleSystem>();
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            var body = entities.SpawnEntity(null, MapCoordinates.Nullspace);
            entities.AddComponent<MindContainerComponent>(body);
            var mind = minds.CreateMind(null);
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                minds.TransferTo(mind.Owner, body);
                roles.MindAddRole(mind.Owner, "MindRoleTraitor", silent: true);
                if (job != null)
                    roles.MindAddJobRole(mind.Owner, silent: true, jobPrototype: job);
                assertion(mind);
            }
            finally
            {
                entities.DeleteEntity(mind.Owner);
                entities.DeleteEntity(body);
                if (previous != null)
                    loc.SetCulture(previous);
            }
        });
    }
}
