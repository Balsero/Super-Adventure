using System.ComponentModel;
using System.Linq;
using SuperAdventure.Services.Factories;
using SuperAdventure.Models;
using SuperAdventure.Services;
using Newtonsoft.Json;
using SuperAdventure.Core;
namespace SuperAdventure.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class GameSession : INotifyPropertyChanged
    {
        /// <summary>
        /// The message broker
        /// </summary>
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        #region Properties
        /// <summary>
        /// The current player
        /// </summary>
        private Player _currentPlayer;
        /// <summary>
        /// The current location
        /// </summary>
        private Location _currentLocation;
        /// <summary>
        /// The current battle
        /// </summary>
        private Battle _currentBattle;
        /// <summary>
        /// The current monster
        /// </summary>
        private Monster _currentMonster;
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
        [JsonIgnore]
        public GameDetails GameDetails { get; private set; }
        /// <summary>
        /// Gets the current world.
        /// </summary>
        /// <value>
        /// The current world.
        /// </value>
        [JsonIgnore]
        public World CurrentWorld { get; }
        /// <summary>
        /// Gets or sets the current player.
        /// </summary>
        /// <value>
        /// The current player.
        /// </value>
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled -= OnPlayerKilled;
                }
                _currentPlayer = value;
                if (_currentPlayer != null)
                {
                    _currentPlayer.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _currentPlayer.OnKilled += OnPlayerKilled;
                }
            }
        }
        /// <summary>
        /// Gets or sets the current location.
        /// </summary>
        /// <value>
        /// The current location.
        /// </value>
        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;
                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                CurrentMonster = MonsterFactory.GetMonsterFromLocation(CurrentLocation);
                CurrentTrader = CurrentLocation.TraderHere;
            }
        }
        /// <summary>
        /// Gets or sets the current monster.
        /// </summary>
        /// <value>
        /// The current monster.
        /// </value>
        [JsonIgnore]
        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                if (_currentBattle != null)
                {
                    _currentBattle.OnCombatVictory -= OnCurrentMonsterKilled;
                    _currentBattle.Dispose();
                    _currentBattle = null;
                }
                _currentMonster = value;
                if (_currentMonster != null)
                {
                    _currentBattle = new Battle(CurrentPlayer, CurrentMonster);
                    _currentBattle.OnCombatVictory += OnCurrentMonsterKilled;
                }
            }
        }
        /// <summary>
        /// Gets the current trader.
        /// </summary>
        /// <value>
        /// The current trader.
        /// </value>
        [JsonIgnore]
        public Trader CurrentTrader { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this instance has location to north.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has location to north; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasLocationToNorth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
        /// <summary>
        /// Gets a value indicating whether this instance has location to east.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has location to east; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasLocationToEast =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
        /// <summary>
        /// Gets a value indicating whether this instance has location to south.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has location to south; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasLocationToSouth =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        /// <summary>
        /// Gets a value indicating whether this instance has location to west.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has location to west; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasLocationToWest =>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
        /// <summary>
        /// Gets a value indicating whether this instance has monster.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has monster; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasMonster => CurrentMonster != null;
        /// <summary>
        /// Gets a value indicating whether this instance has trader.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has trader; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool HasTrader => CurrentTrader != null;
        #endregion
        /// <summary>
        /// Initializes a new instance of the <see cref="GameSession"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="xCoordinate">The x coordinate.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        public GameSession(Player player, int xCoordinate, int yCoordinate)
        {
            PopulateGameDetails();
            CurrentWorld = WorldFactory.CreateWorld();
            CurrentPlayer = player;
            CurrentLocation = CurrentWorld.LocationAt(xCoordinate, yCoordinate);
        }
        /// <summary>
        /// Moves the north.
        /// </summary>
        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
        /// <summary>
        /// Moves the east.
        /// </summary>
        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }
        /// <summary>
        /// Moves the south.
        /// </summary>
        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }
        /// <summary>
        /// Moves the west.
        /// </summary>
        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }
        /// <summary>
        /// Populates the game details.
        /// </summary>
        private void PopulateGameDetails()
        {
            GameDetails = GameDetailsService.ReadGameDetails();
        }
        /// <summary>
        /// Completes the quests at location.
        /// </summary>
        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID &&
                                                             !q.IsCompleted);
                if (questToComplete != null)
                {
                    if (CurrentPlayer.Inventory.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        CurrentPlayer.RemoveItemsFromInventory(quest.ItemsToComplete);
                        _messageBroker.RaiseMessage("");
                        _messageBroker.RaiseMessage($"You completed the '{quest.Name}' quest");
                        // Give the player the quest rewards
                        _messageBroker.RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.AddExperience(quest.RewardExperiencePoints);
                        _messageBroker.RaiseMessage($"You receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);
                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            _messageBroker.RaiseMessage($"You receive a {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                        }
                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }
        /// <summary>
        /// Gives the player quests at location.
        /// </summary>
        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage($"You receive the '{quest.Name}' quest");
                    _messageBroker.RaiseMessage(quest.Description);
                    _messageBroker.RaiseMessage("Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        _messageBroker
                            .RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                    _messageBroker.RaiseMessage("And you will receive:");
                    _messageBroker.RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    _messageBroker.RaiseMessage($"   {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        _messageBroker
                            .RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }
        /// <summary>
        /// Attacks the current monster.
        /// </summary>
        public void AttackCurrentMonster()
        {
            _currentBattle?.AttackOpponent();
        }
        /// <summary>
        /// Uses the current consumable.
        /// </summary>
        public void UseCurrentConsumable()
        {
            if (CurrentPlayer.CurrentConsumable != null)
            {
                if (_currentBattle == null)
                {
                    CurrentPlayer.OnActionPerformed += OnConsumableActionPerformed;
                }
                CurrentPlayer.UseCurrentConsumable();
                if (_currentBattle == null)
                {
                    CurrentPlayer.OnActionPerformed -= OnConsumableActionPerformed;
                }
            }
        }
        /// <summary>
        /// Called when [consumable action performed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="result">The result.</param>
        private void OnConsumableActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }
        /// <summary>
        /// Crafts the item using.
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        public void CraftItemUsing(Recipe recipe)
        {
            if (CurrentPlayer.Inventory.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveItemsFromInventory(recipe.Ingredients);
                foreach (ItemQuantity itemQuantity in recipe.OutputItems)
                {
                    for (int i = 0; i < itemQuantity.Quantity; i++)
                    {
                        GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(outputItem);
                        _messageBroker.RaiseMessage($"You craft 1 {outputItem.Name}");
                    }
                }
            }
            else
            {
                _messageBroker.RaiseMessage("You do not have the required ingredients:");
                foreach (ItemQuantity itemQuantity in recipe.Ingredients)
                {
                    _messageBroker
                        .RaiseMessage($"  {itemQuantity.QuantityItemDescription}");
                }
            }
        }
        /// <summary>
        /// Called when [player killed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnPlayerKilled(object sender, System.EventArgs e)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage("You have been killed.");
            CurrentLocation = CurrentWorld.LocationAt(0, -1);
            CurrentPlayer.CompletelyHeal();
        }
        /// <summary>
        /// Called when [current monster killed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            // Get another monster to fight
            CurrentMonster = MonsterFactory.GetMonsterFromLocation(CurrentLocation);
        }
        /// <summary>
        /// Called when [current player leveled up].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }
    }
}