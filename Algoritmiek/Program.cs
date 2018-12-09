using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Algoritmiek
{
    /// <summary>
    /// Represents the main program that will run all other programs.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Gets the dictionary that holds benchmarks per Type of Program that has ran.
        /// </summary>
        private static readonly Dictionary<Type, IList<Benchmark>> ProgramBenchmarks = new Dictionary<Type, IList<Benchmark>>();

        /// <summary>
        /// Gets the amount of runs the program will loop for.
        /// </summary>
        private const int Runs = 1_000;

        /// <summary>
        /// Main method that is the starting point of the <see cref="Main"/> class.
        /// </summary>
        /// <param name="args">The executing arguments (none used for this program).</param>
        public static void Main(string[] args)
        {
            for (int i = 0; i < Runs; i++)
            {
                foreach (IProgram program in GetPrograms())
                {
                    if (!ProgramBenchmarks.ContainsKey(program.GetType()))
                        ProgramBenchmarks.Add(program.GetType(), new List<Benchmark>());

                    Benchmark setupBenchmark = BenchmarkAction(program.Setup, displayTimings: false, displayEvents: false);
                    ProgramBenchmarks[program.GetType()].Add(setupBenchmark);

                    Benchmark runBenchmark = BenchmarkAction(program.Run, displayTimings: false, displayEvents: false);
                    ProgramBenchmarks[program.GetType()].Add(runBenchmark);
                }
            }

            foreach (IProgram program in GetPrograms())
            {
                Console.WriteLine($"Runs: {Runs.ToString("N0")}");
                DisplayBenchmarkInformation(program, actionName: ((Action)program.Setup).Method.Name);
                DisplayBenchmarkInformation(program, actionName: ((Action)program.Run).Method.Name);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Displays the benchmark information in the <see cref="Console"/>.
        /// </summary>
        /// <param name="program">The program to display the benchmark information for.</param>
        /// <param name="actionName">The name of the action to display the benchmark information for.</param>
        private static void DisplayBenchmarkInformation(IProgram program, string actionName)
        {
            double averageBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Average(bench => bench.TotalMilliseconds);
            double maxBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Max(bench => bench.TotalMilliseconds);
            double minBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Min(bench => bench.TotalMilliseconds);
            Console.WriteLine($"{actionName} timing:");
            Console.WriteLine($"Max: {maxBenchmarkInMilliseconds}ms");
            Console.WriteLine($"Min: {minBenchmarkInMilliseconds}ms");
            Console.WriteLine($"Average: {averageBenchmarkInMilliseconds}ms");
        }

        /// <summary>
        /// Gets the benchmarks for the specified <see cref="IProgram"/> and action that has ran.
        /// </summary>
        /// <param name="program">The program that the benchmarks need to be get for.</param>
        /// <param name="actionName">The action name within the program the benchmarks need to be get for.</param>
        /// <returns>The benchmarks for the specified <see cref="IProgram"/> and action that has ran.</returns>
        private static IEnumerable<Benchmark> GetBenchmarksFor(IProgram program, string actionName)
        {
            return ProgramBenchmarks[program.GetType()].Where(bench => bench.Action.Equals(actionName));
        }

        /// <summary>
        /// Displays the program information in the <see cref="Console"/>.
        /// </summary>
        /// <param name="program">The program to display the information for.</param>
        private static void DiplayProgramInformation(IProgram program)
        {
            Console.WriteLine();
            Console.WriteLine(program.Name);
            Console.WriteLine(program.Description);
            Console.WriteLine();
        }

        /// <summary>
        /// Benchmark the specified <see cref="Action"/>.
        /// </summary>
        /// <param name="action">The action to be benchmarked.</param>
        /// <param name="displayTimings">A value indicating whether the benchmark timings should be writen to the <see cref="Console"/>.</param>
        /// <param name="displayEvents">A value indicating whether the events should be writen to the <see cref="Console"/>.</param>
        /// <returns>The benchmark of the action that has been invoked.</returns>
        private static Benchmark BenchmarkAction(Action action, bool displayTimings, bool displayEvents = false)
        {
            if (displayEvents)
                Console.WriteLine($"Started: {action.Method.DeclaringType.Name}.{action.Method.Name}");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            action.Invoke();
            stopwatch.Stop();
            if (displayTimings)
                DisplayTimings(stopwatch);
            if (displayEvents)
                Console.WriteLine($"Finished: {action.Method.DeclaringType.Name}.{action.Method.Name}");
            return new Benchmark(action: action.Method.Name, totalMilliseconds: stopwatch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// Displays timing of a stopwatch in the <see cref="Console"/>.
        /// </summary>
        /// <param name="stopwatch">The stopwatch the timings should be displayed for.</param>
        private static void DisplayTimings(Stopwatch stopwatch)
        {
            Console.WriteLine("Time elapsed (s): {0}", stopwatch.Elapsed.TotalSeconds);
            Console.WriteLine("Time elapsed (ms): {0}", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Time elapsed (ns): {0}", stopwatch.Elapsed.TotalMilliseconds * 1000000);
        }

        /// <summary>
        /// Gets assignments as programs.
        /// </summary>
        /// <returns>The assignments as programs.</returns>
        private static IEnumerable<IProgram> GetPrograms()
        {
            yield return new Circustrein.CircusTrainProgram();
        }
    }
}
