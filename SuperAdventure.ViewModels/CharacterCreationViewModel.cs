using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SuperAdventure.Services.Factories;
using SuperAdventure.Models;
using SuperAdventure.Services;
namespace SuperAdventure.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class CharacterCreationViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets the game details.
        /// </summary>
        /// <value>
        /// The game details.
        /// </value>
        public GameDetails GameDetails { get; }
        /// <summary>
        /// Gets the selected race.
        /// </summary>
        /// <value>
        /// The selected race.
        /// </value>
        public Race SelectedRace { get; init; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; init; }
        /// <summary>
        /// Gets the player attributes.
        /// </summary>
        /// <value>
        /// The player attributes.
        /// </value>
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; } =
            new ObservableCollection<PlayerAttribute>();
        /// <summary>
        /// Gets a value indicating whether this instance has races.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has races; otherwise, <c>false</c>.
        /// </value>
        public bool HasRaces =>
            GameDetails.Races.Any();
        /// <summary>
        /// Gets a value indicating whether this instance has race attribute modifiers.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has race attribute modifiers; otherwise, <c>false</c>.
        /// </value>
        public bool HasRaceAttributeModifiers =>
            HasRaces && GameDetails.Races.Any(r => r.PlayerAttributeModifiers.Any());
        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCreationViewModel"/> class.
        /// </summary>
        public CharacterCreationViewModel()
        {
            GameDetails = GameDetailsService.ReadGameDetails();
            if (HasRaces)
            {
                SelectedRace = GameDetails.Races.First();
            }

            RollNewCharacter();
        }
        /// <summary>
        /// Rolls the new character.
        /// </summary>
        public void RollNewCharacter()
        {
            PlayerAttributes.Clear();
            foreach (PlayerAttribute playerAttribute in GameDetails.PlayerAttributes)
            {
                playerAttribute.ReRoll();
                PlayerAttributes.Add(playerAttribute);
            }

            ApplyAttributeModifiers();
        }
        /// <summary>
        /// Applies the attribute modifiers.
        /// </summary>
        public void ApplyAttributeModifiers()
        {
            foreach (PlayerAttribute playerAttribute in PlayerAttributes)
            {
                var attributeRaceModifier =
                    SelectedRace.PlayerAttributeModifiers
                                .FirstOrDefault(pam => pam.AttributeKey.Equals(playerAttribute.Key));
                playerAttribute.ModifiedValue =
                    playerAttribute.BaseValue + (attributeRaceModifier?.Modifier ?? 0);
            }
        }
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            Player player = new Player(Name, 0, 10, 10, PlayerAttributes, 10);
            // Give player default inventory items, weapons, recipes, etc.
            player.AddItemToInventory(ItemFactory.CreateGameItem(1001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(2001));
            player.LearnRecipe(RecipeFactory.RecipeByID(1));
            player.LearnRecipe(RecipeFactory.RecipeByID(2));
            player.LearnRecipe(RecipeFactory.RecipeByID(3));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3002));
            player.AddItemToInventory(ItemFactory.CreateGameItem(3003));
            return player;
        }
    }
}
