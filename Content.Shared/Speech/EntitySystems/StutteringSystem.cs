using System.Text;
using System.Text.RegularExpressions;
using Content.Shared.Random.Helpers;
using Content.Shared.Speech.Components;
using Content.Shared.StatusEffectNew;
using JetBrains.Annotations;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Timing;

namespace Content.Shared.Speech.EntitySystems;

public sealed partial class StutteringSystem : RelayAccentSystem<StutteringAccentComponent>
{
    public static readonly EntProtoId StutterEffect = "StatusEffectSlurred";

    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private StatusEffectsSystem _statusEffects = default!;

    // Regex of characters to stutter.
    private static readonly Regex Stutter = new("[b-df-hj-np-tv-wxyz]",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public override string Accentuate(string message, Entity<StutteringAccentComponent>? ent = null)
    {
        if (ent == null)
            return message;

        var random = SharedRandomExtensions.PredictedRandom(_timing, GetNetEntity(ent.Value));
        if (Loc.DefaultCulture?.TwoLetterISOLanguageName == "de")
            return AccentuateGerman(message, ent.Value, random);

        var length = message.Length;
        var finalMessage = new StringBuilder();

        for (var i = 0; i < length; i++)
        {
            var newLetter = message[i].ToString();
            if (Stutter.IsMatch(newLetter) && random.Prob(ent.Value.Comp.MatchRandomProb))
            {
                if (random.Prob(ent.Value.Comp.FourRandomProb))
                {
                    newLetter = $"{newLetter}-{newLetter}-{newLetter}-{newLetter}";
                }
                else if (random.Prob(ent.Value.Comp.ThreeRandomProb))
                {
                    newLetter = $"{newLetter}-{newLetter}-{newLetter}";
                }
                else if (random.Prob(ent.Value.Comp.CutRandomProb))
                {
                    newLetter = "";
                }
                else
                {
                    newLetter = $"{newLetter}-{newLetter}";
                }
            }

            finalMessage.Append(newLetter);
        }

        return finalMessage.ToString();
    }

    private static string AccentuateGerman(string message, Entity<StutteringAccentComponent> ent,
        IRobustRandom random)
    {
        var result = new StringBuilder();
        for (var i = 0; i < message.Length;)
        {
            var soundLength = GetGermanSoundLength(message, i);
            var sound = message.Substring(i, soundLength);
            i += soundLength;

            if (Stutter.IsMatch(sound) && random.Prob(ent.Comp.MatchRandomProb))
            {
                if (random.Prob(ent.Comp.FourRandomProb))
                    sound = string.Concat(new[] { sound, "-", sound, "-", sound, "-", sound });
                else if (random.Prob(ent.Comp.ThreeRandomProb))
                    sound = string.Concat(new[] { sound, "-", sound, "-", sound });
                else if (random.Prob(ent.Comp.CutRandomProb))
                    sound = "";
                else
                    sound = string.Concat(sound, "-", sound);
            }

            result.Append(sound);
        }

        return result.ToString();
    }

    private static int GetGermanSoundLength(string message, int index)
    {
        if (index + 2 < message.Length
            && char.ToLowerInvariant(message[index]) == 's'
            && char.ToLowerInvariant(message[index + 1]) == 'c'
            && char.ToLowerInvariant(message[index + 2]) == 'h')
            return 3;

        if (index + 1 < message.Length
            && char.ToLowerInvariant(message[index]) == 'c'
            && char.ToLowerInvariant(message[index + 1]) == 'h')
            return 2;

        return 1;
    }

    /// <summary>
    /// Applies or refreshes the stuttering speech status effect on an entity.
    /// </summary>
    [PublicAPI]
    public void DoStutter(EntityUid uid, TimeSpan time, bool refresh)
    {
        if (refresh)
            _statusEffects.TryUpdateStatusEffectDuration(uid, StutterEffect, time);
        else
            _statusEffects.TryAddStatusEffectDuration(uid, StutterEffect, time);
    }
}
