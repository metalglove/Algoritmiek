using System;

namespace Algoritmiek.Containervervoer
{
    public static class ContainerFactory
    {
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
