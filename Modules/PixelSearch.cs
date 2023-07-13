using System;
using System.Collections.Generic;
using System.Drawing;
using Discord.Mathematics;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Discord.Modules
{
    public static class PixelSearch
    {
        // Pre-Formatted LocalPlayer HPBar Colors
        private static Color RGB_LOCALPLAYER_HP_BAR_COLOUR = Color.FromArgb(44, 153, 38); // Original Local HP Bar

        // Formatted LocalPlayer HPBar Colors
        static int[] Formatted_LOCALPLAYERHP_Color = new int[3] { RGB_LOCALPLAYER_HP_BAR_COLOUR.B, RGB_LOCALPLAYER_HP_BAR_COLOUR.G, RGB_LOCALPLAYER_HP_BAR_COLOUR.R };

        // Pre-Formatted Enemy HPBar Colors
        private static Color RGB_ENEMY_HP_BAR_COLOR = Color.FromArgb(154, 38, 27); // Original Enemy HP Bar and For Percent Health Check & Count
        private static Color RGB_ENEMY_HPSECOND_BAR_COLOR = Color.FromArgb(206, 101, 90); // Original Enemy HP Bar and For Absolute Health Check
        private static Color RGB_CHECK_HP_ABSOLUTE_COLOR = Color.FromArgb(0, 0, 0); // Original Enemy HP Bar For Absolute Health Count

        // Formatted Enemy HPBar Colors
        static int[] Formatted_ENEMYHP_Color = new int[3] { RGB_ENEMY_HP_BAR_COLOR.B, RGB_ENEMY_HP_BAR_COLOR.G, RGB_ENEMY_HP_BAR_COLOR.R };
        static int[] Formatted_ENEMYHPSECOND_Color = new int[3] { RGB_ENEMY_HPSECOND_BAR_COLOR.B, RGB_ENEMY_HPSECOND_BAR_COLOR.G, RGB_ENEMY_HPSECOND_BAR_COLOR.R };
        static int[] Formatted_CHECKHP_ABSOLUTE_Color = new int[3] { RGB_CHECK_HP_ABSOLUTE_COLOR.B, RGB_CHECK_HP_ABSOLUTE_COLOR.G, RGB_CHECK_HP_ABSOLUTE_COLOR.R };

        // Pre-Formatted Verification HPBar Colors
        private static Color RGB_HPVerifyOriginal_BAR_COLOR = Color.FromArgb(8, 4, 8); // Verify Both Local and Enemy HP Bar in general
        private static Color RGB_HPVerifyMorganaE_BAR_COLOR = Color.FromArgb(16, 8, 16); // Verify Both Local and Enemy HP Bar for Morgana's E
        private static Color RGB_HPVerifyKayleR_BAR_COLOR = Color.FromArgb(8, 12, 8); // Verify Both Local HP Bar for Kayle's R

        // Formatted Verification HPBar Colors 
        static int[] Formatted_HPVerifyOriginal_Color = new int[3] { RGB_HPVerifyOriginal_BAR_COLOR.B, RGB_HPVerifyOriginal_BAR_COLOR.G, RGB_HPVerifyOriginal_BAR_COLOR.R };
        static int[] Formatted_HPVerifyMorganaE_Color = new int[3] { RGB_HPVerifyMorganaE_BAR_COLOR.B, RGB_HPVerifyMorganaE_BAR_COLOR.G, RGB_HPVerifyMorganaE_BAR_COLOR.R };
        static int[] Formatted_HPVerifyKayleR_Color = new int[3] { RGB_HPVerifyKayleR_BAR_COLOR.B, RGB_HPVerifyKayleR_BAR_COLOR.G, RGB_HPVerifyKayleR_BAR_COLOR.R };

        // Pre-Formatted Range Circle Colors
        private static Color _FROMCOLOR = Color.FromArgb(76, 244, 240); // ColorTranslator.FromHtml("#4cf4f0");
        private static Color _TOCOLOR = Color.FromArgb(112, 255, 255); // ColorTranslator.FromHtml("#98ffff");
        //private static Color _TOCOLOR = Color.FromArgb(152, 255, 255); // ColorTranslator.FromHtml("#98ffff");

        // Formatted Range Circle Colors
        static int[] Formatted_FROM_Color = new int[3] { _FROMCOLOR.B, _FROMCOLOR.G, _FROMCOLOR.R };
        static int[] Formatted_TO_Color = new int[3] { _TOCOLOR.B, _TOCOLOR.G, _TOCOLOR.R };

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Point SearchForNearestEnemy(Rectangle LeagueWindowRectangle, int Left, int Right, int Top, int Bottom) // Search For Nearest Enemy To Local
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            Point LocalPlayerPosition = Point.Empty;
            List<Point> AllEnemiesPosition = new List<Point>();
            List<Point> AttackRangeCirclePoints = new List<Point>();

            unsafe
            {
                for (int y = LeagueWindowRectangle.Y; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (!(x <= LeagueWindowRectangle.X + 265 && y <= LeagueWindowRectangle.Y + 125))
                        {
                            //// Check if it's localplayer POSITION
                            if (row[x * 3] >= Formatted_LOCALPLAYERHP_Color[0] & row[x * 3] <= Formatted_LOCALPLAYERHP_Color[0]) //blue
                                if (row[(x * 3) + 1] >= Formatted_LOCALPLAYERHP_Color[1] & row[(x * 3) + 1] <= Formatted_LOCALPLAYERHP_Color[1]) //green
                                    if (row[(x * 3) + 2] >= Formatted_LOCALPLAYERHP_Color[2] & row[(x * 3) + 2] <= Formatted_LOCALPLAYERHP_Color[2]) //red
                                    { // Confirm that it's localPlayer POSITION

                                        if ((row[(x - 1) * 3] >= Formatted_HPVerifyOriginal_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyOriginal_Color[0]) || (row[(x - 1) * 3] >= Formatted_HPVerifyMorganaE_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyMorganaE_Color[0]) || (row[(x - 1) * 3] >= Formatted_HPVerifyKayleR_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyKayleR_Color[0])) //blue
                                            if (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyOriginal_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyOriginal_Color[1] || (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyMorganaE_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyMorganaE_Color[1]) || (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyKayleR_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyKayleR_Color[1])) //green
                                                if (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyOriginal_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyOriginal_Color[2] || (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyMorganaE_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyMorganaE_Color[2]) || (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyKayleR_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyKayleR_Color[2])) //red
                                                {
                                                    LocalPlayerPosition = new Point(x + 43, y + 106);
                                                }
                                    }

                            ////// Check if it's enemy POSITION for Nearest To Local
                            if (row[x * 3] >= Formatted_ENEMYHP_Color[0] & row[x * 3] <= Formatted_ENEMYHP_Color[0]) //blue
                                if (row[(x * 3) + 1] >= Formatted_ENEMYHP_Color[1] & row[(x * 3) + 1] <= Formatted_ENEMYHP_Color[1]) //green
                                    if (row[(x * 3) + 2] >= Formatted_ENEMYHP_Color[2] & row[(x * 3) + 2] <= Formatted_ENEMYHP_Color[2]) //red
                                    { // Confirm that it's enemy POSITION
                                        if ((row[(x - 1) * 3] >= Formatted_HPVerifyOriginal_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyOriginal_Color[0]) || (row[(x - 1) * 3] >= Formatted_HPVerifyMorganaE_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyMorganaE_Color[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_HPVerifyOriginal_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyOriginal_Color[1]) || (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyMorganaE_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyMorganaE_Color[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_HPVerifyOriginal_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyOriginal_Color[2]) || (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyMorganaE_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyMorganaE_Color[2])) //red
                                                {
                                                    Point enemyPosition = new Point(x + 43, y + 106);
                                                    AllEnemiesPosition.Add(enemyPosition);
                                                }
                                    }

                            ////// Check if it's range circle
                            if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                                if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                    if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                    {
                                        AttackRangeCirclePoints.Add(new Point(x, y));
                                    }
                        }
                    }
                }
            }

            Dictionary<Point, double> EnemiesDistanceBetweenLocalPlayer = new Dictionary<Point, double>();

            foreach (var Enemy in AllEnemiesPosition)
            {
                if (LocalPlayerPosition != Point.Empty)
                {
                    var distanceBetweenEnemyAndLocalPlayer_2D = Maths.CheckDistanceBetween2Points(Enemy.X, Enemy.Y, LocalPlayerPosition.X, LocalPlayerPosition.Y) * 100000 / 100000;

                    EnemiesDistanceBetweenLocalPlayer.Add(Enemy, distanceBetweenEnemyAndLocalPlayer_2D);
                }
            }


            // Check to make sure there is at least 1 and verify which enemy is nearest to localplayer
            if (EnemiesDistanceBetweenLocalPlayer.Count > 0)
            {
                Point ClosestEnemyToLocal = Point.Empty;
                double ClosestDistanceToLocal = 9999;

                foreach (KeyValuePair<Point, double> enemyPosition in EnemiesDistanceBetweenLocalPlayer)
                {
                    Point[] Hitboxes = new Point[4];

                    Hitboxes[0] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y - Top); // Top Left
                    Hitboxes[1] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y - Top); // Top Right
                    Hitboxes[2] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y + Bottom); // Bottom Left
                    Hitboxes[3] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y + Bottom); // Bottom Right

                    foreach (var HitboxPoint in Hitboxes)
                    {
                        if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                        {
                            if (enemyPosition.Value < ClosestDistanceToLocal)
                            {
                                ClosestEnemyToLocal = enemyPosition.Key;
                                ClosestDistanceToLocal = enemyPosition.Value;
                            }
                            break;
                        }
                    }
                }

                GetScreenShot.Dispose();
                return ClosestEnemyToLocal;
            }

            GetScreenShot.Dispose();
            return Point.Empty;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Point SearchForNearestEnemyToMouse(Rectangle LeagueWindowRectangle, int Left, int Right, int Top, int Bottom) // Search For Nearest Enemy To Mouse
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            List<Point> AllEnemiesPosition = new List<Point>();
            List<Point> AttackRangeCirclePoints = new List<Point>();

            unsafe
            {
                for (int y = LeagueWindowRectangle.Y; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (!(x <= LeagueWindowRectangle.X + 265 && y <= LeagueWindowRectangle.Y + 125))
                        {
                            ////// Check if it's enemy POSITION for Nearest To Local
                            if (row[x * 3] >= Formatted_ENEMYHP_Color[0] & row[x * 3] <= Formatted_ENEMYHP_Color[0]) //blue
                                if (row[(x * 3) + 1] >= Formatted_ENEMYHP_Color[1] & row[(x * 3) + 1] <= Formatted_ENEMYHP_Color[1]) //green
                                    if (row[(x * 3) + 2] >= Formatted_ENEMYHP_Color[2] & row[(x * 3) + 2] <= Formatted_ENEMYHP_Color[2]) //red
                                    { // Confirm that it's enemy POSITION
                                        if ((row[(x - 1) * 3] >= Formatted_HPVerifyOriginal_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyOriginal_Color[0]) || (row[(x - 1) * 3] >= Formatted_HPVerifyMorganaE_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyMorganaE_Color[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_HPVerifyOriginal_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyOriginal_Color[1]) || (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyMorganaE_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyMorganaE_Color[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_HPVerifyOriginal_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyOriginal_Color[2]) || (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyMorganaE_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyMorganaE_Color[2])) //red
                                                {
                                                    Point enemyPosition = new Point(x + 43, y + 106);
                                                    AllEnemiesPosition.Add(enemyPosition);
                                                }
                                    }

                            ////// Check if it's range circle
                            if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                                if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                    if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                    {
                                        AttackRangeCirclePoints.Add(new Point(x, y));
                                    }
                        }
                    }
                }
            }

            Dictionary<Point, double> EnemiesDistanceBetweenMouse = new Dictionary<Point, double>();

            foreach (var Enemy in AllEnemiesPosition)
            {
                var distanceBetweenEnemyAndMouse_2D = Maths.CheckDistanceBetween2Points(Enemy.X, Enemy.Y, Cursor.Position.X, Cursor.Position.Y) * 100000 / 100000;

                EnemiesDistanceBetweenMouse.Add(Enemy, distanceBetweenEnemyAndMouse_2D);
            }

            // Check to make sure there is at least 1 and verify which enemy is nearest to mouse
            if (EnemiesDistanceBetweenMouse.Count > 0)
            {
                Point ClosestEnemyToMouse = Point.Empty;
                double ClosestDistanceToMouse = 9999;

                foreach (KeyValuePair<Point, double> enemyPosition in EnemiesDistanceBetweenMouse)
                {
                    Point[] Hitboxes = new Point[4];

                    Hitboxes[0] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y - Top); // Top Left
                    Hitboxes[1] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y - Top); // Top Right
                    Hitboxes[2] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y + Bottom); // Bottom Left
                    Hitboxes[3] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y + Bottom); // Bottom Right

                    foreach (var HitboxPoint in Hitboxes)
                    {
                        if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                        {
                            if (enemyPosition.Value < ClosestDistanceToMouse)
                            {
                                ClosestEnemyToMouse = enemyPosition.Key;
                                ClosestDistanceToMouse = enemyPosition.Value;
                            }
                            break;
                        }
                    }

                }
                GetScreenShot.Dispose();
                return ClosestEnemyToMouse;
            }

            GetScreenShot.Dispose();
            return Point.Empty;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Point SearchForLowestPercentHP(Rectangle LeagueWindowRectangle, int Left, int Right, int Top, int Bottom) // Search For Lowest Percent HP
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            List<Point> AttackRangeCirclePoints = new List<Point>();
            Dictionary<Point, double> EnemiesPositionHealthPercent = new Dictionary<Point, double>();

            unsafe
            {
                for (int y = LeagueWindowRectangle.Y; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (!(x <= LeagueWindowRectangle.X + 265 && y <= LeagueWindowRectangle.Y + 125))
                        {
                            ////// Check if it's enemy POSITION for HP Percent
                            if (row[x * 3] >= Formatted_ENEMYHP_Color[0] & row[x * 3] <= Formatted_ENEMYHP_Color[0]) //blue
                                if (row[(x * 3) + 1] >= Formatted_ENEMYHP_Color[1] & row[(x * 3) + 1] <= Formatted_ENEMYHP_Color[1]) //green
                                    if (row[(x * 3) + 2] >= Formatted_ENEMYHP_Color[2] & row[(x * 3) + 2] <= Formatted_ENEMYHP_Color[2]) //red
                                    { // Confirm that it's enemy POSITION
                                        if ((row[(x - 1) * 3] >= Formatted_HPVerifyOriginal_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyOriginal_Color[0]) || (row[(x - 1) * 3] >= Formatted_HPVerifyMorganaE_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyMorganaE_Color[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_HPVerifyOriginal_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyOriginal_Color[1]) || (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyMorganaE_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyMorganaE_Color[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_HPVerifyOriginal_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyOriginal_Color[2]) || (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyMorganaE_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyMorganaE_Color[2])) //red
                                                {
                                                    Point enemyPosition = new Point(x + 43, y + 106);

                                                    double redBarCount = 0;

                                                    for (int newX = x; newX < x + 105; newX++)
                                                    {
                                                        if (row[newX * 3] >= Formatted_ENEMYHP_Color[0] & row[newX * 3] <= Formatted_ENEMYHP_Color[0]) //blue
                                                            if (row[(newX * 3) + 1] >= Formatted_ENEMYHP_Color[1] & row[(newX * 3) + 1] <= Formatted_ENEMYHP_Color[1]) //green
                                                                if (row[(newX * 3) + 2] >= Formatted_ENEMYHP_Color[2] & row[(newX * 3) + 2] <= Formatted_ENEMYHP_Color[2]) //red
                                                                {
                                                                    redBarCount++;
                                                                }

                                                    }
                                                    double finalRedCountPercent = redBarCount / 105;
                                                    EnemiesPositionHealthPercent.Add(enemyPosition, finalRedCountPercent);
                                                }
                                    }

                            ////// Check if it's range circle
                            if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                                if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                    if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                    {
                                        AttackRangeCirclePoints.Add(new Point(x, y));
                                    }
                        }
                    }
                }
            }

            // Check to make sure there is at least 1 and verify which enemy is nearest to mouse
            if (EnemiesPositionHealthPercent.Count > 0)
            {
                Point LowestPercentHealthPlayerPoint = Point.Empty;
                double LowestHealthPercent = 9999;

                foreach (KeyValuePair<Point, double> enemyPosition in EnemiesPositionHealthPercent)
                {
                    Point[] Hitboxes = new Point[4];

                    Hitboxes[0] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y - Top); // Top Left
                    Hitboxes[1] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y - Top); // Top Right
                    Hitboxes[2] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y + Bottom); // Bottom Left
                    Hitboxes[3] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y + Bottom); // Bottom Right

                    foreach (var HitboxPoint in Hitboxes)
                    {
                        if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                        {
                            if (enemyPosition.Value < LowestHealthPercent)
                            {
                                LowestPercentHealthPlayerPoint = enemyPosition.Key;
                                LowestHealthPercent = enemyPosition.Value;
                            }
                            break;
                        }
                    }

                }
                GetScreenShot.Dispose();
                return LowestPercentHealthPlayerPoint;
            }

            GetScreenShot.Dispose();
            return Point.Empty;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Point SearchForLowestAbsoluteHP(Rectangle LeagueWindowRectangle, int Left, int Right, int Top, int Bottom) // Search For Lowest Absolute HP
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            List<Point> AttackRangeCirclePoints = new List<Point>();
            Dictionary<Point, double> EnemiesPositionHealthAbsolute = new Dictionary<Point, double>();

            unsafe
            {
                for (int y = LeagueWindowRectangle.Y; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (!(x <= LeagueWindowRectangle.X + 265 && y <= LeagueWindowRectangle.Y + 125))
                        {
                            //// Check if it's enemy POSITION for HP Absolute
                            if (row[x * 3] >= Formatted_ENEMYHPSECOND_Color[0] & row[x * 3] <= Formatted_ENEMYHPSECOND_Color[0]) //blue
                                if (row[(x * 3) + 1] >= Formatted_ENEMYHPSECOND_Color[1] & row[(x * 3) + 1] <= Formatted_ENEMYHPSECOND_Color[1]) //green
                                    if (row[(x * 3) + 2] >= Formatted_ENEMYHPSECOND_Color[2] & row[(x * 3) + 2] <= Formatted_ENEMYHPSECOND_Color[2]) //red
                                    { // Confirm that it's enemy POSITION
                                        if ((row[(x - 1) * 3] >= Formatted_HPVerifyOriginal_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyOriginal_Color[0]) || (row[(x - 1) * 3] >= Formatted_HPVerifyMorganaE_Color[0] & row[(x - 1) * 3] <= Formatted_HPVerifyMorganaE_Color[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_HPVerifyOriginal_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyOriginal_Color[1]) || (row[((x - 1) * 3) + 1] >= Formatted_HPVerifyMorganaE_Color[1] & row[((x - 1) * 3) + 1] <= Formatted_HPVerifyMorganaE_Color[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_HPVerifyOriginal_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyOriginal_Color[2]) || (row[((x - 1) * 3) + 2] >= Formatted_HPVerifyMorganaE_Color[2] & row[((x - 1) * 3) + 2] <= Formatted_HPVerifyMorganaE_Color[2])) //red
                                                {
                                                    Point enemyPosition = new Point(x + 43, y + 115);

                                                    double blackBarCountHealth = 1;
                                                    double redHPCount = 1;

                                                    for (int newX = x; newX < x + 105; newX++)
                                                    {
                                                        if ((row[newX * 3] >= Formatted_CHECKHP_ABSOLUTE_Color[0] & row[newX * 3] <= Formatted_CHECKHP_ABSOLUTE_Color[0])) //blue
                                                            if ((row[(newX * 3) + 1] >= Formatted_CHECKHP_ABSOLUTE_Color[1] & row[(newX * 3) + 1] <= Formatted_CHECKHP_ABSOLUTE_Color[1])) //green
                                                                if ((row[(newX * 3) + 2] >= Formatted_CHECKHP_ABSOLUTE_Color[2] & row[(newX * 3) + 2] <= Formatted_CHECKHP_ABSOLUTE_Color[2])) //red
                                                                {
                                                                    blackBarCountHealth++;

                                                                    if (row[(newX + 1) * 3] >= Formatted_CHECKHP_ABSOLUTE_Color[0] & row[(newX + 1) * 3] <= Formatted_CHECKHP_ABSOLUTE_Color[0]) //blue
                                                                        if (row[((newX + 1) * 3) + 1] >= Formatted_CHECKHP_ABSOLUTE_Color[1] & row[((newX + 1) * 3) + 1] <= Formatted_CHECKHP_ABSOLUTE_Color[1]) //green
                                                                            if (row[((newX + 1) * 3) + 2] >= Formatted_CHECKHP_ABSOLUTE_Color[2] & row[((newX + 1) * 3) + 2] <= Formatted_CHECKHP_ABSOLUTE_Color[2]) //red
                                                                            {
                                                                                blackBarCountHealth--;
                                                                            }
                                                                }

                                                        if (!(row[newX * 3] >= Formatted_CHECKHP_ABSOLUTE_Color[0] & row[newX * 3] <= Formatted_CHECKHP_ABSOLUTE_Color[0])) //blue
                                                            if (!(row[(newX * 3) + 1] >= Formatted_CHECKHP_ABSOLUTE_Color[1] & row[(newX * 3) + 1] <= Formatted_CHECKHP_ABSOLUTE_Color[1])) //green
                                                                if (!(row[(newX * 3) + 2] >= Formatted_CHECKHP_ABSOLUTE_Color[2] & row[(newX * 3) + 2] <= Formatted_CHECKHP_ABSOLUTE_Color[2])) //red
                                                                {
                                                                    redHPCount++;
                                                                }
                                                    }
                                                    double splitRedHP = Math.Round(redHPCount / blackBarCountHealth, 0);
                                                    double finalHealthAbsolute = blackBarCountHealth * redHPCount;
                                                    EnemiesPositionHealthAbsolute.Add(enemyPosition, finalHealthAbsolute);
                                                }
                                    }

                            ////// Check if it's range circle
                            if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                                if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                    if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                    {
                                        AttackRangeCirclePoints.Add(new Point(x, y));
                                    }
                        }
                    }
                }

            }

            // Check to make sure there is at least 1 and verify which enemy is nearest to mouse
            if (EnemiesPositionHealthAbsolute.Count > 0)
            {
                Point LowestAbsoluteHealthPlayerPoint = Point.Empty;
                double LowestHealthAbsolute = 9999;

                foreach (KeyValuePair<Point, double> enemyPosition in EnemiesPositionHealthAbsolute)
                {
                    Point[] Hitboxes = new Point[4];

                    Hitboxes[0] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y - Top); // Top Left
                    Hitboxes[1] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y - Top); // Top Right
                    Hitboxes[2] = new Point(enemyPosition.Key.X - Left, enemyPosition.Key.Y + Bottom); // Bottom Left
                    Hitboxes[3] = new Point(enemyPosition.Key.X + Right, enemyPosition.Key.Y + Bottom); // Bottom Right

                    //Hitboxes[0] = new Point(enemyPosition.Key.X - 30, enemyPosition.Key.Y - 5); // Top Left
                    //Hitboxes[1] = new Point(enemyPosition.Key.X + 30, enemyPosition.Key.Y - 5); // Top Right
                    //Hitboxes[2] = new Point(enemyPosition.Key.X - 40, enemyPosition.Key.Y + 20); // Bottom Left
                    //Hitboxes[3] = new Point(enemyPosition.Key.X + 40, enemyPosition.Key.Y + 20); // Bottom Right

                    foreach (var HitboxPoint in Hitboxes)
                    {
                        if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                        {
                            if (enemyPosition.Value < LowestHealthAbsolute)
                            {
                                LowestAbsoluteHealthPlayerPoint = enemyPosition.Key;
                                LowestHealthAbsolute = enemyPosition.Value;
                            }
                            break;
                        }
                    }

                }

                GetScreenShot.Dispose();
                return LowestAbsoluteHealthPlayerPoint;
            }
            GetScreenShot.Dispose();
            return Point.Empty;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Pre-Formatted Range Circle Colors
        private static Color RGB_ENEMY_SMALLMINION_HP_COLOR = Color.FromArgb(208, 94, 94);
        private static Color RGB_ENEMY_SMALLMINIONVerify_HP_COLOR = Color.FromArgb(12, 11, 13);
        private static Color RGB_ENEMY_SMALLMINIONVerify2_HP_COLOR = Color.FromArgb(12, 11, 14);
        private static Color RGB_ENEMY_SMALLMINIONVerify3_HP_COLOR = Color.FromArgb(13, 11, 14);
        private static Color RGB_ENEMY_SMALLMINIONVerify4_HP_COLOR = Color.FromArgb(11, 11, 13);

        // Formatted Range Circle Colors
        static int[] Formatted_ENEMY_SMALLMINION_HP_COLOR = new int[3] { RGB_ENEMY_SMALLMINION_HP_COLOR.B, RGB_ENEMY_SMALLMINION_HP_COLOR.G, RGB_ENEMY_SMALLMINION_HP_COLOR.R };
        static int[] Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR = new int[3] { RGB_ENEMY_SMALLMINIONVerify_HP_COLOR.B, RGB_ENEMY_SMALLMINIONVerify_HP_COLOR.G, RGB_ENEMY_SMALLMINIONVerify_HP_COLOR.R };
        static int[] Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR = new int[3] { RGB_ENEMY_SMALLMINIONVerify2_HP_COLOR.B, RGB_ENEMY_SMALLMINIONVerify2_HP_COLOR.G, RGB_ENEMY_SMALLMINIONVerify2_HP_COLOR.R };
        static int[] Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR = new int[3] { RGB_ENEMY_SMALLMINIONVerify3_HP_COLOR.B, RGB_ENEMY_SMALLMINIONVerify3_HP_COLOR.G, RGB_ENEMY_SMALLMINIONVerify3_HP_COLOR.R };
        static int[] Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR = new int[3] { RGB_ENEMY_SMALLMINIONVerify4_HP_COLOR.B, RGB_ENEMY_SMALLMINIONVerify4_HP_COLOR.G, RGB_ENEMY_SMALLMINIONVerify4_HP_COLOR.R };

        public static Point SearchForLastHitMinion(Rectangle LeagueWindowRectangle)
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            List<Point> AttackRangeCirclePoints = new List<Point>();
            Dictionary<Point, double> MinionPositionHealthPercent = new Dictionary<Point, double>();

            unsafe
            {
                for (int y = LeagueWindowRectangle.Y + 80; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X + 250; x < RegionIn_BitmapData.Width; x++)
                    {
                        ////// Check if it's enemy small minion HP
                        if (row[x * 3] >= (Formatted_ENEMY_SMALLMINION_HP_COLOR[0]) & row[x * 3] <= (Formatted_ENEMY_SMALLMINION_HP_COLOR[0])) //blue
                            if (row[(x * 3) + 1] >= (Formatted_ENEMY_SMALLMINION_HP_COLOR[1]) & row[(x * 3) + 1] <= (Formatted_ENEMY_SMALLMINION_HP_COLOR[1])) //green
                                if (row[(x * 3) + 2] >= (Formatted_ENEMY_SMALLMINION_HP_COLOR[2]) & row[(x * 3) + 2] <= (Formatted_ENEMY_SMALLMINION_HP_COLOR[2])) //red
                                { // Confirm that it's enemy small minion POSITION
                                    if ((row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[0])) //blue
                                        if ((row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[1])) //green
                                            if ((row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[2])) //red
                                            {
                                                Point minionPosition = new Point(x + 20, y + 40);

                                                double redBarCount = 0;

                                                for (int newX = x; newX < x + 60; newX++)
                                                {
                                                    if (row[newX * 3] >= Formatted_ENEMY_SMALLMINION_HP_COLOR[0] & row[newX * 3] <= Formatted_ENEMY_SMALLMINION_HP_COLOR[0]) //blue
                                                        if (row[(newX * 3) + 1] >= Formatted_ENEMY_SMALLMINION_HP_COLOR[1] & row[(newX * 3) + 1] <= Formatted_ENEMY_SMALLMINION_HP_COLOR[1]) //green
                                                            if (row[(newX * 3) + 2] >= Formatted_ENEMY_SMALLMINION_HP_COLOR[2] & row[(newX * 3) + 2] <= Formatted_ENEMY_SMALLMINION_HP_COLOR[2]) //red
                                                            {
                                                                redBarCount++;
                                                            }

                                                }
                                                double finalRedCountPercent = redBarCount / 60;

                                                if (finalRedCountPercent < 0.16)
                                                {
                                                    MinionPositionHealthPercent.Add(minionPosition, finalRedCountPercent);
                                                }
                                            }
                                }

                        ////// Check if it's range circle
                        if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                            if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                {
                                    AttackRangeCirclePoints.Add(new Point(x, y));
                                }
                    }

                }

                // Check to make sure there is at least 1 and verify which enemy is nearest to mouse
                if (MinionPositionHealthPercent.Count > 0)
                {
                    Point LowestPercentHealthMinionPoint = Point.Empty;
                    double LowestHealthPercent = 9999;

                    foreach (KeyValuePair<Point, double> enemyPosition in MinionPositionHealthPercent)
                    {
                        Point[] Hitboxes = new Point[4];

                        Hitboxes[0] = new Point(enemyPosition.Key.X - 10, enemyPosition.Key.Y);
                        Hitboxes[1] = new Point(enemyPosition.Key.X, enemyPosition.Key.Y - 5);
                        Hitboxes[2] = new Point(enemyPosition.Key.X + 10, enemyPosition.Key.Y);
                        Hitboxes[3] = new Point(enemyPosition.Key.X, enemyPosition.Key.Y + 20);

                        foreach (var HitboxPoint in Hitboxes)
                        {
                            if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                            {
                                if (enemyPosition.Value < LowestHealthPercent)
                                {
                                    LowestPercentHealthMinionPoint = enemyPosition.Key;
                                    LowestHealthPercent = enemyPosition.Value;
                                }
                                break;
                            }
                        }

                    }
                    GetScreenShot.Dispose();
                    return LowestPercentHealthMinionPoint;
                }

                GetScreenShot.Dispose();
                return Point.Empty;
            }
        }

        public static Point SearchForLaneClearMinions(Rectangle LeagueWindowRectangle)
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            List<Point> AttackRangeCirclePoints = new List<Point>();
            Dictionary<Point, double> MinionPositionHealthPercent = new Dictionary<Point, double>();

            unsafe
            {
                for (int y = LeagueWindowRectangle.Y + 80; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X + 250; x < RegionIn_BitmapData.Width; x++)
                    {
                        ////// Check if it's enemy small minion HP
                        if (row[x * 3] >= (Formatted_ENEMY_SMALLMINION_HP_COLOR[0]) & row[x * 3] <= (Formatted_ENEMY_SMALLMINION_HP_COLOR[0])) //blue
                            if (row[(x * 3) + 1] >= (Formatted_ENEMY_SMALLMINION_HP_COLOR[1]) & row[(x * 3) + 1] <= (Formatted_ENEMY_SMALLMINION_HP_COLOR[1])) //green
                                if (row[(x * 3) + 2] >= (Formatted_ENEMY_SMALLMINION_HP_COLOR[2]) & row[(x * 3) + 2] <= (Formatted_ENEMY_SMALLMINION_HP_COLOR[2])) //red
                                { // Confirm that it's enemy small minion POSITION
                                    if ((row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[0])) //blue
                                        if ((row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[1])) //green
                                            if ((row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify_HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify2_HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify3_HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_ENEMY_SMALLMINIONVerify4_HP_COLOR[2])) //red
                                            {
                                                Point minionPosition = new Point(x + 20, y + 40);

                                                double redBarCount = 0;

                                                for (int newX = x; newX < x + 60; newX++)
                                                {
                                                    if (row[newX * 3] >= Formatted_ENEMY_SMALLMINION_HP_COLOR[0] & row[newX * 3] <= Formatted_ENEMY_SMALLMINION_HP_COLOR[0]) //blue
                                                        if (row[(newX * 3) + 1] >= Formatted_ENEMY_SMALLMINION_HP_COLOR[1] & row[(newX * 3) + 1] <= Formatted_ENEMY_SMALLMINION_HP_COLOR[1]) //green
                                                            if (row[(newX * 3) + 2] >= Formatted_ENEMY_SMALLMINION_HP_COLOR[2] & row[(newX * 3) + 2] <= Formatted_ENEMY_SMALLMINION_HP_COLOR[2]) //red
                                                            {
                                                                redBarCount++;
                                                            }

                                                }
                                                double finalRedCountPercent = redBarCount / 60;

                                                if (finalRedCountPercent < 0.16)
                                                {
                                                    MinionPositionHealthPercent.Add(minionPosition, finalRedCountPercent);
                                                }
                                                else if (!(finalRedCountPercent >= 0.25 && finalRedCountPercent <= 0.33))
                                                {
                                                    MinionPositionHealthPercent.Add(minionPosition, finalRedCountPercent);

                                                }
                                            }
                                }

                        ////// Check if it's range circle
                        if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                            if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                {
                                    AttackRangeCirclePoints.Add(new Point(x, y));
                                }
                    }

                }

                // Check to make sure there is at least 1 and verify which enemy is nearest to mouse
                if (MinionPositionHealthPercent.Count > 0)
                {
                    Point LowestPercentHealthMinionPoint = Point.Empty;
                    double LowestHealthPercent = 9999;

                    foreach (KeyValuePair<Point, double> enemyPosition in MinionPositionHealthPercent)
                    {
                        Point[] Hitboxes = new Point[4];

                        Hitboxes[0] = new Point(enemyPosition.Key.X - 10, enemyPosition.Key.Y);
                        Hitboxes[1] = new Point(enemyPosition.Key.X, enemyPosition.Key.Y - 5);
                        Hitboxes[2] = new Point(enemyPosition.Key.X + 10, enemyPosition.Key.Y);
                        Hitboxes[3] = new Point(enemyPosition.Key.X, enemyPosition.Key.Y + 20);

                        foreach (var HitboxPoint in Hitboxes)
                        {
                            if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                            {
                                if (enemyPosition.Value < LowestHealthPercent)
                                {
                                    LowestPercentHealthMinionPoint = enemyPosition.Key;
                                    LowestHealthPercent = enemyPosition.Value;
                                }
                                break;
                            }
                        }

                    }
                    GetScreenShot.Dispose();
                    return LowestPercentHealthMinionPoint;
                }

                GetScreenShot.Dispose();
                return Point.Empty;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Pre-Formatted Range Circle Colors
        private static Color RGB_BARON_HP_COLOR = Color.FromArgb(107, 29, 24);
        private static Color RGB_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR = Color.FromArgb(150, 40, 32);
        private static Color RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR = Color.FromArgb(127, 34, 27);
        private static Color RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR = Color.FromArgb(210, 55, 45);

        private static Color RGB_BARON_VerifyHP_COLOR = Color.FromArgb(0, 8, 8);
        private static Color RGB_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR = Color.FromArgb(8, 4, 8);
        private static Color RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR = Color.FromArgb(8, 12, 16);
        private static Color RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR = Color.FromArgb(8, 12, 16);
        private static Color RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR = Color.FromArgb(8, 13, 16);
        private static Color RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR = Color.FromArgb(8, 13, 17);

        // Formatted Range Circle Colors
        static int[] Formatted_BARON_HP_COLOR = new int[3] { RGB_BARON_HP_COLOR.B, RGB_BARON_HP_COLOR.G, RGB_BARON_HP_COLOR.R };
        static int[] Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR = new int[3] { RGB_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR.B, RGB_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR.G, RGB_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR.R };
        static int[] Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR = new int[3] { RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR.B, RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR.G, RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR.R };
        static int[] Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR = new int[3] { RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR.B, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR.G, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR.R };

        static int[] Formatted_BARON_VerifyHP_COLOR = new int[3] { RGB_BARON_VerifyHP_COLOR.B, RGB_BARON_VerifyHP_COLOR.G, RGB_BARON_VerifyHP_COLOR.R };
        static int[] Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR = new int[3] { RGB_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR.B, RGB_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR.G, RGB_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR.R };
        static int[] Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR = new int[3] { RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR.B, RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR.G, RGB_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR.R };
        static int[] Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR = new int[3] { RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR.B, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR.G, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR.R };
        static int[] Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR = new int[3] { RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR.B, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR.G, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR.R };
        static int[] Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR = new int[3] { RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR.B, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR.G, RGB_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR.R };


        public static Point SearchForJungleCamps(Rectangle LeagueWindowRectangle)
        {
            Rectangle NeglectedPartsUI = new Rectangle(0, 0, LeagueWindowRectangle.Width, LeagueWindowRectangle.Height);
            Bitmap GetScreenShot = ScreenExService.GetScreenCapture(NeglectedPartsUI);

            BitmapData RegionIn_BitmapData = GetScreenShot.LockBits(NeglectedPartsUI, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            Dictionary<Point, Point[]> AllCampsPosition = new Dictionary<Point, Point[]>();
            List<Point> AttackRangeCirclePoints = new List<Point>();


            unsafe
            {
                for (int y = LeagueWindowRectangle.Y; y < RegionIn_BitmapData.Height - 100; y++)
                {
                    byte* row = (byte*)RegionIn_BitmapData.Scan0 + (y * RegionIn_BitmapData.Stride);
                    for (int x = LeagueWindowRectangle.X; x < RegionIn_BitmapData.Width; x++)
                    {
                        if (!(x <= LeagueWindowRectangle.X + 300 && y <= LeagueWindowRectangle.Y + 150))
                        {
                            ////// Check if it's enemy small minion HP
                            if ((row[x * 3] >= (Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR[0]) & row[x * 3] <= (Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR[0])) || (row[x * 3] >= (Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR[0]) & row[x * 3] <= (Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR[0])) || (row[x * 3] >= (Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR[0]) & row[x * 3] <= (Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR[0])) || (row[x * 3] >= (Formatted_BARON_HP_COLOR[0]) & row[x * 3] <= (Formatted_BARON_HP_COLOR[0]))) //blue
                                if ((row[(x * 3) + 1] >= (Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR[1]) & row[(x * 3) + 1] <= (Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR[1])) || (row[(x * 3) + 1] >= (Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR[1]) & row[(x * 3) + 1] <= (Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR[1])) || (row[(x * 3) + 1] >= (Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR[1]) & row[(x * 3) + 1] <= (Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR[1])) || (row[(x * 3) + 1] >= (Formatted_BARON_HP_COLOR[1]) & row[(x * 3) + 1] <= (Formatted_BARON_HP_COLOR[1]))) //green
                                    if ((row[(x * 3) + 2] >= (Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR[2]) & row[(x * 3) + 2] <= (Formatted_RED_BLUEBUFF_DRAGON_CRAB_HP_COLOR[2])) || (row[(x * 3) + 2] >= (Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR[2]) & row[(x * 3) + 2] <= (Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_HP_COLOR[2])) || (row[(x * 3) + 2] >= (Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR[2]) & row[(x * 3) + 2] <= (Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_HP_COLOR[2])) || (row[(x * 3) + 2] >= (Formatted_BARON_HP_COLOR[2]) & row[(x * 3) + 2] <= (Formatted_BARON_HP_COLOR[2]))) //red
                                    { // confirm it's camp HP position

                                        if ((row[(x - 1) * 3] >= Formatted_BARON_VerifyHP_COLOR[0] & row[(x - 1) * 3] <= Formatted_BARON_VerifyHP_COLOR[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_BARON_VerifyHP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_BARON_VerifyHP_COLOR[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_BARON_VerifyHP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_BARON_VerifyHP_COLOR[2])) //red
                                                {
                                                    Point baronPosition = new Point(x + 50, y + 150);

                                                    Point[] Hitboxes = new Point[4];

                                                    Hitboxes[0] = new Point(baronPosition.X - 50, baronPosition.Y); // Top Left
                                                    Hitboxes[1] = new Point(baronPosition.X + 50, baronPosition.Y); // Top Right
                                                    Hitboxes[2] = new Point(baronPosition.X - 200, baronPosition.Y + 50); // Bottom Left
                                                    Hitboxes[3] = new Point(baronPosition.X + 200, baronPosition.Y + 50); // Bottom Right

                                                    AllCampsPosition.Add(baronPosition, Hitboxes);
                                                }

                                        if ((row[(x - 1) * 3] >= Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR[0] & row[(x - 1) * 3] <= Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_RED_BLUEBUFF_DRAGON_CRAB_VerifyHP_COLOR[2])) //red
                                                {
                                                    Point bigCampPosition = new Point(x + 65, y + 100);

                                                    Point[] Hitboxes = new Point[4];

                                                    Hitboxes[0] = new Point(bigCampPosition.X - 30, bigCampPosition.Y - 5); // Top Left
                                                    Hitboxes[1] = new Point(bigCampPosition.X + 30, bigCampPosition.Y - 5); // Top Right
                                                    Hitboxes[2] = new Point(bigCampPosition.X - 40, bigCampPosition.Y + 20); // Bottom Left
                                                    Hitboxes[3] = new Point(bigCampPosition.X + 40, bigCampPosition.Y + 20); // Bottom Right

                                                    AllCampsPosition.Add(bigCampPosition, Hitboxes);
                                                }

                                        if ((row[(x - 1) * 3] >= Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR[0] & row[(x - 1) * 3] <= Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_FROG_BIGWOLF_BIGRAPTOR_BIGGOLEM_VerifyHP_COLOR[2])) //red
                                                {
                                                    Point mediumCampPosition = new Point(x + 35, y + 60);

                                                    Point[] Hitboxes = new Point[4];

                                                    Hitboxes[0] = new Point(mediumCampPosition.X - 30, mediumCampPosition.Y - 5); // Top Left
                                                    Hitboxes[1] = new Point(mediumCampPosition.X + 30, mediumCampPosition.Y - 5); // Top Right
                                                    Hitboxes[2] = new Point(mediumCampPosition.X - 40, mediumCampPosition.Y + 20); // Bottom Left
                                                    Hitboxes[3] = new Point(mediumCampPosition.X + 40, mediumCampPosition.Y + 20); // Bottom Right

                                                    AllCampsPosition.Add(mediumCampPosition, Hitboxes);
                                                }

                                        if ((row[(x - 1) * 3] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR[0]) || (row[(x - 1) * 3] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR[0] & row[(x - 1) * 3] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR[0])) //blue
                                            if ((row[((x - 1) * 3) + 1] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR[1]) || (row[((x - 1) * 3) + 1] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR[1] & row[((x - 1) * 3) + 1] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR[1])) //green
                                                if ((row[((x - 1) * 3) + 2] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify1HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify2HP_COLOR[2]) || (row[((x - 1) * 3) + 2] >= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR[2] & row[((x - 1) * 3) + 2] <= Formatted_SMALLWOLF_SMALLRAPTOR_SMALLGOLEM_Verify3HP_COLOR[2])) //red
                                                {
                                                    Point smallCampPosition = new Point(x + 25, y + 50);

                                                    Point[] Hitboxes = new Point[4];

                                                    Hitboxes[0] = new Point(smallCampPosition.X - 30, smallCampPosition.Y - 5); // Top Left
                                                    Hitboxes[1] = new Point(smallCampPosition.X + 30, smallCampPosition.Y - 5); // Top Right
                                                    Hitboxes[2] = new Point(smallCampPosition.X - 40, smallCampPosition.Y + 20); // Bottom Left
                                                    Hitboxes[3] = new Point(smallCampPosition.X + 40, smallCampPosition.Y + 20); // Bottom Right

                                                    AllCampsPosition.Add(smallCampPosition, Hitboxes);
                                                }
                                    }

                            ////// Check if it's range circle
                            if (row[x * 3] >= (Formatted_FROM_Color[0]) & row[x * 3] <= (Formatted_TO_Color[0])) //blue
                                if (row[(x * 3) + 1] >= (Formatted_FROM_Color[1]) & row[(x * 3) + 1] <= (Formatted_TO_Color[1])) //green
                                    if (row[(x * 3) + 2] >= (Formatted_FROM_Color[2]) & row[(x * 3) + 2] <= (Formatted_TO_Color[2])) //red
                                    {
                                        AttackRangeCirclePoints.Add(new Point(x, y));
                                    }
                        }
                    }

                }

                Dictionary<Point, double> CampsDistanceBetweenMouse = new Dictionary<Point, double>();

                foreach (var Enemy in AllCampsPosition)
                {
                    var distanceBetweenEnemyAndMouse_2D = Maths.CheckDistanceBetween2Points(Enemy.Key.X, Enemy.Key.Y, Cursor.Position.X, Cursor.Position.Y) * 100000 / 100000;

                    CampsDistanceBetweenMouse.Add(Enemy.Key, distanceBetweenEnemyAndMouse_2D);
                }

                // Check to make sure there is at least 1 and verify which enemy is nearest to mouse
                if (CampsDistanceBetweenMouse.Count > 0)
                {
                    Point ClosestEnemyToMouse = Point.Empty;
                    double ClosestDistanceToMouse = 9999;


                    foreach (KeyValuePair<Point, double> enemyPosition in CampsDistanceBetweenMouse)
                    {
                        Point[] Hitboxes = new Point[4];

                        Point Key = enemyPosition.Key;

                        Hitboxes[0] = new Point(AllCampsPosition[Key][0].X, AllCampsPosition[Key][0].Y); // Top Left
                        Hitboxes[1] = new Point(AllCampsPosition[Key][1].X, AllCampsPosition[Key][1].Y); // Top Right
                        Hitboxes[2] = new Point(AllCampsPosition[Key][2].X, AllCampsPosition[Key][2].Y); // Bottom Left
                        Hitboxes[3] = new Point(AllCampsPosition[Key][3].X, AllCampsPosition[Key][3].Y); // Bottom Right

                        foreach (var HitboxPoint in Hitboxes)
                        {
                            if (Utility.IsInPolygon(HitboxPoint, AttackRangeCirclePoints))
                            {
                                if (enemyPosition.Value < ClosestDistanceToMouse)
                                {
                                    ClosestEnemyToMouse = enemyPosition.Key;
                                    ClosestDistanceToMouse = enemyPosition.Value;
                                }
                                break;
                            }
                        }

                    }
                    GetScreenShot.Dispose();
                    return ClosestEnemyToMouse;
                }

                GetScreenShot.Dispose();
                return Point.Empty;
            }
        }
    }

}
