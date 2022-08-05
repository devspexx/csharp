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
        //returns string[]
        // [0] = a direct download url
        // [1] = upload time took in ms
        public static async Task<string[]> UploadFile(string path, string user, bool isAProfilePicture = false) {
            
            // start the stopwatch to keep track of the upload progress
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            // ref variable for progress
            int uploadPercentage = 0;
            
            try {
                // store path of the file in var 
                var uploadedFile = File.Open(path, FileMode.Open);
                
                // create a task that will upload a picture in user path: user.<user>.profilePicture.png
                var task = new FirebaseStorage(database_path)
                    .Child(user)
                    .Child(isAProfilePicture ? 
                           Path.GetFileName(path) + "|" + DateTime.Now.ToString("HH:mm:ss") + Path.GetExtension(path) : 
                           "profilePicture.png")
                    .PutAsync(uploadedFile);
                
                // track progress percentage via public variable
                task.Progress.ProgressChanged += (s, r) => uploadPercentage = r.Percentage;
                
                // gives a download url
                var downloadurl = await task; 
               
                // stop the stopwatch
                sw.Stop();
                
                // return data
                return new string[] { downloadurl, sw.Elapsed.TotalMilliseconds.ToString() };
            }
            // return empty string if there's an exception thrown
            catch {
                sw.Stop();
                return new string[] { };
            }
        }
    }
}
