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
    public class ItemPercentage
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; }
        /// <summary>
        /// Gets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public int Percentage { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemPercentage"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="percentage">The percentage.</param>
        public ItemPercentage(int id, int percentage)
        {
            ID = id;
            Percentage = percentage;
        }
    }
}
