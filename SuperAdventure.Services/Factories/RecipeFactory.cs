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
    public static class RecipeFactory
    {
        /// <summary>
        /// The game data filename
        /// </summary>
        private const string GAME_DATA_FILENAME = ".\\GameData\\Recipes.xml";
        /// <summary>
        /// The recipes
        /// </summary>
        private static readonly List<Recipe> _recipes = new List<Recipe>();
        static RecipeFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                LoadRecipesFromNodes(data.SelectNodes("/Recipes/Recipe"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }
        /// <summary>
        /// Loads the recipes from nodes.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        private static void LoadRecipesFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                var ingredients = new List<ItemQuantity>();
                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));
                    ingredients.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }
                var outputItems = new List<ItemQuantity>();
                foreach (XmlNode childNode in node.SelectNodes("./OutputItems/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.AttributeAsInt("ID"));
                    outputItems.Add(new ItemQuantity(item, childNode.AttributeAsInt("Quantity")));
                }
                Recipe recipe =
                    new Recipe(node.AttributeAsInt("ID"),
                        node.SelectSingleNode("./Name")?.InnerText ?? "",
                        ingredients, outputItems);
                _recipes.Add(recipe);
            }
        }
        /// <summary>
        /// Recipes the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }
    }
}