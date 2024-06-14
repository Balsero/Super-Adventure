using System;
using SuperAdventure.Models;
namespace SuperAdventure.Models.Actions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SuperAdventure.Models.Actions.BaseAction" />
    /// <seealso cref="SuperAdventure.Models.Actions.IAction" />
    public class Heal : BaseAction, IAction
    {
        /// <summary>
        /// The hit points to heal
        /// </summary>
        private readonly int _hitPointsToHeal;
        public Heal(GameItem itemInUse, int hitPointsToHeal)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{itemInUse.Name} is not consumable");
            }
            _hitPointsToHeal = hitPointsToHeal;
        }
        /// <summary>
        /// Executes the specified actor.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="target">The target.</param>
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name.ToLower()}";
            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal > 1 ? "s" : "")}.");
            target.Heal(_hitPointsToHeal);
        }
    }
}