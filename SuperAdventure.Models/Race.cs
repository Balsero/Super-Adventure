using System.Collections.Generic;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Race
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets the player attribute modifiers.
        /// </summary>
        /// <value>
        /// The player attribute modifiers.
        /// </value>
        public List<PlayerAttributeModifier> PlayerAttributeModifiers { get; } =
            new List<PlayerAttributeModifier>();
    }
}
