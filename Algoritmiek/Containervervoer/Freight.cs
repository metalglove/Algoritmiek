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

        /// <summary>
        /// Initializes a new instance of the <see cref="Freight"/> struct.
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="freightType"></param>
        public Freight(double weight, FreightType freightType)
        {
            Weight = weight;
            FreightType = freightType;
        }
    }
}
