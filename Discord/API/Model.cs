using Newtonsoft.Json;

namespace Discord.API
{
    public partial class LiveGameData
    {
        [JsonProperty("activePlayer")]
        public ActivePlayer ActivePlayer { get; set; }

        [JsonProperty("allPlayers")]
        public AllPlayer[] AllPlayers { get; set; }

        [JsonProperty("events")]
        public Events Events { get; set; }

        [JsonProperty("gameData")]
        public GameData GameData { get; set; }
    }

    public partial class ActivePlayer
    {
        [JsonProperty("abilities")]
        public Abilities Abilities { get; set; }

        [JsonProperty("championStats")]
        public ChampionStats ChampionStats { get; set; }

        [JsonProperty("currentGold")]
        public long CurrentGold { get; set; }

        [JsonProperty("fullRunes")]
        public FullRunes FullRunes { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }
    }

    public partial class Abilities
    {
        [JsonProperty("E")]
        public E E { get; set; }

        [JsonProperty("Passive")]
        public E Passive { get; set; }

        [JsonProperty("Q")]
        public E Q { get; set; }

        [JsonProperty("R")]
        public E R { get; set; }

        [JsonProperty("W")]
        public E W { get; set; }
    }

    public partial class E
    {
        [JsonProperty("abilityLevel", NullValueHandling = NullValueHandling.Ignore)]
        public long? AbilityLevel { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("rawDescription")]
        public string RawDescription { get; set; }

        [JsonProperty("rawDisplayName")]
        public string RawDisplayName { get; set; }
    }

    public partial class ChampionStats
    {
        [JsonProperty("abilityPower")]
        public long AbilityPower { get; set; }

        [JsonProperty("armor")]
        public long Armor { get; set; }

        [JsonProperty("armorPenetrationFlat")]
        public long ArmorPenetrationFlat { get; set; }

        [JsonProperty("armorPenetrationPercent")]
        public long ArmorPenetrationPercent { get; set; }

        [JsonProperty("attackDamage")]
        public double AttackDamage { get; set; }

        [JsonProperty("attackRange")]
        public long AttackRange { get; set; }

        [JsonProperty("attackSpeed")]
        public double AttackSpeed { get; set; }

        [JsonProperty("bonusArmorPenetrationPercent")]
        public long BonusArmorPenetrationPercent { get; set; }

        [JsonProperty("bonusMagicPenetrationPercent")]
        public long BonusMagicPenetrationPercent { get; set; }

        [JsonProperty("cooldownReduction")]
        public long CooldownReduction { get; set; }

        [JsonProperty("critChance")]
        public long CritChance { get; set; }

        [JsonProperty("critDamage")]
        public long CritDamage { get; set; }

        [JsonProperty("currentHealth")]
        public long CurrentHealth { get; set; }

        [JsonProperty("healthRegenRate")]
        public double HealthRegenRate { get; set; }

        [JsonProperty("lifeSteal")]
        public double LifeSteal { get; set; }

        [JsonProperty("magicLethality")]
        public long MagicLethality { get; set; }

        [JsonProperty("magicPenetrationFlat")]
        public long MagicPenetrationFlat { get; set; }

        [JsonProperty("magicPenetrationPercent")]
        public long MagicPenetrationPercent { get; set; }

        [JsonProperty("magicResist")]
        public long MagicResist { get; set; }

        [JsonProperty("maxHealth")]
        public long MaxHealth { get; set; }

        [JsonProperty("moveSpeed")]
        public long MoveSpeed { get; set; }

        [JsonProperty("physicalLethality")]
        public long PhysicalLethality { get; set; }

        [JsonProperty("resourceMax")]
        public double ResourceMax { get; set; }

        [JsonProperty("resourceRegenRate")]
        public double ResourceRegenRate { get; set; }

        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("resourceValue")]
        public double ResourceValue { get; set; }

        [JsonProperty("spellVamp")]
        public long SpellVamp { get; set; }

        [JsonProperty("tenacity")]
        public long Tenacity { get; set; }
    }

    public partial class FullRunes
    {
        [JsonProperty("generalRunes")]
        public Keystone[] GeneralRunes { get; set; }

        [JsonProperty("keystone")]
        public Keystone Keystone { get; set; }

        [JsonProperty("primaryRuneTree")]
        public Keystone PrimaryRuneTree { get; set; }

        [JsonProperty("secondaryRuneTree")]
        public Keystone SecondaryRuneTree { get; set; }

        [JsonProperty("statRunes")]
        public StatRune[] StatRunes { get; set; }
    }

    public partial class Keystone
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rawDescription")]
        public string RawDescription { get; set; }

        [JsonProperty("rawDisplayName")]
        public string RawDisplayName { get; set; }
    }

    public partial class StatRune
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rawDescription")]
        public string RawDescription { get; set; }
    }

    public partial class AllPlayer
    {
        [JsonProperty("championName")]
        public string ChampionName { get; set; }

        [JsonProperty("isBot")]
        public bool IsBot { get; set; }

        [JsonProperty("isDead")]
        public bool IsDead { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("rawChampionName")]
        public string RawChampionName { get; set; }

        [JsonProperty("rawSkinName")]
        public string RawSkinName { get; set; }

        [JsonProperty("respawnTimer")]
        public long RespawnTimer { get; set; }

        [JsonProperty("runes")]
        public Runes Runes { get; set; }

        [JsonProperty("scores")]
        public Scores Scores { get; set; }

        [JsonProperty("skinID")]
        public long SkinId { get; set; }

        [JsonProperty("skinName")]
        public string SkinName { get; set; }

        [JsonProperty("summonerName")]
        public string SummonerName { get; set; }

        [JsonProperty("summonerSpells")]
        public SummonerSpells SummonerSpells { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("canUse")]
        public bool CanUse { get; set; }

        [JsonProperty("consumable")]
        public bool Consumable { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("itemID")]
        public long ItemId { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("rawDescription")]
        public string RawDescription { get; set; }

        [JsonProperty("rawDisplayName")]
        public string RawDisplayName { get; set; }

        [JsonProperty("slot")]
        public long Slot { get; set; }
    }

    public partial class Runes
    {
        [JsonProperty("keystone")]
        public Keystone Keystone { get; set; }

        [JsonProperty("primaryRuneTree")]
        public Keystone PrimaryRuneTree { get; set; }

        [JsonProperty("secondaryRuneTree")]
        public Keystone SecondaryRuneTree { get; set; }
    }

    public partial class Scores
    {
        [JsonProperty("assists")]
        public long Assists { get; set; }

        [JsonProperty("creepScore")]
        public long CreepScore { get; set; }

        [JsonProperty("deaths")]
        public long Deaths { get; set; }

        [JsonProperty("kills")]
        public long Kills { get; set; }

        [JsonProperty("wardScore")]
        public long WardScore { get; set; }
    }

    public partial class SummonerSpells
    {
        [JsonProperty("summonerSpellOne", NullValueHandling = NullValueHandling.Ignore)]
        public E SummonerSpellOne { get; set; }

        [JsonProperty("summonerSpellTwo", NullValueHandling = NullValueHandling.Ignore)]
        public E SummonerSpellTwo { get; set; }
    }

    public partial class Events
    {
        [JsonProperty("Events")]
        public Event[] EventsEvents { get; set; }
    }

    public partial class Event
    {
        [JsonProperty("EventID")]
        public long EventId { get; set; }

        [JsonProperty("EventName")]
        public string EventName { get; set; }

        [JsonProperty("EventTime")]
        public double EventTime { get; set; }
    }

    public partial class GameData
    {
        [JsonProperty("gameMode")]
        public string GameMode { get; set; }

        [JsonProperty("gameTime")]
        public double GameTime { get; set; }

        [JsonProperty("mapName")]
        public string MapName { get; set; }

        [JsonProperty("mapNumber")]
        public long MapNumber { get; set; }

        [JsonProperty("mapTerrain")]
        public string MapTerrain { get; set; }
    }
}
