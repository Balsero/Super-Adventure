using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        /// <value>
        /// The x coordinate.
        /// </value>
        public int XCoordinate { get; }
        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        /// <value>
        /// The y coordinate.
        /// </value>
        public int YCoordinate { get; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonIgnore]
        public string Name { get; }
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonIgnore]
        public string Description { get; }
        /// <summary>
        /// Gets the name of the image.
        /// </summary>
        /// <value>
        /// The name of the image.
        /// </value>
        [JsonIgnore]
        public string ImageName { get; }
        /// <summary>
        /// Gets the quests available here.
        /// </summary>
        /// <value>
        /// The quests available here.
        /// </value>
        [JsonIgnore]
        public List<Quest> QuestsAvailableHere { get; } = new List<Quest>();
        /// <summary>
        /// Gets the monsters here.
        /// </summary>
        /// <value>
        /// The monsters here.
        /// </value>
        [JsonIgnore]
        public List<MonsterEncounter> MonstersHere { get; } =
            new List<MonsterEncounter>();
        /// <summary>
        /// Gets or sets the trader here.
        /// </summary>
        /// <value>
        /// The trader here.
        /// </value>
        [JsonIgnore]
        public Trader TraderHere { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="xCoordinate">The x coordinate.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="imageName">Name of the image.</param>
        public Location(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
        }
        /// <summary>
        /// Adds the monster.
        /// </summary>
        /// <param name="monsterID">The monster identifier.</param>
        /// <param name="chanceOfEncountering">The chance of encountering.</param>
        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if (MonstersHere.Exists(m => m.MonsterID == monsterID))
            {
                // This monster has already been added to this location.
                // So, overwrite the ChanceOfEncountering with the new number.
                MonstersHere.First(m => m.MonsterID == monsterID)
                            .ChanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                // This monster is not already at this location, so add it.
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }
    }
}