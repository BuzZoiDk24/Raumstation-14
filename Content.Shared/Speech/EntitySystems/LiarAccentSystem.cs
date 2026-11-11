using Content.Shared.Speech.Components;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class LiarAccentSystem : RelayAccentSystem<LiarAccentComponent>
{
    [Dependency] private ReplacementAccentSystem _replacement = default!;

    // Change the meaning before dialects alter the words used by the liar rules.
    protected override Type[] AccentBefore =>
    [
        typeof(ScottishAccentSystem), typeof(GermanAccentSystem), typeof(SouthernAccentSystem),
        typeof(AustrianAccentSystem), typeof(FrenchAccentSystem), typeof(SpanishAccentSystem),
        typeof(RussianAccentSystem), typeof(ReplacementAccentSystem), typeof(StutteringSystem),
        typeof(SlurredSystem),
    ];

    public override string Accentuate(string message, Entity<LiarAccentComponent>? ent = null)
    {
        return _replacement.ApplyReplacements(message, "liar", ent?.Owner);
    }
}
