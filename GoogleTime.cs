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
                    return result.Headers.Date.Value.DateTime.AddHours(2); // dodaj 2 uri zaostanka
                }
                catch {
                    // ce ni interneta zacasno returnaj lokalni cajt
                    return DateTime.Now;
                }
            }
        }
    }
}
