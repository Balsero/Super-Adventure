using System.Xml;
using SuperAdventure.Models;
using SuperAdventure.Models.Shared;
namespace SuperAdventure.Services.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public static class WorldFactory
    {
        /// <summary>
        /// The game data filename
        /// </summary>
        private const string GAME_DATA_FILENAME = ".\\GameData\\Locations.xml";
        public static World CreateWorld()
        {
            World world = new World();
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                string rootImagePath =
                    data.SelectSingleNode("/Locations")
                        .AttributeAsString("RootImagePath");
                LoadLocationsFromNodes(world,
                                       rootImagePath,
                                       data.SelectNodes("/Locations/Location"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
            return world;
        }
        /// <summary>
        /// Loads the locations from nodes.
        /// </summary>
        /// <param name="world">The world.</param>
        /// <param name="rootImagePath">The root image path.</param>
        /// <param name="nodes">The nodes.</param>
        private static void LoadLocationsFromNodes(World world, string rootImagePath, XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                Location location =
                    new Location(node.AttributeAsInt("X"),
                                 node.AttributeAsInt("Y"),
                                 node.AttributeAsString("Name"),
                                 node.SelectSingleNode("./Description")?.InnerText ?? "",
                                 $".{rootImagePath}{node.AttributeAsString("ImageName")}");
                AddMonsters(location, node.SelectNodes("./Monsters/Monster"));
                AddQuests(location, node.SelectNodes("./Quests/Quest"));
                AddTrader(location, node.SelectSingleNode("./Trader"));
                world.AddLocation(location);
            }
        }
        /// <summary>
        /// Adds the monsters.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="monsters">The monsters.</param>
        private static void AddMonsters(Location location, XmlNodeList monsters)
        {
            if (monsters == null)
            {
                return;
            }
            foreach (XmlNode monsterNode in monsters)
            {
                location.AddMonster(monsterNode.AttributeAsInt("ID"),
                                    monsterNode.AttributeAsInt("Percent"));
            }
        }
        /// <summary>
        /// Adds the quests.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="quests">The quests.</param>
        private static void AddQuests(Location location, XmlNodeList quests)
        {
            if (quests == null)
            {
                return;
            }
            foreach (XmlNode questNode in quests)
            {
                location.QuestsAvailableHere
                        .Add(QuestFactory.GetQuestByID(questNode.AttributeAsInt("ID")));
            }
        }
        /// <summary>
        /// Adds the trader.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="traderHere">The trader here.</param>
        private static void AddTrader(Location location, XmlNode traderHere)
        {
            if (traderHere == null)
            {
                return;
            }
            location.TraderHere =
                TraderFactory.GetTraderByID(traderHere.AttributeAsInt("ID"));
        }
    }
}