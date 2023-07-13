using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Windows.Forms;


namespace Discord.Modules
{
    public static class Utility
    {
        public static Keys ConvertFromString(string keystr)
        {
            return (Keys)Enum.Parse(typeof(Keys), keystr);
        }

        public static bool IsKeyPressed(string keystr)
        {
            return (WindowAPIs.GetAsyncKeyState((int)ConvertFromString(keystr)) & 0x8000) != 0;
        }

        public static bool IsGameOnDisplay()
        {
            return WindowAPIs.GetActiveWindowTitle() == "League of Legends (TM) Client";
        }

        public static bool IsInPolygon(this Point point, IEnumerable<Point> polygon)
        {
            bool result = false;
            if (polygon.Count() > 0)
            {
                var a = polygon.Last();
                foreach (var b in polygon)
                {
                    if ((b.X == point.X) && (b.Y == point.Y))
                        return true;

                    if ((b.Y == a.Y) && (point.Y == a.Y) && (a.X <= point.X) && (point.X <= b.X))
                        return true;

                    if ((b.Y < point.Y) && (a.Y >= point.Y) || (a.Y < point.Y) && (b.Y >= point.Y))
                    {
                        if (b.X + (point.Y - b.Y) / (a.Y - b.Y) * (a.X - b.X) <= point.X)
                            result = !result;
                    }
                    a = b;
                }
            }

            return result;
        }

        public static Rectangle GetLeagueSize()
        {
            string className = "RiotWindowClass";
            string windowName = "League of Legends (TM) Client";

            Rectangle rect;
            IntPtr hwnd = WindowAPIs.FindWindow(className, windowName);

            WindowAPIs.GetWindowRect(hwnd, out rect);
            return rect;
        }

        public static string GetUniqueIdentifier()
        {
            var disk = new ManagementObject(@"win32_logicaldisk.deviceid=""c:""");
            disk.Get();

            string volumeSerial = disk["VolumeSerialNumber"].ToString();
            disk.Dispose();

            var managClass = new ManagementClass("win32_processor");
            var managCollec = managClass.GetInstances();

            foreach (ManagementObject managObj in managCollec)
            {
                //Get only the first CPU's ID
                return volumeSerial + managObj.Properties["processorID"].Value.ToString();
            }
            return string.Empty;
        }

    }
}
