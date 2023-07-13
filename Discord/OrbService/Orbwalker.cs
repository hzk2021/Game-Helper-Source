using System;
using Discord.Game;
using System.Windows.Forms;
using System.Drawing;

namespace Discord.OrbService
{
    public static class Orbwalker
    {
        static Random rnd = new Random();
        public static void StartOrbwalk(int Ping, double attackSpeed, int delayModifier, Point enemy2DPosition, int maxClickSpeed = 100,
            bool resetQ = false, bool resetW = false, bool resetE = false, bool resetR = false, bool emergencyPlan = false)
        {
            if (LocalPlayer.CanAttack(attackSpeed) && enemy2DPosition != Point.Empty)
            {
                Point oldPos = Cursor.Position;

                Engine.IssueOrder(OrderType.AttackUnit, enemy2DPosition);

                Engine.LastAATick = Environment.TickCount;
                LocalPlayer.NextMoveCommandT = Environment.TickCount + LocalPlayer.GetWindupTime_New(attackSpeed) + (30 + Ping / 2) + delayModifier; // Extra Delay , 30 + Ping /2

                // Thread.Sleep(20) // Mouse cursor fix?
                Engine.IssueOrder(OrderType.MoveCursorTo, oldPos);

            }
            else if (LocalPlayer.CanAttack(attackSpeed) && emergencyPlan)
            {
                Engine.IssueOrder(OrderType.AttackUnit, Cursor.Position);

                Engine.LastAATick = Environment.TickCount;
                LocalPlayer.NextMoveCommandT = Environment.TickCount + LocalPlayer.GetWindupTime_New(attackSpeed) + (30 + Ping / 2) + delayModifier; // Extra Delay , 30 + Ping /2

                Engine.IssueOrder(OrderType.MoveCursorTo, Cursor.Position);
            }

            if (LocalPlayer.CanMove())
            {
                LocalPlayer.CheckResetSkills(enemy2DPosition, resetQ, resetW, resetE, resetR);

                Engine.IssueOrder(OrderType.Move);
                LocalPlayer.NextMoveCommandT = Environment.TickCount + rnd.Next(80, maxClickSpeed);
            }
        }

    }
}
