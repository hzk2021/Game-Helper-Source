
namespace Discord.API
{
    public class ChampionWindUpPercent
    {
        public static double GetChampionWindUp(string championName)
        {
            double championWindup = 23;

            switch (championName)
            {
                case "Ashe": // Start of ADC Champion List
                    championWindup = 21.93;
                    break;
                case "Aphelios":
                    championWindup = 15.333;
                    break;
                case "Caitlyn":
                    championWindup = 17.708;
                    break;
                case "Corki":
                    championWindup = 10;
                    break;
                case "Draven":
                    championWindup = 15.614;
                    break;
                case "Ezreal":
                    championWindup = 18.84;
                    break;
                case "Jhin":
                    championWindup = 15.63;
                    break;
                case "Jinx":
                    championWindup = 16.875;
                    break;
                case "Kai'Sa":
                    championWindup = 16.108;
                    break;
                case "Kalista":
                    championWindup = 36;
                    break;
                case "Kayle":
                    championWindup = 19.355;
                    break;
                case "Kindred":
                    championWindup = 17.54;
                    break;
                case "Kog'Maw":
                    championWindup = 16.622;
                    break;
                case "Lucian":
                    championWindup = 15;
                    break;
                case "Miss Fortune":
                    championWindup = 14.8;
                    break;
                case "Senna":
                    championWindup = 34.38;
                    break;
                case "Samira":
                    championWindup = 15;
                    break;
                case "Quinn":
                    championWindup = 17.54;
                    break;
                case "Sivir":
                    championWindup = 12;
                    break;
                case "Tristana":
                    championWindup = 14.801;
                    break;
                case "Twitch":
                    championWindup = 20.192;
                    break;
                case "Varus":
                    championWindup = 17.544;
                    break;
                case "Vayne":
                    championWindup = 17.544;
                    break;
                case "Xayah":
                    championWindup = 17.687;
                    break;
                case "Ahri": // Start of Range Champion List
                    championWindup = 18.75;
                    break;
                case "Anivia":
                    championWindup = 29.17;
                    break;
                case "Annie":
                    championWindup = 19.58;
                    break;
                case "Aurelion Sol":
                    championWindup = 20;
                    break;
                case "Azir":
                    championWindup = 18.75;
                    break;
                case "Bard":
                    championWindup = 18.75;
                    break;
                case "Brand":
                    championWindup = 18.75;
                    break;
                case "Cassiopeia":
                    championWindup = 19.2;
                    break;
                case "Elise":
                    championWindup = 18.75;
                    break;
                case "Fiddlesticks":
                    championWindup = 22.92;
                    break;
                case "Gnar":
                    championWindup = 14.6;
                    break;
                case "Graves":
                    championWindup = 0.5;
                    break;
                case "Heimerdinger":
                    championWindup = 20.08;
                    break;
                case "Ivern":
                    championWindup = 23;
                    break;
                case "Janna":
                    championWindup = 22;
                    break;
                case "Karma":
                    championWindup = 16.15;
                    break;
                case "Karthus":
                    championWindup = 34.38;
                    break;
                case "Kennen":
                    championWindup = 20;
                    break;
                case "Leblanc":
                    championWindup = 16.67;
                    break;
                case "Lissandra":
                    championWindup = 18.75;
                    break;
                case "Lulu":
                    championWindup = 18.75;
                    break;
                case "Lux":
                    championWindup = 15.63;
                    break;
                case "Malzahar":
                    championWindup = 19;
                    break;
                case "Morgana":
                    championWindup = 14;
                    break;
                case "Nami":
                    championWindup = 18;
                    break;
                case "Neeko":
                    championWindup = 21.48;
                    break;
                case "Nidalee":
                    championWindup = 15;
                    break;
                case "Nocturne":
                    championWindup = 20.053;
                    break;
                case "Orianna":
                    championWindup = 17.54;
                    break;
                case "Rakan":
                    championWindup = 17.14;
                    break;
                case "Ryze":
                    championWindup = 20;
                    break;
                case "Sona":
                    championWindup = 17.18;
                    break;
                case "Soraka":
                    championWindup = 18.7;
                    break;
                case "Swain":
                    championWindup = 14;
                    break;
                case "Syndra":
                    championWindup = 18.75;
                    break;
                case "Taliyah":
                    championWindup = 16.15;
                    break;
                case "Teemo":
                    championWindup = 21.57;
                    break;
                case "Thresh":
                    championWindup = 23.96;
                    break;
                case "Twisted Fate":
                    championWindup = 24.4;
                    break;
                case "Urgot":
                    championWindup = 15;
                    break;
                case "Veigar":
                    championWindup = 19.09;
                    break;
                case "Vel'Koz":
                    championWindup = 20;
                    break;
                case "Viktor":
                    championWindup = 18;
                    break;
                case "Vladimir":
                    championWindup = 19.74;
                    break;
                case "Xerath":
                    championWindup = 25.07;
                    break;
                case "Yuumi":
                    championWindup = 15.63;
                    break;
                case "Ziggs":
                    championWindup = 20.63;
                    break;
                case "Zilean":
                    championWindup = 18;
                    break;
                case "Zoe":
                    championWindup = 16.15;
                    break;
                case "Zyra":
                    championWindup = 14.58;
                    break;
                default:
                    championWindup = 0;
                    break;
            }

            return championWindup / 100;
        }
    }

}
