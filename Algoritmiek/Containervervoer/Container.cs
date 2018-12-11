namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents a standardized container for shipping.
    /// </summary>
    public abstract class Container : IContainer
    {
        /// <summary>
        /// Gets the freight for the container.
        /// </summary>
        public Freight Freight { get; }

        /// <summary>
        /// Gets the current weight of the container.
        /// </summary>
        public double Weight => Freight.Weight + DefaultWeight;

        /// <summary>
        /// The default weight of the container in kilograms.
        /// </summary>
        public const double DefaultWeight = 4_000;

        /// <summary>
        /// The maximum weight of the container with contents in kilograms.
        /// </summary>
        public const double MaxWeight = 30_000;

        public Container(Freight freight)
        {
            Freight = freight;
        }
    }
}
