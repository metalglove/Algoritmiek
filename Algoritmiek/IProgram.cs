namespace Algoritmiek
{
    /// <summary>
    /// Represents methods used for programs that require a setup mechanism.
    /// </summary>
    public interface IProgram
    {
        /// <summary>
        /// Gets the name of the program.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the program.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether <see cref="Setup"/> has ran.
        /// </summary>
        bool HasSetupRan { get; }

        /// <summary>
        /// Runs the program.
        /// The <see cref="Setup"/> method must be called before calling <see cref="Run"/>. 
        /// </summary>
        /// <exception cref="SetupNotRanException">
        /// Thrown when the method <see cref="Setup"/> has not ran.
        /// </exception>
        void Run();

        /// <summary>
        /// Sets up the program to be ready to run it.
        /// This method must be called before <see cref="Run"/> is called.
        /// </summary>
        void Setup();
    }
}