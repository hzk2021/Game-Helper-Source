using Discord.Game;
using Discord.Hardware;

namespace Discord.Activators
{
    public static class AutoUsage
    {
        public static void AutoHeal(int AutoHealPercent)
        {
            double maxHealth = Engine.GetLocalObjectMaxHealth();
            double onePercentHealth = maxHealth / 100;
            double autoHealHealth = onePercentHealth * AutoHealPercent;

            double currentHealth = Engine.GetLocalObjectCurrentHealth();

            bool isPlayerDead = Engine.IsLocalObjectPlayerDead();

            if (currentHealth <= autoHealHealth && !isPlayerDead)
            {
                string summonerSpellOne = Engine.GetSummonerSpellOneName();
                string summonerSpellTwo = Engine.GetSummonerSpellTwoName();

                if (summonerSpellOne == "Heal")
                {
                    Keyboard.SendKeyPress("D");
                }
                else if (summonerSpellTwo == "Heal")
                {
                    Keyboard.SendKeyPress("F");
                }
            }
        }
    }
}