using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.Speech.Components;

/// <summary>
/// The character's scottish speech trait, independent of species and equipment accents.
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(ScottishAccentSystem))]
public sealed partial class ScottishAccentComponent : BaseAccentComponent;
