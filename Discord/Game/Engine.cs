using Discord.API;
using System.Threading.Tasks;
using System.Drawing;
using Discord.Hardware;
using Discord.Modules;

namespace Discord.Game
{
    public static class Engine
    {
        public static double LastAATick = 0;

        public static void ResetAutoAttackTimer()
        {
            LastAATick = 0;
        }

        public static double GetLocalObjectAttackSpeed()
        {
            return Service.GetAPIData().ActivePlayer.ChampionStats.AttackSpeed;
        }

        public static double GetLocalObjectAttackRange()
        {
            return Service.GetAPIData().ActivePlayer.ChampionStats.AttackRange;
        }

        public static double GetLocalObjectAttackDamage()
        {
            return Service.GetAPIData().ActivePlayer.ChampionStats.AttackDamage;
        }

        public static string GetLocalObjectChampionName()
        {
            LiveGameData result = Service.GetAPIData();
            string championName = string.Empty;

            foreach (var player in result.AllPlayers)
            {
                if (player.SummonerName == result.ActivePlayer.SummonerName)
                {
                    championName = player.ChampionName;
                }
            }

            return championName;
        }

        public static double GetLocalObjectCurrentHealth()
        {
            return Service.GetAPIData().ActivePlayer.ChampionStats.CurrentHealth;
        }

        public static double GetLocalObjectMaxHealth()
        {
            return Service.GetAPIData().ActivePlayer.ChampionStats.MaxHealth;
        }

        public static string GetSummonerSpellOneName()
        {
            LiveGameData result = Service.GetAPIData();
            foreach (var player in result.AllPlayers)
            {
                if (player.SummonerName == result.ActivePlayer.SummonerName)
                {
                    return player.SummonerSpells.SummonerSpellOne.DisplayName;
                }
            }

            return string.Empty;
        }

        public static string GetSummonerSpellTwoName()
        {
            LiveGameData result = Service.GetAPIData();
            foreach (var player in result.AllPlayers)
            {
                if (player.SummonerName == result.ActivePlayer.SummonerName)
                {
                    return player.SummonerSpells.SummonerSpellTwo.DisplayName;
                }
            }

            return string.Empty;
        }

        public static double GetGameTime()
        {
            return Service.GetAPIData().GameData.GameTime;
        }

        public static int GetLocalObjectLevel()
        {
            return (int)Service.GetAPIData().ActivePlayer.Level;
        }

        public static bool IsLocalObjectPlayerDead()
        {
            LiveGameData result = Service.GetAPIData();
            foreach (var player in result.AllPlayers)
            {
                if (player.SummonerName == result.ActivePlayer.SummonerName)
                {
                    return player.IsDead;
                }
            }

            return false;
        }

        public static void IssueOrder(OrderType Order, Point Vector2D = new Point())
        {
            switch (Order)
            {
                case OrderType.MoveCursorTo:
                    Mouse.MoveMouseToXY(Vector2D.X, Vector2D.Y);
                    Mouse.MoveMouseToXY(Vector2D.X, Vector2D.Y);
                    Mouse.MoveMouseToXY(Vector2D.X, Vector2D.Y);
                    break;
                case OrderType.AttackUnit:
                    //Keyboard.SendKeyPress("S");
                    Mouse.MoveMouseToXY50Times(Vector2D.X, Vector2D.Y);
                    Keyboard.SendKeyPress("F10");
                    break;
                case OrderType.Move:
                    //Mouse.LeftClickDown();
                    //Thread.Sleep(5);
                    //Mouse.LeftClickUp();

                    Mouse.FullRightClick();
                    break;
                case OrderType.AutoAttack:
                    Keyboard.SendKeyPress("F10");
                    break;
                case OrderType.Stop:
                    Keyboard.SendKeyPress("S");
                    break;
            }
        }

        public static async void ResetQ()
        {
            if (Utility.IsKeyPressed("Q"))
            {
                int AnimationWait = ChampionsSkillsAnimationTimeReset.GetQAnimationTime(GetLocalObjectChampionName());
                if (AnimationWait != 0)
                {
                    await Task.Delay(AnimationWait);
                    ResetAutoAttackTimer();
                }
            }
        }

        public static async void ResetW()
        {
            if (Utility.IsKeyPressed("W"))
            {
                int AnimationWait = ChampionsSkillsAnimationTimeReset.GetWAnimationTime(GetLocalObjectChampionName());
                if (AnimationWait != 0)
                {
                    await Task.Delay(AnimationWait);
                    ResetAutoAttackTimer();
                }
            }
        }

        public static async void ResetE()
        {
            if (Utility.IsKeyPressed("E"))
            {
                int AnimationWait = ChampionsSkillsAnimationTimeReset.GetEAnimationTime(GetLocalObjectChampionName());
                if (AnimationWait != 0)
                {
                    await Task.Delay(AnimationWait);
                    ResetAutoAttackTimer();
                }
            }
        }

        public static async void ResetR()
        {
            if (Utility.IsKeyPressed("R"))
            {
                int AnimationWait = ChampionsSkillsAnimationTimeReset.GetRAnimationTime(GetLocalObjectChampionName());
                if (AnimationWait != 0)
                {
                    await Task.Delay(AnimationWait);
                    ResetAutoAttackTimer();
                }
            }
        }

    }
}
