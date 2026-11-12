using Content.Server.Objectives.Systems;
using Content.Shared.Roles;
using Robust.Shared.Prototypes;

namespace Content.Server.Objectives.Components;

/// <summary>
/// Objective has a target number of something.
/// Assignment uses a fixed job override when configured, otherwise rolls between the minimum and maximum.
/// </summary>
[RegisterComponent, Access(typeof(NumberObjectiveSystem))]
public sealed partial class NumberObjectiveComponent : Component
{
    /// <summary>
    /// Number to use in the objective condition.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public int Target;

    /// <summary>
    /// Minimum number for target to roll. (Inclusive)
    /// </summary>
    [DataField(required: true)]
    public int Min;

    /// <summary>
    /// Maximum number for target to roll. (Inclusive)
    /// </summary>
    [DataField(required: true)]
    public int Max;

    /// <summary>
    /// Optional fixed target counts for the assignee's actual job at assignment.
    /// Jobs without an override retain the normal Min/Max roll.
    /// </summary>
    [DataField]
    public Dictionary<ProtoId<JobPrototype>, int> JobTargets = new();

    /// <summary>
    /// Optional title locale id, passed "count" with <see cref="Target"/>.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public string? Title;

    /// <summary>
    /// Optional description locale id, passed "count" with <see cref="Target"/>.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public string? Description;
}
