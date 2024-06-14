using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Quest
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
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonIgnore]
        public string Description { get; }
        /// <summary>
        /// Gets the items to complete.
        /// </summary>
        /// <value>
        /// The items to complete.
        /// </value>
        [JsonIgnore]
        public List<ItemQuantity> ItemsToComplete { get; }
        /// <summary>
        /// Gets the reward experience points.
        /// </summary>
        /// <value>
        /// The reward experience points.
        /// </value>
        [JsonIgnore]
        public int RewardExperiencePoints { get; }
        /// <summary>
        /// Gets the reward gold.
        /// </summary>
        /// <value>
        /// The reward gold.
        /// </value>
        [JsonIgnore]
        public int RewardGold { get; }
        /// <summary>
        /// Gets the reward items.
        /// </summary>
        /// <value>
        /// The reward items.
        /// </value>
        [JsonIgnore]
        public List<ItemQuantity> RewardItems { get; }
        /// <summary>
        /// Gets the tool tip contents.
        /// </summary>
        /// <value>
        /// The tool tip contents.
        /// </value>
        [JsonIgnore]
        public string ToolTipContents =>
            Description + Environment.NewLine + Environment.NewLine +
            "Items to complete the quest" + Environment.NewLine +
            "===========================" + Environment.NewLine +
            string.Join(Environment.NewLine, ItemsToComplete.Select(i => i.QuantityItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Rewards\r\n" +
            "===========================" + Environment.NewLine +
            $"{RewardExperiencePoints} experience points" + Environment.NewLine +
            $"{RewardGold} gold pieces" + Environment.NewLine +
            string.Join(Environment.NewLine, RewardItems.Select(i => i.QuantityItemDescription));
        /// <summary>
        /// Initializes a new instance of the <see cref="Quest"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="itemsToComplete">The items to complete.</param>
        /// <param name="rewardExperiencePoints">The reward experience points.</param>
        /// <param name="rewardGold">The reward gold.</param>
        /// <param name="rewardItems">The reward items.</param>
        public Quest(int id, string name, string description, List<ItemQuantity> itemsToComplete,
                     int rewardExperiencePoints, int rewardGold, List<ItemQuantity> rewardItems)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemsToComplete = itemsToComplete;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardGold = rewardGold;
            RewardItems = rewardItems;
        }
    }
}
