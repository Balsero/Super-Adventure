using System.Collections.Generic;

namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GameDetails
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; }
        /// <summary>
        /// Gets the sub title.
        /// </summary>
        /// <value>
        /// The sub title.
        /// </value>
        public string SubTitle { get; }
        /// <summary>
        /// Gets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; }

        /// <summary>
        /// Gets the player attributes.
        /// </summary>
        /// <value>
        /// The player attributes.
        /// </value>
        public List<PlayerAttribute> PlayerAttributes { get; } =
            new List<PlayerAttribute>();
        /// <summary>
        /// Gets the races.
        /// </summary>
        /// <value>
        /// The races.
        /// </value>
        public List<Race> Races { get; } =
            new List<Race>();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDetails"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="subTitle">The sub title.</param>
        /// <param name="version">The version.</param>
        public GameDetails(string title, string subTitle, string version)
        {
            Title = title;
            SubTitle = subTitle;
            Version = version;
        }
    }
}
