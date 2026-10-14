using Content.Shared.Chat;
using NUnit.Framework;
using Robust.Shared.GameObjects;

namespace Content.Tests.Shared.Chat;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public sealed class CodewordHighlightTest
{
    [TestCase("rot", "rot", "[color=red]rot[/color]")]
    [TestCase("Rot", "rot", "[color=red]Rot[/color]")]
    [TestCase("ROT", "rot", "[color=red]ROT[/color]")]
    [TestCase("rot.", "rot", "[color=red]rot[/color].")]
    [TestCase("rot,", "rot", "[color=red]rot[/color],")]
    [TestCase("rot!", "rot", "[color=red]rot[/color]!")]
    [TestCase("rot?", "rot", "[color=red]rot[/color]?")]
    [TestCase("(rot)", "rot", "([color=red]rot[/color])")]
    [TestCase("„rot“", "rot", "„[color=red]rot[/color]“")]
    [TestCase("Das ist rot, nicht blau.", "rot", "Das ist [color=red]rot[/color], nicht blau.")]
    [TestCase("rot, rot.", "rot", "[color=red]rot[/color], [color=red]rot[/color].")]
    [TestCase("Brot", "rot", "Brot")]
    [TestCase("rote roten roter", "rot", "rote roten roter")]
    [TestCase("rot2 2rot rot_blau", "rot", "rot2 2rot rot_blau")]
    [TestCase("ärot rotä rot²", "rot", "ärot rotä rot²")]
    [TestCase("rot\u0308", "rot", "rot\u0308")]
    [TestCase("läuft", "laufen", "läuft")]
    [TestCase("MÜDE!", "müde", "[color=red]MÜDE[/color]!")]
    [TestCase("müden", "müde", "müden")]
    [TestCase("Ski fahren.", "Ski fahren", "[color=red]Ski fahren[/color].")]
    [TestCase("a.b axb", "a.b", "[color=red]a.b[/color] axb")]
    [TestCase("[color=rot]rot[/color]", "rot", "[color=rot][color=red]rot[/color][/color]")]
    [TestCase("giessen", "gießen", "[color=red]giessen[/color]")]
    [TestCase("Giessen.", "gießen", "[color=red]Giessen[/color].")]
    [TestCase("GIESSEN,", "gießen", "[color=red]GIESSEN[/color],")]
    [TestCase("gießen!", "gießen", "[color=red]gießen[/color]!")]
    [TestCase("heiss", "heiß", "[color=red]heiss[/color]")]
    [TestCase("HEISS", "heiß", "[color=red]HEISS[/color]")]
    [TestCase("HEIẞ", "heiß", "[color=red]HEIẞ[/color]")]
    [TestCase("heiss", "HEIẞ", "[color=red]heiss[/color]")]
    [TestCase("vergiessen giessend giessen2", "gießen", "vergiessen giessend giessen2")]
    [TestCase("heissen", "heiß", "heissen")]
    [TestCase("wißen", "wissen", "wißen")]
    [TestCase("rot", "", "rot")]
    public void MatchesOnlyCompleteLiteralCodewords(string input, string codeword, string expected)
    {
        var message = new ChatMessage(ChatChannel.Local, input, input, NetEntity.Invalid, null);
        Assert.That(SharedChatSystem.InjectTagAroundString(message, codeword, "color", "red", wholeWord: true),
            Is.EqualTo(expected));
    }

    [Test]
    public void OrdinaryHighlightsKeepSubstringMatching()
    {
        const string input = "Brot";
        var message = new ChatMessage(ChatChannel.Local, input, input, NetEntity.Invalid, null);
        Assert.That(SharedChatSystem.InjectTagAroundString(message, "rot", "color", "red"),
            Is.EqualTo("B[color=red]rot[/color]"));
    }
}
