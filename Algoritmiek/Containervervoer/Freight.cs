namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents 
    /// </summary>
    public struct Freight
    {
        /// <summary>
        /// The weight of the freight.
        /// </summary>
        public readonly double Weight;

        /// <summary>
        /// The type of freight.
        /// </summary>
        public readonly FreightType FreightType;

        public Freight(double weight, FreightType freightType = FreightType.Default)
        {
            Weight = weight;
            FreightType = freightType;
        }
    }
}
