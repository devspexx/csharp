public async static Task<DateTime> GetCurrentTime()
{
    HttpClient httpClient = new HttpClient();
    try {
        var result = await httpClient.GetAsync("https://google.com",
                  HttpCompletionOption.ResponseHeadersRead);

        return result.Headers.Date.Value.DateTime;
    } catch {

        // if there's an exception return the local (system) time
        return DateTime.Now;
    }
}
