using System.Collections;
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
        public const double MaximumWeight = 5_500_000;

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
            ContainerDeck = new ContainerDeck(length, width, height);
        }

        /// <summary>
        /// Sorts the containers onto the container deck.
        /// </summary>
        /// <param name="containers">The list of containers to sort onto the container deck.</param>
        public void Sort(List<Container> containers)
        {
            List<ValuableContainer> valuableContainers = containers.OfType<ValuableContainer>().ToList();
            List<ReeferContainer> reeferContainers = containers.OfType<ReeferContainer>().OrderByDescending(container => container.Weight).ToList();
            List<DryContainer> dryContainers = containers.OfType<DryContainer>().OrderByDescending(container => container.Weight).ToList();

            ContainerDeck.TryAddAllDryContainers(ref dryContainers);
            ContainerDeck.TryAddAllValuableContainers(ref valuableContainers);
            ContainerDeck.TryAddAllReeferContainers(ref reeferContainers);        }
    }
}
