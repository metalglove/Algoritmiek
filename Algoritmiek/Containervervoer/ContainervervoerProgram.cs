using System;

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
        /// <inheritdoc cref="IProgram.Name"/>
        public string Name => GetType().Name;

        /// <inheritdoc cref="IProgram.Description" />
        public string Description => "Sort freight into containers and onto the freighter accordingly.";

        /// <inheritdoc cref="IProgram.HasSetupRan"/>
        public bool HasSetupRan { get; private set; }

        /// <inheritdoc />
        public void Setup()
        {
            HasSetupRan = true;
        }

        /// <inheritdoc />
        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
