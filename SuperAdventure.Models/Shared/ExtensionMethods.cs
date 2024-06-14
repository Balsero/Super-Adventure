
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using SuperAdventure.Models;
using Newtonsoft.Json.Linq;

namespace SuperAdventure.Models.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Attributes as int.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static int AttributeAsInt(this XmlNode node, string attributeName)
        {
            return Convert.ToInt32(node.AttributeAsString(attributeName));
        }

        public static string AttributeAsString(this XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];

            if (attribute == null)
            {
                throw new ArgumentException($"The attribute '{attributeName}' does not exist");
            }

            return attribute.Value;
        }

        /// <summary>
        /// Strings the value of.
        /// </summary>
        /// <param name="jsonObject">The json object.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string StringValueOf(this JObject jsonObject, string key)
        {
            return jsonObject[key].ToString();
        }

        /// <summary>
        /// Strings the value of.
        /// </summary>
        /// <param name="jsonToken">The json token.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string StringValueOf(this JToken jsonToken, string key)
        {
            return jsonToken[key].ToString();
        }

        /// <summary>
        /// Ints the value of.
        /// </summary>
        /// <param name="jsonToken">The json token.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static int IntValueOf(this JToken jsonToken, string key)
        {
            return Convert.ToInt32(jsonToken[key]);
        }
        /// <summary>
        /// Gets the attribute.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="attributeKey">The attribute key.</param>
        /// <returns></returns>
        public static PlayerAttribute GetAttribute(this LivingEntity entity, string attributeKey)
        {
            return entity.Attributes
                         .First(pa => pa.Key.Equals(attributeKey,
                                                    StringComparison.CurrentCultureIgnoreCase));
        }
        /// <summary>
        /// Itemses the that are.
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public static List<GameItem> ItemsThatAre(this IEnumerable<GameItem> inventory,
                                                  GameItem.ItemCategory category)
        {
            return inventory.Where(i => i.Category == category).ToList();
        }
    }
}