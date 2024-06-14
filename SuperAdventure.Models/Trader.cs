
using System.Collections.Generic;
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SuperAdventure.Models.LivingEntity" />
    public class Trader : LivingEntity
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Trader"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Trader(int id, string name) : base(name, 9999, 9999, new List<PlayerAttribute>(), 9999)
        {
            ID = id;
        }
    }
}