using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SuperAdventure.Models;
using SuperAdventure.Models.Shared;
namespace SuperAdventure.Services.Factories
{
    /// <summary>
    /// 
    /// </summary>
    internal static class QuestFactory
    {
        /// <summary>
        /// The game data filename
        /// </summary>
        private const string GAME_DATA_FILENAME = ".\\GameData\\Quests.xml";
        /// <summary>
        /// The quests
        /// </summary>
        private static readonly List<Quest> _quests = new List<Quest>();
        static QuestFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadQuestsFromNodes(data.SelectNodes("/Quests/Quest"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }
        /// <summary>
        /// Loads the quests from nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private static void LoadQuestsFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                // Declare the items need to complete the quest, and its reward items
                List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
                List<ItemQuantity> rewardItems = new List<ItemQuantity>();
                foreach (XmlNode childNode in node.SelectNodes("./ItemsToComplete/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));
                    itemsToComplete.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }
                foreach (XmlNode childNode in node.SelectNodes("./RewardItems/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));
                    rewardItems.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }
                _quests.Add(new Quest(node.AttributeAsInt("ID"),
                                      node.SelectSingleNode("./Name")?.InnerText ?? "",
                                      node.SelectSingleNode("./Description")?.InnerText ?? "",
                                      itemsToComplete,
                                      node.AttributeAsInt("RewardExperiencePoints"),
                                      node.AttributeAsInt("RewardGold"),
                                      rewardItems));
            }
        }
        /// <summary>
        /// Gets the quest by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}