
namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>
        /// The player.
        /// </value>
        public Player Player { get; init; }
        /// <summary>
        /// Gets the x coordinate.
        /// </summary>
        /// <value>
        /// The x coordinate.
        /// </value>
        public int XCoordinate { get; init; }
        /// <summary>
        /// Gets the y coordinate.
        /// </summary>
        /// <value>
        /// The y coordinate.
        /// </value>
        public int YCoordinate { get; init; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="xCoordinate">The x coordinate.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        public GameState(Player player, int xCoordinate, int yCoordinate)
        {
            Player = player;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }
    }
}
