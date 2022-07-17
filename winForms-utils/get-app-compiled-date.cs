public static string getExeLastCompiledDateTimeString()
{
    DateTime lastCompileDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
    return lastCompileDate.ToString("dd/MM/yyyy HH:mm:ss");
}
