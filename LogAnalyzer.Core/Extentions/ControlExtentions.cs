using Avalonia.Controls;

namespace LogAnalyzer.Core.Extentions;

public static class ControlExtentions
{
    private static ContextMenu? _contextMenu;

    public static void OpenContextMenu(this Control control, MenuItem[] menuItems)
    {
        _contextMenu?.Close();
        _contextMenu?.Items.Clear();

        _contextMenu = new ContextMenu();

        foreach (MenuItem item in menuItems)
            _contextMenu.Items.Add(item);

        _contextMenu.Open(control);
    }

}