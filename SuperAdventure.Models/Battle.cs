using SuperAdventure.Models.EventArgs;
using SuperAdventure.Core;
using SuperAdventure.Models.Shared;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class Battle : IDisposable
    {
        /// <summary>
        /// The message broker
        /// </summary>
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        /// <summary>
        /// The player
        /// </summary>
        private readonly Player _player;
        /// <summary>
        /// The opponent
        /// </summary>
        private readonly Monster _opponent;
        /// <summary>
        /// 
        /// </summary>
        private enum Combatant
        {
            /// <summary>
            /// The player
            /// </summary>
            Player,
            /// <summary>
            /// The opponent
            /// </summary>
            Opponent
        }
        /// <summary>
        /// Occurs when [on combat victory].
        /// </summary>
        public event EventHandler<CombatVictoryEventArgs> OnCombatVictory;
        /// <summary>
        /// Initializes a new instance of the <see cref="Battle"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="opponent">The opponent.</param>
        public Battle(Player player, Monster opponent)
        {
            _player = player;
            _opponent = opponent;
            _player.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnKilled += OnOpponentKilled;
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You see a {_opponent.Name} here!");
            if (FirstAttacker(_player, _opponent) == Combatant.Opponent)
            {
                AttackPlayer();
            }
        }
        /// <summary>
        /// Attacks the opponent.
        /// </summary>
        public void AttackOpponent()
        {
            if (_player.CurrentWeapon == null)
            {
                _messageBroker.RaiseMessage("You must select a weapon, to attack.");
                return;
            }
            _player.UseCurrentWeaponOn(_opponent);
            if (_opponent.IsAlive)
            {
                AttackPlayer();
            }
        }
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _player.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnKilled -= OnOpponentKilled;
        }
        /// <summary>
        /// Called when [opponent killed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnOpponentKilled(object sender, System.EventArgs e)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You defeated the {_opponent.Name}!");
            _messageBroker.RaiseMessage($"You receive {_opponent.RewardExperiencePoints} experience points.");
            _player.AddExperience(_opponent.RewardExperiencePoints);
            _messageBroker.RaiseMessage($"You receive {_opponent.Gold} gold.");
            _player.ReceiveGold(_opponent.Gold);
            foreach (GameItem gameItem in _opponent.Inventory.Items)
            {
                _messageBroker.RaiseMessage($"You receive one {gameItem.Name}.");
                _player.AddItemToInventory(gameItem);
            }
            OnCombatVictory?.Invoke(this, new CombatVictoryEventArgs());
        }
        /// <summary>
        /// Attacks the player.
        /// </summary>
        private void AttackPlayer()
        {
            _opponent.UseCurrentWeaponOn(_player);
        }
        /// <summary>
        /// Called when [combatant action performed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="result">The result.</param>
        private void OnCombatantActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }
        /// <summary>
        /// Firsts the attacker.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="opponent">The opponent.</param>
        /// <returns></returns>
        private static Combatant FirstAttacker(Player player, Monster opponent)
        {
            // Formula is: ((Dex(player)^2 - Dex(monster)^2)/10) + Random(-10/10)
            // For dexterity values from 3 to 18, this should produce an offset of +/- 41.5
            int playerDexterity = player.GetAttribute("DEX").ModifiedValue *
                                  player.GetAttribute("DEX").ModifiedValue;
            int opponentDexterity = opponent.GetAttribute("DEX").ModifiedValue *
                                    opponent.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;
            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset
                ? Combatant.Player
                : Combatant.Opponent;
        }
    }
}