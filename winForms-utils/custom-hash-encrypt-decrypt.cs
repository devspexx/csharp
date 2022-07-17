private static int processamount = 0;

private static string[] x = {
    "A|j", "B|)", "C|}", "D|1", "E|0","F|+",
    "G|t", "H|5","I|<", "J|y", "K|b","L|:",
    "M|g", "N|{","O|%", "P|#", "R|?","S|a",
    "T|c", "U|w","V|s", "Z|;", "X|e","Y|d",
    "W|l", "f|-",">|2", "6|=", "[|@","3|h",
    "v|m", "x|$","*|i", "n|o", "k|&",".|p",
    "u|]", "_|r","4|!", "8|/", "7|(","z|9",
    "ABCDEFGHIJKLMNOPRSTUVZXYWf>6[3vx*nk.u_487z"
};

/// <summary>
/// Method used to generate a passsword, max length is 75, 16 is default
/// </summary>
/// <returns>Returns a generated password in original format[0] and hashed[1]</returns>
public static string[] GeneratePassword(int length = 16)
{
    string original = string.Empty, hashed = "spexx@";
    Random r = new Random();
    for (int i = 0; i < length; i++)
    {
        int ind = r.Next(x.Length - 1);
        string indstr = x[ind];
        original += indstr[0];
        hashed += indstr[2];
    }
    return new string[] { original, hashed };
}

public static string UnHash(string toEncrypt)
{
    if (toEncrypt.Length == 0) return string.Empty;

    processamount++;

    string toReturn = toEncrypt.Substring(6, toEncrypt.Length - 6);
    string toReturnCompiled = string.Empty;
    string toReturnID = string.Empty;
    
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Reset(); stopwatch.Start();
    for (int a = 0; a < toReturn.Length; a++)
    {
        for (int b = 0; b < x.Length - 1; b++)
        {
            if (toReturn[a].Equals(x[b][2]))
            {
                toReturnCompiled += x[b][0];
                toReturnID += "[" + a + "|" + b + "]";

                Console.WriteLine(
                    "[-] processing process #" + processamount + 
                    ". details: a=" + a + ", b=" + b + ", switched=" + x[b][0] + " -> " + x[b][2]);
            }
        }
    }
    stopwatch.Stop();

    Console.WriteLine(
        "------------------------------------------------------------------\n" +
        "[x] process #" + processamount + " completed in " + stopwatch.ElapsedMilliseconds + "milliseconds.\n" +
        "[x] identifier: " + toReturnID + "\n" +
        "[x] input: " + toEncrypt + " § length=" + toEncrypt.Length + "\n" +
        "[x] output: " + toReturnCompiled + " § length=" + toReturnCompiled.Length +
        "\n------------------------------------------------------------------");

    return toReturnCompiled;
}