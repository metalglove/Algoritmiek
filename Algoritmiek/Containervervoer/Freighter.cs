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
            List<ValuableContainer> valuableContainers = containers.OfType<ValuableContainer>().ToList();
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

            ContainerDeck.TryAddAllDryContainers(ref dryContainers);
            ContainerDeck.TryAddAllReeferContainers(ref reeferContainers);
            int dryContainersPlacedCount = dryContainerCount - dryContainers.Count;
            if (dryContainersPlacedCount < minDryContainers)
            {
                int minimumContainersToPlace = minDryContainers - dryContainersPlacedCount;
                ContainerDeck.TryAddDryContainersOnTop(ref dryContainers, minimumContainersToPlace, minValuableContainers);
                dryContainersPlacedCount = dryContainerCount - dryContainers.Count;
            }

            ContainerDeck.TryAddAllValuableContainers(ref valuableContainers);

            int valuableContainersPlacedCount = valuableContainerCount - valuableContainers.Count;
            if (valuableContainersPlacedCount < minValuableContainers)
            {
                int minimumContainersToPlace = minValuableContainers - valuableContainersPlacedCount;
                //ContainerDeck.TryAddValuableContainersOnTopOfReeferContainers(ref valuableContainers, minimumContainersToPlace, minReeferContainers);
                valuableContainersPlacedCount = valuableContainerCount - valuableContainers.Count;
            }

            int reeferContainersPlacedCount = reeferContainerCount - reeferContainers.Count;
            if (reeferContainersPlacedCount < minReeferContainers)
            {
                int minimumContainersToPlace = minReeferContainers - reeferContainersPlacedCount;
                //ContainerDeck.TryAddLastRequiredReeferContainers(ref reeferContainers, minimumContainersToPlace);
                reeferContainersPlacedCount = reeferContainerCount - reeferContainers.Count;
            }

            FreighterPrinter.PrintFreighter(this);

            // check for left over containers and report them
            Tuple<double, double> weights = ContainerDeck.GetLeftAndRightWeights();
            double marge = Weight * 0.20;
            bool balanced = weights.Item1 > weights.Item2
                ? weights.Item2 + marge > weights.Item1
                : weights.Item1 + marge > weights.Item2;
            double weightDifference = weights.Item1 > weights.Item2
                ? weights.Item1 - weights.Item2
                : weights.Item2 - weights.Item1;
            double weightDifferenceInPercentage = weightDifference / marge;
            bool minimumWeightIsMet = Weight >= MaximumWeight / 2;

            bool minimumDryContainersPlacedIsMet = dryContainersPlacedCount > minDryContainers;
            bool minimumReeferContainersPlacedIsMet = reeferContainersPlacedCount > minReeferContainers;
            bool minimumValuableContainersPlacedIsMet = valuableContainersPlacedCount > minValuableContainers;
            Console.WriteLine($"The minimum dry containers to be placed is: {minDryContainers}. " + "This goal is " + (minimumDryContainersPlacedIsMet ? string.Empty : "not ") + "met");
            Console.WriteLine($"The minimum valuable containers to be placed is: {minValuableContainers}. " + "This goal is " + (minimumValuableContainersPlacedIsMet ? string.Empty : "not ") + "met");
            Console.WriteLine($"The minimum reefer containers to be placed is: {minReeferContainers}. " + "This goal is " + (minimumReeferContainersPlacedIsMet ? string.Empty : "not ") + "met");

            Console.WriteLine("The freighter is " + (minimumWeightIsMet ? string.Empty : "not ") + "allowed to set sail because the 50% minimum weight is " + (minimumWeightIsMet ? string.Empty : "not ") + "met.");
            Console.WriteLine($"The maximum weight difference is 20% and the current weight difference is {weightDifferenceInPercentage:P2}.");
            Console.WriteLine("The freighter is " + (balanced ? string.Empty : "not ") + "balanced.");
            Console.WriteLine();
            Console.WriteLine($"Maximum weight: {MaximumWeight}, Minimum weight: {MaximumWeight / 2}, Current weight: {Weight}");
            Console.WriteLine($"Weight of the left side: {weights.Item1}, Weight of the right side: {weights.Item2}");
            Console.WriteLine();
            Console.WriteLine($"From the {dryContainerCount} dry containers. {dryContainerCount - dryContainers.Count} were sorted and {dryContainers.Count} are left over.");
            Console.WriteLine($"From the {valuableContainerCount} valuable containers. {valuableContainerCount - valuableContainers.Count} were sorted and {valuableContainers.Count} are left over.");
            Console.WriteLine($"From the {reeferContainerCount} reefer containers. {reeferContainerCount - reeferContainers.Count} were sorted and {reeferContainers.Count} are left over.");
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
