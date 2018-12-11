using System;
using System.Collections.Generic;

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
        public List<Container> Containers { get; }

        /// <summary>
        /// Gets the freighter for the containers to be sorted on.
        /// </summary>
        public Freighter Freighter { get; }

        /// <inheritdoc cref="IProgram.Name"/>
        public string Name => GetType().Name;

        /// <inheritdoc cref="IProgram.Description" />
        public string Description => "Sort freight into containers and onto the freighter accordingly.";

        /// <inheritdoc cref="IProgram.HasSetupRan"/>
        public bool HasSetupRan { get; private set; }

        public ContainervervoerProgram()
        {
            Containers = new List<Container>();
            Freighter = new Freighter(6, 4, 6);
        }

        /// <inheritdoc />
        public void Setup()
        {
            HasSetupRan = true;
            Containers.Add(new DryContainer(new Freight(2_000)));
            Containers.Add(new DryContainer(new Freight(2_000)));
            Containers.Add(new DryContainer(new Freight(2_000)));
            Containers.Add(new DryContainer(new Freight(2_000)));
            Containers.Add(new DryContainer(new Freight(2_000)));
            Containers.Add(new DryContainer(new Freight(2_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(6_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(5_000)));
            Containers.Add(new DryContainer(new Freight(8_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(26_000)));
            Containers.Add(new DryContainer(new Freight(20_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new DryContainer(new Freight(24_000)));
            Containers.Add(new ValuableContainer(new Freight(24_000)));
            Containers.Add(new ValuableContainer(new Freight(24_000)));
            Containers.Add(new ValuableContainer(new Freight(24_000)));
            Containers.Add(new ValuableContainer(new Freight(24_000)));
            Containers.Add(new ValuableContainer(new Freight(24_000)));
            Containers.Add(new ValuableContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
            Containers.Add(new ReeferContainer(new Freight(24_000)));
        }

        /// <inheritdoc />
        public void Run()
        {
            Freighter.Sort(Containers);
            FreighterPrinter.PrintFreighter(Freighter);
        }
    }
}
