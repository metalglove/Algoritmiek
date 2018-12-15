using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents the cargo ship also known as a freighter.
    /// </summary>
    public class Freighter
    {
        /// <summary>
        /// The maximum weight in kilograms.
        /// </summary>
        public const double MaximumWeight = 2_250_000;

        /// <summary>
        /// Gets the container deck.
        /// </summary>
        public ContainerDeck ContainerDeck { get; }

        /// <summary>
        /// Gets the total weight of the freighter.
        /// </summary>
        public double Weight => ContainerDeck.Weight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Freighter"/> class.
        /// </summary>
        /// <param name="length">The desired maximum length of the container deck.</param>
        /// <param name="width">The desired maximum width of the container deck.</param>
        /// <param name="height">The desired maximum height of the container deck.</param>
        public Freighter(int length, int width, int height)
        {
            ContainerDeck = new ContainerDeck(this, length, width, height);
        }

        /// <summary>
        /// Sorts the containers onto the container deck.
        /// </summary>
        /// <param name="containers">The list of containers to sort onto the container deck.</param>
        /// <param name="minValuableContainers">The minimum required valuable containers to be placed.</param>
        /// <param name="minReeferContainers">The minimum required reefer containers to be placed.</param>
        /// <param name="minDryContainers">The minimum required dry containers to be placed.</param>
        public void Sort(List<Container> containers, int minDryContainers, int minValuableContainers, int minReeferContainers)
        {
            List<ValuableContainer> valuableContainers = containers.OfType<ValuableContainer>().OrderByDescending(container => container.Weight).ToList();
            List<ReeferContainer> reeferContainers = containers.OfType<ReeferContainer>().OrderByDescending(container => container.Weight).ToList();
            List<DryContainer> dryContainers = containers.OfType<DryContainer>().OrderByDescending(container => container.Weight).ToList();

            int dryContainerCount = dryContainers.Count;
            int valuableContainerCount = valuableContainers.Count;
            int reeferContainerCount = reeferContainers.Count;

            if (minDryContainers > dryContainerCount)
                throw new ArgumentOutOfRangeException(nameof(minDryContainers));
            if (minValuableContainers > valuableContainerCount || minValuableContainers > ContainerDeck.Width * ContainerDeck.Length)
                throw new ArgumentOutOfRangeException(nameof(minValuableContainers));
            if (minReeferContainers > reeferContainerCount)
                throw new ArgumentOutOfRangeException(nameof(minReeferContainers));

            ContainerDeck.TryAddAllContainers(ref dryContainers);
            ContainerDeck.TryAddAllContainers(ref reeferContainers);

            int dryContainersPlacedCount = dryContainerCount - dryContainers.Count;
            if (dryContainersPlacedCount < minDryContainers)
            {
                int minimumContainersToPlace = minDryContainers - dryContainersPlacedCount;
                ContainerDeck.TryAddContainersOnTop(ref dryContainers, minimumContainersToPlace);
            }

            ContainerDeck.TryAddAllContainers(ref valuableContainers);

            int valuableContainersPlacedCount = valuableContainerCount - valuableContainers.Count;
            if (valuableContainersPlacedCount < minValuableContainers)
            {
                int minimumContainersToPlace = minValuableContainers - valuableContainersPlacedCount;
                ContainerDeck.TryAddContainersOnTop(ref valuableContainers, minimumContainersToPlace);
            }

            int minimumReeferContainersToPlace = reeferContainers.Count; // does not need to check for minimum any more because this is the last time we are adding containers.
            ContainerDeck.TryAddContainersOnTop(ref reeferContainers, minimumReeferContainersToPlace);
        }

        /// <summary>
        /// Checks if the container would exceed the maximum weight if it would be added on the container deck.
        /// </summary>
        /// <param name="container">The container to check the weight for.</param>
        /// <returns><c>true</c> if the weight of the container + the current weight of the container deck exceeds the maximum weight; otherwise, <c>false</c>.</returns>
        public bool CheckIfContainerExceedsMaximumWeight(Container container)
        {
            return container.Weight + Weight > MaximumWeight;
        }
    }
}
