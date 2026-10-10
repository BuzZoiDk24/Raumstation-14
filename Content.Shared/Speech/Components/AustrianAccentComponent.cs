using Content.Shared.Speech.EntitySystems;
using Robust.Shared.GameStates;

namespace Content.Shared.Speech.Components;

[RegisterComponent, NetworkedComponent]
[Access(typeof(AustrianAccentSystem))]
public sealed partial class AustrianAccentComponent : BaseAccentComponent;
