using System.ComponentModel;
using SuperAdventure.Core;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class PlayerAttribute : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; }
        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; }
        /// <summary>
        /// Gets the dice notation.
        /// </summary>
        /// <value>
        /// The dice notation.
        /// </value>
        public string DiceNotation { get; }
        /// <summary>
        /// Gets or sets the base value.
        /// </summary>
        /// <value>
        /// The base value.
        /// </value>
        public int BaseValue { get; set; }
        /// <summary>
        /// Gets or sets the modified value.
        /// </summary>
        /// <value>
        /// The modified value.
        /// </value>
        public int ModifiedValue { get; set; }
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        // Constructor that will use DiceService to create a BaseValue.
        // The constructor this calls will put that same value into BaseValue and ModifiedValue
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="diceNotation">The dice notation.</param>
        public PlayerAttribute(string key, string displayName, string diceNotation)
            : this(key, displayName, diceNotation, DiceService.Instance.Roll(diceNotation).Value)
        {
        }
        // Constructor that takes a baseValue and also uses it for modifiedValue,
        // for when we're creating a new attribute
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="diceNotation">The dice notation.</param>
        /// <param name="baseValue">The base value.</param>
        public PlayerAttribute(string key, string displayName, string diceNotation,
                               int baseValue) :
            this(key, displayName, diceNotation, baseValue, baseValue)
        {
        }
        // This constructor is eventually called by the others, 
        // or used when reading a Player's attributes from a saved game file.
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAttribute"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="diceNotation">The dice notation.</param>
        /// <param name="baseValue">The base value.</param>
        /// <param name="modifiedValue">The modified value.</param>
        public PlayerAttribute(string key, string displayName, string diceNotation,
                               int baseValue, int modifiedValue)
        {
            Key = key;
            DisplayName = displayName;
            DiceNotation = diceNotation;
            BaseValue = baseValue;
            ModifiedValue = modifiedValue;
        }
        /// <summary>
        /// Res the roll.
        /// </summary>
        public void ReRoll()
        {
            BaseValue = DiceService.Instance.Roll(DiceNotation).Value;
            ModifiedValue = BaseValue;
        }
    }
}
