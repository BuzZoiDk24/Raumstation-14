using Content.Shared.Speech.Components;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class ScottishAccentSystem : RelayAccentSystem<ScottishAccentComponent>
{
    [Dependency] private ReplacementAccentSystem _replacement = default!;

    protected override Type[] AccentBefore => [typeof(StutteringSystem), typeof(SlurredSystem)];

    public override string Accentuate(string message, Entity<ScottishAccentComponent>? ent = null)
    {
        return _replacement.ApplyReplacements(message, "scottish", ent?.Owner);
    }
}
