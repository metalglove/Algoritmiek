using System;

namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Provides easy construction of containers.
    /// </summary>
    public static class ContainerFactory
    {
        /// <summary>
        /// Creates a new instance of a container based on the type of freight.
        /// </summary>
        /// <param name="freight">The freight for a new container.</param>
        /// <returns>A container based on the type of freight; otherwise, throws a <see cref="ArgumentException"/></returns>
        /// <exception cref="ArgumentOutOfRangeException">If freight type is not given correctly or weight is too high and exceeds the maximum weight for a container.</exception>
        public static Container Create(Freight freight)
        {
            if (freight.Weight > 26_000)
                throw new ArgumentOutOfRangeException(nameof(freight));
            Container container; 
            switch (freight.FreightType)
            {
                case FreightType.Dry:
                    container = new DryContainer(freight);
                    break;
                case FreightType.Valuable:
                    container = new ValuableContainer(freight);
                    break;
                case FreightType.Refrigerated:
                    container = new ReeferContainer(freight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return container;
        }
    }
}
