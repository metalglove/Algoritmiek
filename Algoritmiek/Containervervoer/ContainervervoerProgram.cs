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
        /// Gets or sets the freighter for the containers to be sorted on.
        /// </summary>
        public Freighter Freighter { get; private set; }

        /// <inheritdoc cref="IProgram.Name"/>
        public string Name => GetType().Name;

        /// <inheritdoc cref="IProgram.Description" />
        public string Description => "Sort containers onto the freighter accordingly.";

        /// <inheritdoc cref="IProgram.HasSetupRan"/>
        public bool HasSetupRan { get; private set; }

        public ContainervervoerProgram()
        {
            Containers = new List<Container>();
        }

        /// <inheritdoc />
        public void Setup()
        {
            HasSetupRan = true;
            Freighter = new Freighter(4, 5, 6);

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

        }

        /// <inheritdoc />
        public void Run()
        {
            Freighter.Sort(Containers, 0, 0, 0);
        }
    }
}
