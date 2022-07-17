// use Microsoft.WindowsAPICodePack.Taskbar
// https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Core/
public struct Taskbar
{
    public static void TaskbarAddIconButton(IntPtr handle, Icon icon, string tooptiponhover)
    {
        TaskbarManager.Instance.ThumbnailToolBars.AddButtons(handle, new ThumbnailToolBarButton(icon, tooptiponhover));
    }
    public static void TaskbarSetAppOverlayIcon(IntPtr handle, Icon icon, string accessibilityText)
    {
        TaskbarManager.Instance.SetOverlayIcon(handle, Resources.app_icon_ico, accessibilityText);
    }
    public static void TaskbarSetAppPercentage(IntPtr handle, int percent, int maxpercent, TaskbarProgressBarState state)
    {
        TaskbarManager.Instance.SetProgressState(state, handle);
        TaskbarManager.Instance.SetProgressValue(percent, maxpercent, handle);
    }
}