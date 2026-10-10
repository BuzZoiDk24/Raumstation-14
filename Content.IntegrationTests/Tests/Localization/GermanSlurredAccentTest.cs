using System.Globalization;
using Content.IntegrationTests.Fixtures;
using Content.Shared.Speech.Components;
using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameObjects;
using Robust.Shared.Localization;
using Robust.Shared.Map;

namespace Content.IntegrationTests.Tests.Localization;

[TestFixture]
public sealed class GermanSlurredAccentTest : GameTest
{
    [TestCase("sch")]
    [TestCase("SCH")]
    [TestCase("ch")]
    [TestCase("CH")]
    [TestCase("Zzßẞ")]
    [TestCase("1234567890")]
    [TestCase("!?;,:")]
    [TestCase("")]
    [TestCase("\t\n")]
    public async Task ConsonantsNumbersAndPunctuationStayIntact(string input)
    {
        await WithEntity((system, entity) =>
            Assert.That(system.Accentuate(input, entity), Is.EqualTo(input)));
    }

    [Test]
    public async Task VowelsKeepTheirLettersAndCase()
    {
        await WithEntity((system, entity) =>
        {
            Assert.That(system.Accentuate("aäoöuü", entity),
                Does.Match("^a{1,2}ä{1,2}o{1,2}ö{1,2}u{1,2}ü{1,2}$"));
            Assert.That(system.Accentuate("AÄOÖUÜ", entity),
                Does.Match("^A{1,2}Ä{1,2}O{1,2}Ö{1,2}U{1,2}Ü{1,2}$"));
            Assert.That(system.Accentuate("sie", entity), Does.Match("^si{1,2}e{1,2}$"));
            Assert.That(system.Accentuate("bitte", entity), Does.Match("^bi{1,2}tt(e{1,2}|')$"));
        });
    }

    [Test]
    public async Task MissingAccentEntityLeavesSpeechUnchanged()
    {
        await WithEntity((system, _) =>
            Assert.That(system.Accentuate("Ich brauche Hilfe!"), Is.EqualTo("Ich brauche Hilfe!")));
    }

    [Test]
    public async Task GermanInterjectionsAreLocalized()
    {
        await WithEntity((_, _) =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            Assert.That(loc.GetString("slur-accent-confused"), Is.EqualTo("...häääh..."));
            Assert.That(loc.GetString("slur-accent-burp"), Is.EqualTo("*RÜLPS*."));
        });
    }

    private async Task WithEntity(Action<SlurredSystem, Entity<SlurredAccentComponent>> assertion)
    {
        await Pair.Server.WaitAssertion(() =>
        {
            var loc = Pair.Server.ResolveDependency<ILocalizationManager>();
            var previous = loc.DefaultCulture;
            var entities = Pair.Server.ResolveDependency<IEntityManager>();
            var uid = entities.SpawnEntity(null, MapCoordinates.Nullspace);
            try
            {
                loc.SetCulture(CultureInfo.GetCultureInfo("de-DE"));
                var component = entities.AddComponent<SlurredAccentComponent>(uid);
                var system = Pair.Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SlurredSystem>();
                assertion(system, new Entity<SlurredAccentComponent>(uid, component));
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
