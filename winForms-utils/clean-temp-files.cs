// shell32 for trash bin 
enum RecycleFlags : uint
{
    SHERB_NOCONFIRMATION = 0x00000001,
    SHERB_NOPROGRESSUI = 0x00000002,
    SHERB_NOSOUND = 0x00000004
}
[DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

// defined temp files paths
string path = "C:/Users/" + Environment.UserName + "/AppData/Local/Temp";
string path2 = "C:/Windows/Temp";
string path3 = "C:/temp"; // optional

private async void DoStuff() {
  List<string> dirs = new List<string>();
  List<string> files = new List<string>();
            
  foreach (string dir in Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories))
  {
      dirs.Add(dir);
      label1.Text = "found dir: " + dir;
      foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
      {
          label1.Text = "found file: " + file;
          files.Add(file);
      }
      await Task.Delay(1);
  }

  foreach (string dir in Directory.GetDirectories(path2, "*.*", SearchOption.AllDirectories))
  {
      dirs.Add(dir);
      label1.Text = "found dir: " + dir;
      foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
      {
          label1.Text = "found file: " + file;
          files.Add(file);
      }
      await Task.Delay(1);
  }

  if (Directory.Exists(path3))
  {
      foreach (string dir in Directory.GetDirectories(path3, "*.*", SearchOption.AllDirectories))
      {
          dirs.Add(dir);
          label1.Text = "found dir: " + dir;
          foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
          {
              label1.Text = "found file: " + file;
              files.Add(file);
          }
          await Task.Delay(1);
      }
  }
  
  // delete dirs forcefully
  foreach (string dir in dirs)
  {
      progressBar1.Value++;
      label1.Text = "deleting: " + dir;

      try
      {
          Directory.Delete(dir, true);
      } catch (Exception) { }
      await Task.Delay(1);
  }
  
  // arg index 1 = If this value is an empty string or NULL, all Recycle Bins on all drives will be emptied.
  // arg index 2 = don't show progress reports/confirmation windows
  SHEmptyRecycleBin(Handle, null, RecycleFlags.SHERB_NOCONFIRMATION);
}
