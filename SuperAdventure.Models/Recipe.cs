using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonIgnore]
        public string Name { get; }
        /// <summary>
        /// Gets the ingredients.
        /// </summary>
        /// <value>
        /// The ingredients.
        /// </value>
        [JsonIgnore] public List<ItemQuantity> Ingredients { get; }
        /// <summary>
        /// Gets the output items.
        /// </summary>
        /// <value>
        /// The output items.
        /// </value>
        [JsonIgnore]
        public List<ItemQuantity> OutputItems { get; }
        /// <summary>
        /// Gets the tool tip contents.
        /// </summary>
        /// <value>
        /// The tool tip contents.
        /// </value>
        [JsonIgnore]
        public string ToolTipContents =>
            "Ingredients" + Environment.NewLine +
            "===========" + Environment.NewLine +
            string.Join(Environment.NewLine, Ingredients.Select(i => i.QuantityItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Creates" + Environment.NewLine +
            "===========" + Environment.NewLine +
            string.Join(Environment.NewLine, OutputItems.Select(i => i.QuantityItemDescription));
        /// <summary>
        /// Initializes a new instance of the <see cref="Recipe"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="ingredients">The ingredients.</param>
        /// <param name="outputItems">The output items.</param>
        public Recipe(int id, string name, List<ItemQuantity> ingredients, List<ItemQuantity> outputItems)
        {
            ID = id;
            Name = name;
            Ingredients = ingredients;
            OutputItems = outputItems;
        }
    }
}
