using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QuickFix.Classes
{
    class GoogleTime
    {
        public async static Task<DateTime> GetCurrentTime() {
            using (var client = new HttpClient()) {
                try
                {
                    var result = await client.GetAsync("https://google.com",
                          HttpCompletionOption.ResponseHeadersRead);
                    return result.Headers.Date.Value.DateTime.AddHours(2); // add 2 hours of delay
                }
                catch {
                    // if ethernet isnt available return the local configured time
                    return DateTime.Now;
                }
            }
        }
    }
}
