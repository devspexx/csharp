using Firebase.Storage;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Classes
{
    public class AccountUtils
    {
        public static int procents_upload;
        
        public static async Task<string[]> PrenesiDatotekoNaServer(string path, string uporabnik, bool profilnaSlika = false) {
            try {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var stream = File.Open(path, FileMode.Open);
                if (!profilnaSlika) {
                    var task = new FirebaseStorage(database_path)
                        .Child(uporabnik)
                        .Child(Path.GetFileName(path) + "|" + DateTime.Now.ToString("HH:mm:ss") + Path.GetExtension(path))
                        .PutAsync(stream);
                    task.Progress.ProgressChanged += (s, r) => procents_upload = r.Percentage;
                    var downloadurl = await task;
                    sw.Stop();
                    return new string[] { downloadurl, sw.Elapsed.TotalMilliseconds.ToString() };
                } else {
                    var task = new FirebaseStorage(database_path)
                        .Child(uporabnik)
                        .Child("profilePicture.png")
                        .PutAsync(stream);
                    task.Progress.ProgressChanged += (s, r) => procents_upload = r.Percentage;
                    var downloadurl = await task;
                    sw.Stop();
                    return new string[] { downloadurl, sw.Elapsed.TotalMilliseconds.ToString() };
                }
            }
            catch (Exception) {
                return null;
            }
        }
    }
}
