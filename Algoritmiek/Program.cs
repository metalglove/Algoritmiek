using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Algoritmiek.Circustrein;
using Algoritmiek.Containervervoer;
using Algoritmiek.Utilities;

namespace Algoritmiek
{
    /// <summary>
    /// Represents the main program that will run all other programs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        /// Main method that is the starting point of the <see cref="Main"/> class.
        /// </summary>
        /// <param name="args">The executing arguments (none used for this program).</param>
        public static void Main(string[] args)
        {
            BenchmarkRunner benchmarkRunner = new BenchmarkRunner(programs: GetPrograms(), runs: 1);
            benchmarkRunner.Run();
            Console.ReadKey();
        }

        /// <summary>
        /// Gets assignments as programs.
        /// </summary>
        /// <returns>The assignments as programs.</returns>
        private static IEnumerable<IProgram> GetPrograms()
        {
            // Each algorithmic assignment will end up here. (more to come!)
            yield return new CircusTrainProgram();
            yield return new ContainervervoerProgram();
        }
    }
}
