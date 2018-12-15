using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents the implementation for the "Containervervoer" assignment.
    /// See <see cref="Description"/> for a short description.
    /// <para>
    /// A detailed description can be found under the Assignments folder in "Containervervoer.txt".
    /// </para> 
    /// </summary>
    public class ContainervervoerProgram : IProgram
    {
        /// <summary>
        /// Gets the containers to sort on to the freighter.
        /// </summary>
        public List<Container> Containers { get; private set; }

        /// <summary>
        /// Gets the original dry containers count.
        /// </summary>
        public int DryContainersCount { get; private set; }

        /// <summary>
        /// Gets the original valuable containers count.
        /// </summary>
        public int ValuableContainersCount { get; private set; }

        /// <summary>
        /// Gets the original reefer containers count.
        /// </summary>
        public int ReeferContainersCount { get; private set; }

        /// <summary>
        /// Gets the minimum dry containers to be placed.
        /// </summary>
        public int MinDryContainersToBePlaced { get; private set; }

        /// <summary>
        /// Gets the minimum valuable containers to be placed.
        /// </summary>
        public int MinValuableContainersToBePlaced { get; private set; }

        /// <summary>
        /// Gets the minimum reefer containers to be placed.
        /// </summary>
        public int MinReeferContainersToBePlaced { get; private set; }

        /// <summary>
        /// Gets or sets the freighter for the containers to be sorted on.
        /// </summary>
        public Freighter Freighter { get; private set; }

        /// <inheritdoc cref="IProgram.Name"/>
        public string Name => GetType().Name;

        /// <inheritdoc cref="IProgram.Description" />
        public string Description => "Sort containers onto the freighter accordingly.";

        /// <inheritdoc cref="IProgram.HasSetupRan"/>
        public bool HasSetupRan { get; private set; }

        /// <inheritdoc />
        public void Setup()
        {
            Containers = new List<Container>();
            //Freighter = new Freighter(4, 3, 5); // is not allowed to set sail.
            Freighter = new Freighter(6, 5, 5); // is allowed to set sail

            #region Adds
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(6_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(8_000, FreightType.Dry)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Valuable)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));
            //Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Refrigerated)));

            Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(2_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(6_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(5_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(8_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(26_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(20_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Dry)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Valuable)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            Containers.Add(ContainerFactory.Create(new Freight(24_000, FreightType.Refrigerated)));
            #endregion Adds

            DryContainersCount = Containers.OfType<DryContainer>().Count();
            ValuableContainersCount = Containers.OfType<ValuableContainer>().Count();
            ReeferContainersCount = Containers.OfType<ReeferContainer>().Count();

            MinDryContainersToBePlaced = 40;
            MinValuableContainersToBePlaced = 12;
            MinReeferContainersToBePlaced = 12;

            HasSetupRan = true;
        }

        /// <inheritdoc />
        public void Print()
        {
            FreighterPrinter.PrintFreighter(Freighter);

            Tuple<double, double> weights = Freighter.ContainerDeck.GetLeftAndRightWeights();
            double marge = Freighter.Weight * 0.20;
            bool balanced = weights.Item1 > weights.Item2
                ? weights.Item2 + marge > weights.Item1
                : weights.Item1 + marge > weights.Item2;
            double weightDifference = weights.Item1 > weights.Item2
                ? weights.Item1 - weights.Item2
                : weights.Item2 - weights.Item1;
            double weightDifferenceInPercentage = weightDifference / Freighter.Weight;
            bool minimumWeightIsMet = Freighter.Weight >= Freighter.MaximumWeight / 2;

            IEnumerable<Container> containersPlaced = Freighter.ContainerDeck.GetContainers();

            bool minimumDryContainersPlacedIsMet = containersPlaced.OfType<DryContainer>().Count() >= MinDryContainersToBePlaced;
            bool minimumValuableContainersPlacedIsMet = containersPlaced.OfType<ValuableContainer>().Count() >= MinValuableContainersToBePlaced;
            bool minimumReeferContainersPlacedIsMet = containersPlaced.OfType<ReeferContainer>().Count() >= MinReeferContainersToBePlaced;
            Console.WriteLine($"The minimum dry containers to be placed is: {MinDryContainersToBePlaced}. " + "This goal is " + (minimumDryContainersPlacedIsMet ? string.Empty : "not ") + "met");
            Console.WriteLine($"The minimum valuable containers to be placed is: {MinValuableContainersToBePlaced}. " + "This goal is " + (minimumValuableContainersPlacedIsMet ? string.Empty : "not ") + "met");
            Console.WriteLine($"The minimum reefer containers to be placed is: {MinReeferContainersToBePlaced}. " + "This goal is " + (minimumReeferContainersPlacedIsMet ? string.Empty : "not ") + "met");
            Console.WriteLine("The 50% minimum weight is " + (minimumWeightIsMet ? string.Empty : "not ") + "met.");
            Console.WriteLine($"The maximum weight difference is 20% and the current weight difference is {weightDifferenceInPercentage:P2}.");
            Console.WriteLine("The freighter is " + (balanced ? string.Empty : "not ") + "balanced.");
            Console.WriteLine();
            Console.WriteLine($"Maximum weight: {Freighter.MaximumWeight}, Minimum weight: {Freighter.MaximumWeight / 2}, Current weight: {Freighter.Weight}");
            Console.WriteLine($"Weight of the left side: {weights.Item1}, Weight of the right side: {weights.Item2}");
            Console.WriteLine();
            Console.WriteLine($"From the {DryContainersCount} dry containers. {DryContainersCount - containersPlaced.OfType<DryContainer>().Count()} were sorted and {containersPlaced.OfType<DryContainer>().Count()} are left over.");
            Console.WriteLine($"From the {ValuableContainersCount} valuable containers. {ValuableContainersCount - containersPlaced.OfType<ValuableContainer>().Count()} were sorted and {containersPlaced.OfType<ValuableContainer>().Count()} are left over.");
            Console.WriteLine($"From the {ReeferContainersCount} reefer containers. {ReeferContainersCount - containersPlaced.OfType<ReeferContainer>().Count()} were sorted and {containersPlaced.OfType<ReeferContainer>().Count()} are left over.");
            Console.WriteLine();
            bool isAllowedToSetSail = balanced && minimumWeightIsMet && minimumDryContainersPlacedIsMet &&
                                      minimumValuableContainersPlacedIsMet && minimumReeferContainersPlacedIsMet;
            Console.ForegroundColor = isAllowedToSetSail ? ConsoleColor.Green : ConsoleColor.DarkRed;
            Console.WriteLine("The freighter is " + (isAllowedToSetSail ? string.Empty : "not ") + "allowed to set sail.");
            Console.ResetColor();
        }

        /// <inheritdoc />
        public void Run()
        {
            if (!HasSetupRan)
                throw new SetupNotRanException();

            Freighter.Sort(Containers, MinDryContainersToBePlaced, MinValuableContainersToBePlaced, MinReeferContainersToBePlaced);
        }
    }
}
