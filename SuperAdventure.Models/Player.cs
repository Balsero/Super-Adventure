using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SuperAdventure.Models.LivingEntity" />
    public class Player : LivingEntity
    {
        #region Properties
        /// <summary>
        /// The experience points
        /// </summary>
        private int _experiencePoints;
        /// <summary>
        /// Gets the experience points.
        /// </summary>
        /// <value>
        /// The experience points.
        /// </value>
        public int ExperiencePoints
        {
            get => _experiencePoints;
            private set
            {
                _experiencePoints = value;
                
                SetLevelAndMaximumHitPoints();
            }
        }
        /// <summary>
        /// Gets the quests.
        /// </summary>
        /// <value>
        /// The quests.
        /// </value>
        public ObservableCollection<QuestStatus> Quests { get; } =
            new ObservableCollection<QuestStatus>();
        /// <summary>
        /// Gets the recipes.
        /// </summary>
        /// <value>
        /// The recipes.
        /// </value>
        public ObservableCollection<Recipe> Recipes { get; } =
            new ObservableCollection<Recipe>();
        #endregion
        /// <summary>
        /// Occurs when [on leveled up].
        /// </summary>
        public event EventHandler OnLeveledUp;
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="experiencePoints">The experience points.</param>
        /// <param name="maximumHitPoints">The maximum hit points.</param>
        /// <param name="currentHitPoints">The current hit points.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="gold">The gold.</param>
        public Player(string name, int experiencePoints,
                      int maximumHitPoints, int currentHitPoints,
                      IEnumerable<PlayerAttribute> attributes, int gold) :
            base(name, maximumHitPoints, currentHitPoints, attributes, gold)
        {
            ExperiencePoints = experiencePoints;
        }
        /// <summary>
        /// Adds the experience.
        /// </summary>
        /// <param name="experiencePoints">The experience points.</param>
        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }
        /// <summary>
        /// Learns the recipe.
        /// </summary>
        /// <param name="recipe">The recipe.</param>
        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }
        /// <summary>
        /// Sets the level and maximum hit points.
        /// </summary>
        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;
            Level = (ExperiencePoints / 100) + 1;
            if (Level != originalLevel)
            {
                MaximumHitPoints = Level * 10;
                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
    }
}