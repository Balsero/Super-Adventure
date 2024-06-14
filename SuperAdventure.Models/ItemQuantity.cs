namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ItemQuantity
    {
        /// <summary>
        /// The game item
        /// </summary>
        private readonly GameItem _gameItem;
        /// <summary>
        /// Gets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        public int ItemID => _gameItem.ItemTypeID;
        /// <summary>
        /// Gets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; }
        /// <summary>
        /// Gets the quantity item description.
        /// </summary>
        /// <value>
        /// The quantity item description.
        /// </value>
        public string QuantityItemDescription =>
            $"{Quantity} {_gameItem.Name}";
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemQuantity"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="quantity">The quantity.</param>
        public ItemQuantity(GameItem item, int quantity)
        {
            _gameItem = item;
            Quantity = quantity;
        }
    }
}
