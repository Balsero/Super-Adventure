namespace SuperAdventure.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class World
    {
        /// <summary>
        /// The locations
        /// </summary>
        private readonly List<Location> _locations = new List<Location>();
        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="location">The location.</param>
        public void AddLocation(Location location)
        {
            _locations.Add(location);
        }
        /// <summary>
        /// Locations at.
        /// </summary>
        /// <param name="xCoordinate">The x coordinate.</param>
        /// <param name="yCoordinate">The y coordinate.</param>
        /// <returns></returns>
        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in _locations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }
            return null;
        }
    }
}