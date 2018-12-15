using System.Diagnostics.CodeAnalysis;

namespace Algoritmiek.Utilities
{
    /// <summary>
    /// Represents a benchmark of an action.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public struct Benchmark
    {
        /// <summary>
        /// Gets the name of the action that was executed.
        /// </summary>
        public readonly string Action;

        /// <summary>
        /// Gets the total time in milliseconds the action executed for.
        /// </summary>
        public readonly double TotalMilliseconds;

        /// <summary>
        /// Gets the total ticks the action executed for.
        /// </summary>
        public readonly long Ticks;

        /// <summary>
        /// Initializes the a new instance of the <see cref="Benchmark"/> struct.
        /// </summary>
        /// <param name="action">The name of the action that was executed.</param>
        /// <param name="totalMilliseconds">The total time in milliseconds the action executed for.</param>
        /// <param name="ticks">The total ticks the action executed for.</param>
        public Benchmark(string action, double totalMilliseconds, long ticks)
        {
            Action = action;
            TotalMilliseconds = totalMilliseconds;
            Ticks = ticks;
        }
    }
}
