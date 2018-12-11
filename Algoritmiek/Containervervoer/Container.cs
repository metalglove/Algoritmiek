namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents a standardized container for shipping.
    /// </summary>
    public class Container
    {
        /// <summary>
        /// The freight for the container.
        /// </summary>
        private readonly Freight _freight;

        /// <summary>
        /// The default weight of the container in kilograms.
        /// </summary>
        private const int DefaultWeight = 4_000;

        /// <summary>
        /// The maximum weight of the container with contents in kilograms.
        /// </summary>
        private const int MaxWeight = 30_000;

        public Container(Freight freight)
        {
            _freight = freight;
        }
    }
}
