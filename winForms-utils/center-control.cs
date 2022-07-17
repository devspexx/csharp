public static void CenterControlInPanel(Control c, Size panel, bool top = true, bool left = true)
{
    if (top)
        c.Top = (panel.Height - c.Size.Height) / 2;
    if (left)
        c.Left = (panel.Width - c.Size.Width) / 2;
}