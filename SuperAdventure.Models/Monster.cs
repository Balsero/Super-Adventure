using System.Collections.Generic;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SuperAdventure.Models.LivingEntity" />
    public class Monster : LivingEntity
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; }
        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        public string ImageName { get; }
        /// <summary>
        /// Gets the reward experience points.
        /// </summary>
        /// <value>
        /// The reward experience points.
        /// </value>
        public int RewardExperiencePoints { get; }
        /// <summary>
        /// Gets the loot table.
        /// </summary>
        /// <value>
        /// The loot table.
        /// </value>
        public List<ItemPercentage> LootTable { get; } =
            new List<ItemPercentage>();
        /// <summary>
        /// Initializes a new instance of the <see cref="Monster"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="imageName">Name of the image.</param>
        /// <param name="maximumHitPoints">The maximum hit points.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="currentWeapon">The current weapon.</param>
        /// <param name="rewardExperiencePoints">The reward experience points.</param>
        /// <param name="gold">The gold.</param>
        public Monster(int id, string name, string imageName,
                       int maximumHitPoints, IEnumerable<PlayerAttribute> attributes,
                       GameItem currentWeapon,
                       int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, maximumHitPoints, attributes, gold)
        {
            ID = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
        }
        /// <summary>
        /// Adds the item to loot table.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="percentage">The percentage.</param>
        public void AddItemToLootTable(int id, int percentage)
        {
            // Remove the entry from the loot table,
            // if it already contains an entry with this ID
            LootTable.RemoveAll(ip => ip.ID == id);
            LootTable.Add(new ItemPercentage(id, percentage));
        }
        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Monster Clone()
        {
            Monster newMonster =
                new Monster(ID, Name, ImageName, MaximumHitPoints, Attributes,
                    CurrentWeapon, RewardExperiencePoints, Gold);
            newMonster.LootTable.AddRange(LootTable);
            return newMonster;
        }
    }
}
