using SuperAdventure.Models.Actions;
using Newtonsoft.Json;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GameItem
    {
        /// <summary>
        /// 
        /// </summary>
        public enum ItemCategory
        {
            /// <summary>
            /// The miscellaneous
            /// </summary>
            Miscellaneous,
            /// <summary>
            /// The weapon
            /// </summary>
            Weapon,
            /// <summary>
            /// The consumable
            /// </summary>
            Consumable
        }
        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [JsonIgnore]
        public ItemCategory Category { get; }
        /// <summary>
        /// Gets the item type identifier.
        /// </summary>
        /// <value>
        /// The item type identifier.
        /// </value>
        public int ItemTypeID { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonIgnore]
        public string Name { get; }
        /// <summary>
        /// Gets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        [JsonIgnore]
        public int Price { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is unique.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unique; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsUnique { get; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        [JsonIgnore]
        public IAction Action { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameItem"/> class.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <param name="itemTypeID">The item type identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        /// <param name="isUnique">if set to <c>true</c> [is unique].</param>
        /// <param name="action">The action.</param>
        public GameItem(ItemCategory category, int itemTypeID, string name, int price,
                        bool isUnique = false, IAction action = null)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
        }
        /// <summary>
        /// Performs the action.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="target">The target.</param>
        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, Price, IsUnique, Action);
        }
    }
}

