using System.Collections.Generic;

namespace Discord.API
{
    public static class ChampionsSkillsAnimationTimeReset
    {
        private static Dictionary<string, Dictionary<string, int>> ChampSkillsAnimationTime = new Dictionary<string, Dictionary<string, int>>()
        {
            {"Ashe", new Dictionary<string, int>{ {"Q", 5}, {"W", 0}, {"E", 0}, { "R", 0} } },
            {"Vayne", new Dictionary<string, int>{ {"Q", 200}, {"W", 0}, {"E", 0}, { "R", 0} } },
            {"Sivir", new Dictionary<string, int>{ {"Q", 0}, {"W", 150}, {"E", 0}, { "R", 0} } },
            {"Lucian", new Dictionary<string, int>{ {"Q", 0}, {"W", 0}, {"E", 250}, { "R", 0} } },
        };

        public static int GetQAnimationTime(string ChampionName)
        {
            if (!(ChampionName == "Ashe" || ChampionName == "Vayne" || ChampionName == "Sivir" || ChampionName == "Lucian"))
            {
                return 0;
            }
            return ChampSkillsAnimationTime[ChampionName]["Q"];
        }

        public static int GetWAnimationTime(string ChampionName)
        {
            if (!(ChampionName == "Ashe" || ChampionName == "Vayne" || ChampionName == "Sivir" || ChampionName == "Lucian"))
            {
                return 0;
            }
            return ChampSkillsAnimationTime[ChampionName]["W"];
        }

        public static int GetEAnimationTime(string ChampionName)
        {
            if (!(ChampionName == "Ashe" || ChampionName == "Vayne" || ChampionName == "Sivir" || ChampionName == "Lucian"))
            {
                return 0;
            }
            return ChampSkillsAnimationTime[ChampionName]["E"];
        }

        public static int GetRAnimationTime(string ChampionName)
        {
            if (!(ChampionName == "Ashe" || ChampionName == "Vayne" || ChampionName == "Sivir" || ChampionName == "Lucian"))
            {
                return 0;
            }
            return ChampSkillsAnimationTime[ChampionName]["R"];
        }
    }
}
