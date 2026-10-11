using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Content.Client.Botany.UI;

/// <summary>
/// Keeps botany list markers beside their text, including wrapped lines.
/// </summary>
internal static class BotanyTextList
{
    public static BoxContainer CreateRow(string markup)
    {
        var row = new BoxContainer
        {
            HorizontalExpand = true,
            Margin = new Thickness(4, 1),
        };
        var marker = new RichTextLabel
        {
            MinWidth = 14,
            VerticalAlignment = Control.VAlignment.Top,
        };
        marker.SetMessage(FormattedMessage.FromMarkupPermissive("[font size=11]•[/font]"), tagsAllowed: null);

        var text = new RichTextLabel { HorizontalExpand = true };
        text.SetMessage(FormattedMessage.FromMarkupPermissive($"[font size=11]{markup}[/font]"), tagsAllowed: null);
        row.AddChild(marker);
        row.AddChild(text);
        return row;
    }
}
