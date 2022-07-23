// https://www.nuget.org/packages/WindowsAPICodePack-Shell/

// using Microsoft.WindowsAPICodePack.Shell;
// using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

private string GetVideoDuration(string filePath)
{
    using (ShellObject shell = ShellObject.FromParsingName(filePath))
    {
        IShellProperty prop = shell.Properties.System.Media.Duration;
        string duration = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None);
        return duration;
    }
}
