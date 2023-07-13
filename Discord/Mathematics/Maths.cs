using System;

namespace Discord.Mathematics
{
    public static class Maths
    {
        public static double CheckDistanceBetween2Points(int x1, int y1, int x2, int y2)
        {
            // Calculating distance 
            return Math.Sqrt(Math.Pow(x2 - x1, 2) +
                          Math.Pow(y2 - y1, 2) * 1.0);
        }
    }
}
