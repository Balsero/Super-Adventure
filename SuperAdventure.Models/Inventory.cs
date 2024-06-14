using System.Collections.Generic;
using System.Linq;
using SuperAdventure.Models.Shared;
using Newtonsoft.Json;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Inventory
    {
        #region Backing variables
        /// <summary>
        /// The backing inventory
        /// </summary>
        private readonly List<GameItem> _backingInventory =
            new List<GameItem>();
        /// <summary>
        /// The backing grouped inventory items
        /// </summary>
        private readonly List<GroupedInventoryItem> _backingGroupedInventoryItems =
            new List<GroupedInventoryItem>();
        #endregion
        #region Properties
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IReadOnlyList<GameItem> Items => _backingInventory.AsReadOnly();
        /// <summary>
        /// Gets the grouped inventory.
        /// </summary>
        /// <value>
        /// The grouped inventory.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<GroupedInventoryItem> GroupedInventory =>
            _backingGroupedInventoryItems.AsReadOnly();
        /// <summary>
        /// Gets the weapons.
        /// </summary>
        /// <value>
        /// The weapons.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<GameItem> Weapons =>
            _backingInventory.ItemsThatAre(GameItem.ItemCategory.Weapon).AsReadOnly();
        /// <summary>
        /// Gets the consumables.
        /// </summary>
        /// <value>
        /// The consumables.
        /// </value>
        [JsonIgnore]
        public IReadOnlyList<GameItem> Consumables =>
            _backingInventory.ItemsThatAre(GameItem.ItemCategory.Consumable).AsReadOnly();
        /// <summary>
        /// Gets a value indicating whether this instance has consumable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has consumable; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasConsumable => Consumables.Any();
        #endregion
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public Inventory(IEnumerable<GameItem> items = null)
        {
            if (items == null)
            {
                return;
            }
            foreach (GameItem item in items)
            {
                _backingInventory.Add(item);
                AddItemToGroupedInventory(item);
            }
        }
        #endregion
        #region Public functions
        /// <summary>
        /// Determines whether [has all these items] [the specified items].
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns>
        ///   <c>true</c> if [has all these items] [the specified items]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasAllTheseItems(IEnumerable<ItemQuantity> items)
        {
            return items.All(item => Items.Count(i => i.ItemTypeID == item.ItemID) >= item.Quantity);
        }
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Inventory AddItem(GameItem item)
        {
            return AddItems(new List<GameItem> { item });
        }
        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public Inventory AddItems(IEnumerable<GameItem> items)
        {
            return new Inventory(Items.Concat(items));
        }
        /// <summary>
        /// Removes the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Inventory RemoveItem(GameItem item)
        {
            return RemoveItems(new List<GameItem> { item });
        }
        /// <summary>
        /// Removes the items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public Inventory RemoveItems(IEnumerable<GameItem> items)
        {
            // REFACTOR: Look for a cleaner solution, with fewer temporary variables.
            List<GameItem> workingInventory = Items.ToList();
            IEnumerable<GameItem> itemsToRemove = items.ToList();
            foreach (GameItem item in itemsToRemove)
            {
                workingInventory.Remove(item);
            }
            return new Inventory(workingInventory);
        }
        /// <summary>
        /// Removes the items.
        /// </summary>
        /// <param name="itemQuantities">The item quantities.</param>
        /// <returns></returns>
        public Inventory RemoveItems(IEnumerable<ItemQuantity> itemQuantities)
        {
            // REFACTOR
            Inventory workingInventory = new Inventory(Items);
            foreach (ItemQuantity itemQuantity in itemQuantities)
            {
                for (int i = 0; i < itemQuantity.Quantity; i++)
                {
                    workingInventory =
                        workingInventory
                            .RemoveItem(workingInventory
                                .Items
                                .First(item => item.ItemTypeID == itemQuantity.ItemID));
                }
            }
            return workingInventory;
        }
        #endregion
        #region Private functions
        // REFACTOR: Look for a better way to do this (extension method?)
        /// <summary>
        /// Adds the item to grouped inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        private void AddItemToGroupedInventory(GameItem item)
        {
            if (item.IsUnique)
            {
                _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 1));
            }
            else
            {
                if (_backingGroupedInventoryItems.All(gi => gi.Item.ItemTypeID != item.ItemTypeID))
                {
                    _backingGroupedInventoryItems.Add(new GroupedInventoryItem(item, 0));
                }
                _backingGroupedInventoryItems.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity++;
            }
        }
        #endregion
    }
}
