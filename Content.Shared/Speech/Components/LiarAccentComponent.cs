using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.Speech.Components;

/// <summary>
/// The character's liar speech trait, independent of species and equipment accents.
/// </summary>
[RegisterComponent, NetworkedComponent]
[Access(typeof(LiarAccentSystem))]
public sealed partial class LiarAccentComponent : BaseAccentComponent;
