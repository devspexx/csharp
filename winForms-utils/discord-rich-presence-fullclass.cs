using DiscordRPC;
using DiscordRPC.Logging;
using Spexx.Frontend.Forms;
using System;
using Button = DiscordRPC.Button;

namespace Spexx.Backend
{
    internal class DiscordHelper
    {
        #region AUTH AND VARIABLES

        public DiscordRpcClient client;

        public string CLIENT_ID = "your-client-id";
        public string PRESENCE_DETAILS = "Editing 'win32-wrapper.cs'";
        public string PRESENCE_STATE = "";
        public string ASSETS_LARGE_IMAGE_TEXT = "";
        public string ASSETS_SMALL_IMAGE_TEXT = "";
        public string ASSETS_LARGE_IMAGE_KEY = "main";
        public string ASSETS_SMALL_IMAGE_KEY = "";

        #endregion AUTH AND VARIABLES

        #region MAIN PROGRAM

        public void Initialize()
        {
            client = new DiscordRpcClient(CLIENT_ID);

            if (AppSettings.Functionality.DEBUG_MODE)
            {
                client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

                client.OnReady += (sender, e) =>
                {
                    Console.WriteLine("Received Ready from user {0}", e.User.Username);
                };
                client.OnPresenceUpdate += (sender, e) =>
                {
                    Console.WriteLine("Received Update! {0}", e.Presence);
                };
            }
            
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = PRESENCE_DETAILS,
                State = PRESENCE_STATE,

                Assets = new Assets()
                {
                    LargeImageKey = ASSETS_LARGE_IMAGE_KEY,
                    LargeImageText = ASSETS_LARGE_IMAGE_TEXT,
                    SmallImageKey = ASSETS_LARGE_IMAGE_KEY,
                    SmallImageText = ASSETS_SMALL_IMAGE_TEXT
                },

                Party = new Party()
                {
                    ID = Secrets.CreateFriendlySecret(new Random()),
                    Size = 1,
                    Max = 4,
                    Privacy = Party.PrivacySetting.Public
                },
                Buttons = new Button[] 
                { 
                    new Button() { Label = "https://spexx.dev/", Url = "https://spexx.dev/" }
                },

                Timestamps = Timestamps.Now
            });
            client.Invoke();
        }
        public void RefreshPresence()
        {
            client.SetPresence(new RichPresence()
            {
                Details = PRESENCE_DETAILS,
                Assets = new Assets()
                {
                    LargeImageKey = ASSETS_LARGE_IMAGE_KEY
                },
                Buttons = new DiscordRPC.Button[] {
                        new DiscordRPC.Button()
                        {
                            Label = "https://spexx.dev/",
                            Url = "https://www.spexx.dev/"
                        }
                    },
                Timestamps = Timestamps.Now
            });
            client.Invoke();
        }

        #endregion MAIN PROGRAM
    }
}