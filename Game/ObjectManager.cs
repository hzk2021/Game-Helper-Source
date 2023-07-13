using System.Drawing;

namespace Discord.Game
{
    public static class ObjectManager
    {
        private static Point _Enemy2DPosition = Point.Empty;

        public static Point EnemyPosition
        {
            get { return _Enemy2DPosition; }
            set { _Enemy2DPosition = value; }
        }

    }
}
