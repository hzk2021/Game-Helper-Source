using System;
using System.Drawing;
using Discord.API;

namespace Discord.Game
{
    public static class LocalPlayer
    {
        public static double NextMoveCommandT = 0;

        public static double GetWindupTime_New(double attackSpeed)
        {
            return GetAttackDelay_New(attackSpeed) * ChampionWindUpPercent.GetChampionWindUp(Engine.GetLocalObjectChampionName());
        }

        public static double GetAttackDelay_New(double attackSpeed)
        {
            // Original AttackDelay *Formula* ?
            // return (1000 / Engine.GetLocalObjectAttackSpeed());

            // New AttackDelay *Formula* ?
            return (1000 / attackSpeed) * 1.07;
        }

        public static bool CanAttack(double attackSpeed)
        {
            return Engine.LastAATick + GetAttackDelay_New(attackSpeed) < Environment.TickCount;
        }

        public static bool CanMove()
        {
            return NextMoveCommandT < Environment.TickCount;
        }

        public static void CheckResetSkills(Point enemyPos, bool resetQ = false, bool resetW = false, bool resetE = false, bool resetR = false)
        {
            if (enemyPos != Point.Empty)
            {
                if (resetQ)
                {
                    Engine.ResetQ();
                }
                if (resetW)
                {
                    Engine.ResetW();
                }
                if (resetE)
                {
                    Engine.ResetE();
                }
                if (resetR)
                {
                    Engine.ResetR();
                }
            }
        }
    }

}
