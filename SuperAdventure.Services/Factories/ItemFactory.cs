using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using SuperAdventure.Models.Actions;
using SuperAdventure.Models;
using SuperAdventure.Models.Shared;
namespace SuperAdventure.Services.Factories
{
    /// <summary>
    /// 
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        /// The game data filename
        /// </summary>
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";
        /// <summary>
        /// The standard game items
        /// </summary>
        private static readonly List<GameItem> _standardGameItems = new List<GameItem>();
        static ItemFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/HealingItems/HealingItem"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/MiscellaneousItems/MiscellaneousItem"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }
        /// <summary>
        /// Creates the game item.
        /// </summary>
        /// <param name="itemTypeID">The item type identifier.</param>
        /// <returns></returns>
        public static GameItem CreateGameItem(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }
        /// <summary>
        /// Loads the items from nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                GameItem.ItemCategory itemCategory = DetermineItemCategory(node.Name);

                GameItem gameItem =
                    new GameItem(itemCategory,
                                 node.AttributeAsInt("ID"),
                                 node.AttributeAsString("Name"),
                                 node.AttributeAsInt("Price"),
                                 itemCategory == GameItem.ItemCategory.Weapon);
                if (itemCategory == GameItem.ItemCategory.Weapon)
                {
                    gameItem.Action =
                        new AttackWithWeapon(gameItem, node.AttributeAsString("DamageDice"));
                }
                else if (itemCategory == GameItem.ItemCategory.Consumable)
                {
                    gameItem.Action =
                        new Heal(gameItem,
                                 node.AttributeAsInt("HitPointsToHeal"));
                }
                _standardGameItems.Add(gameItem);
            }
        }
        /// <summary>
        /// Determines the item category.
        /// </summary>
        /// <param name="itemType">Type of the item.</param>
        /// <returns></returns>
        private static GameItem.ItemCategory DetermineItemCategory(string itemType)
        {
            switch (itemType)
            {
                case "Weapon":
                    return GameItem.ItemCategory.Weapon;
                case "HealingItem":
                    return GameItem.ItemCategory.Consumable;
                default:
                    return GameItem.ItemCategory.Miscellaneous;
            }
        }
    }
}