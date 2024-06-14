using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Newtonsoft.Json;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class LivingEntity : INotifyPropertyChanged
    {
        #region Properties
        /// <summary>
        /// The current weapon
        /// </summary>
        private GameItem _currentWeapon;
        /// <summary>
        /// The current consumable
        /// </summary>
        private GameItem _currentConsumable;
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public ObservableCollection<PlayerAttribute> Attributes { get; } =
            new ObservableCollection<PlayerAttribute>();
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
        /// <summary>
        /// Gets the current hit points.
        /// </summary>
        /// <value>
        /// The current hit points.
        /// </value>
        public int CurrentHitPoints { get; private set; }
        /// <summary>
        /// Gets or sets the maximum hit points.
        /// </summary>
        /// <value>
        /// The maximum hit points.
        /// </value>
        public int MaximumHitPoints { get; protected set; }
        /// <summary>
        /// Gets the gold.
        /// </summary>
        /// <value>
        /// The gold.
        /// </value>
        public int Gold { get; private set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; protected set; }
        /// <summary>
        /// Gets the inventory.
        /// </summary>
        /// <value>
        /// The inventory.
        /// </value>
        public Inventory Inventory { get; private set; }
        /// <summary>
        /// Gets or sets the current weapon.
        /// </summary>
        /// <value>
        /// The current weapon.
        /// </value>
        public GameItem CurrentWeapon
        {
            get => _currentWeapon;
            set
            {
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }
                _currentWeapon = value;
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
            }
        }
        /// <summary>
        /// Gets or sets the current consumable.
        /// </summary>
        /// <value>
        /// The current consumable.
        /// </value>
        public GameItem CurrentConsumable
        {
            get => _currentConsumable;
            set
            {
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }
                _currentConsumable = value;
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance is alive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is alive; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsAlive => CurrentHitPoints > 0;
        /// <summary>
        /// Gets a value indicating whether this instance is dead.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is dead; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsDead => !IsAlive;
        #endregion
        /// <summary>
        /// Occurs when [on action performed].
        /// </summary>
        public event EventHandler<string> OnActionPerformed;
        /// <summary>
        /// Occurs when [on killed].
        /// </summary>
        public event EventHandler OnKilled;
        /// <summary>
        /// Initializes a new instance of the <see cref="LivingEntity"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="maximumHitPoints">The maximum hit points.</param>
        /// <param name="currentHitPoints">The current hit points.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="gold">The gold.</param>
        /// <param name="level">The level.</param>
        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints,
                               IEnumerable<PlayerAttribute> attributes, int gold, int level = 1)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;
            foreach (PlayerAttribute attribute in attributes)
            {
                Attributes.Add(attribute);
            }
            Inventory = new Inventory();
        }
        /// <summary>
        /// Uses the current weapon on.
        /// </summary>
        /// <param name="target">The target.</param>
        public void UseCurrentWeaponOn(LivingEntity target)
        {
            CurrentWeapon.PerformAction(this, target);
        }
        /// <summary>
        /// Uses the current consumable.
        /// </summary>
        public void UseCurrentConsumable()
        {
            CurrentConsumable.PerformAction(this, this);
            RemoveItemFromInventory(CurrentConsumable);
        }
        /// <summary>
        /// Takes the damage.
        /// </summary>
        /// <param name="hitPointsOfDamage">The hit points of damage.</param>
        public void TakeDamage(int hitPointsOfDamage)
        {
            CurrentHitPoints -= hitPointsOfDamage;
            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }
        /// <summary>
        /// Heals the specified hit points to heal.
        /// </summary>
        /// <param name="hitPointsToHeal">The hit points to heal.</param>
        public void Heal(int hitPointsToHeal)
        {
            CurrentHitPoints += hitPointsToHeal;
            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }
        /// <summary>
        /// Completelies the heal.
        /// </summary>
        public void CompletelyHeal()
        {
            CurrentHitPoints = MaximumHitPoints;
        }
        /// <summary>
        /// Receives the gold.
        /// </summary>
        /// <param name="amountOfGold">The amount of gold.</param>
        public void ReceiveGold(int amountOfGold)
        {
            Gold += amountOfGold;
        }
        public void SpendGold(int amountOfGold)
        {
            if (amountOfGold > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }
            Gold -= amountOfGold;
        }
        /// <summary>
        /// Adds the item to inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddItemToInventory(GameItem item)
        {
            Inventory = Inventory.AddItem(item);
        }
        /// <summary>
        /// Removes the item from inventory.
        /// </summary>
        /// <param name="item">The item.</param>
        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory = Inventory.RemoveItem(item);
        }
        /// <summary>
        /// Removes the items from inventory.
        /// </summary>
        /// <param name="itemQuantities">The item quantities.</param>
        public void RemoveItemsFromInventory(IEnumerable<ItemQuantity> itemQuantities)
        {
            Inventory = Inventory.RemoveItems(itemQuantities);
        }
        #region Private functions
        /// <summary>
        /// Raises the on killed event.
        /// </summary>
        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }
        /// <summary>
        /// Raises the action performed event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="result">The result.</param>
        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
        #endregion
    }
}