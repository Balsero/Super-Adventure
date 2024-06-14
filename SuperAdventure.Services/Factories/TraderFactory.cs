using System;
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
    public static class TraderFactory
    {
        /// <summary>
        /// The game data filename
        /// </summary>
        private const string GAME_DATA_FILENAME = ".\\GameData\\Traders.xml";
        /// <summary>
        /// The traders
        /// </summary>
        private static readonly List<Trader> _traders = new List<Trader>();
        static TraderFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadTradersFromNodes(data.SelectNodes("/Traders/Trader"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }
        /// <summary>
        /// Loads the traders from nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private static void LoadTradersFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                Trader trader =
                    new Trader(node.AttributeAsInt("ID"),
                               node.SelectSingleNode("./Name")?.InnerText ?? "");
                foreach (XmlNode childNode in node.SelectNodes("./InventoryItems/Item"))
                {
                    int quantity = childNode.AttributeAsInt("Quantity");
                    // Create a new GameItem object for each item we add.
                    // This is to allow for unique items, like swords with enchantments.
                    for (int i = 0; i < quantity; i++)
                    {
                        trader.AddItemToInventory(ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID")));
                    }
                }
                _traders.Add(trader);
            }
        }
        /// <summary>
        /// Gets the trader by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static Trader GetTraderByID(int id)
        {
            return _traders.FirstOrDefault(t => t.ID == id);
        }
    }
}
