using System.Drawing;

namespace Spexx.Frontend.Forms
{
    internal class AppSettings
    {
        public struct Functionality
        {
            // if debug mode is enabled, ctrl+D will be accessible
            // debug mode can be / should ONLY be enabled via params -debug / -d
            public static bool DEBUG_MODE = false;

            // if SHOW_BORDERS is false:
            // BORDER_RESIZE_EANBLED will be too, app resizing will be disabled
            public static bool BORDER_RESIZE_EANBLED = true;
            public static bool SHOW_BORDERS = true;

            public static bool SHOW_HEADER = true;
            public static bool DISABLE_ALT_F4_PRESS = true;

            // Ctrl+R to reload the app
            public static bool ENABLE_APP_RESTART = true;

            public static bool ENABLE_DRAGGING = true;
            public static bool DETECT_CMD_LINE_ARGS = true;

            // may take up to 10 seconds to load
            public static bool LOAD_STATISTICS = false;

            // will ping the server and get roundtrip time in ms (milliseconds)
            // + onhover event with ms for global label "connecting.."
            public static bool PING_SERVER = true;
        }

        public struct Themes
        {
            public struct DARK
            {
                public static Color BORDER_COLOR_APP_ACTIVE = Color.FromArgb(35, 35, 35);
                public static Color BORDER_COLOR_APP_NOTACTIVE = Color.FromArgb(45, 45, 45);

                public static Color HEADER_BACKGROUND_ACTIVE = Color.White;
                public static Color HEADER_BACKGROUND_NOTACTIVE = Color.FromArgb(245, 245, 245);

                public static Color HEADER_BUTTONS_BACKGROUND_ACTIVE = Color.FromArgb(245, 245, 245);
                public static Color HEADER_BUTTONS_BACKGROUND_NOTACTIVE = Color.White;
            }

            public struct GLOBAL
            {
                public static int BORDER_SIZE_PX = 1;

                public static bool CUSTOM_CARET_BLINK_TIME = true;
                public static uint CARET_BLINK_TIME = 9999;
            }
        }
    }
}
