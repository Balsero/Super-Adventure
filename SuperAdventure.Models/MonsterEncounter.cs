using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MonsterEncounter
    {
        /// <summary>
        /// Gets the monster identifier.
        /// </summary>
        /// <value>
        /// The monster identifier.
        /// </value>
        public int MonsterID { get;}
        /// <summary>
        /// Gets or sets the chance of encountering.
        /// </summary>
        /// <value>
        /// The chance of encountering.
        /// </value>
        public int ChanceOfEncountering { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MonsterEncounter"/> class.
        /// </summary>
        /// <param name="monsterID">The monster identifier.</param>
        /// <param name="chanceOfEncountering">The chance of encountering.</param>
        public MonsterEncounter(int monsterID, int chanceOfEncountering)
        {
            MonsterID = monsterID;
            ChanceOfEncountering = chanceOfEncountering;
        }
    }
}
