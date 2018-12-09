using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Algoritmiek.Utilities
{
    /// <summary>
    /// Provides benchmarks for programs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BenchmarkRunner
    {
        /// <summary>
        /// Gets the dictionary that holds benchmarks per Type of Program that has ran.
        /// </summary>
        private readonly Dictionary<Type, IList<Benchmark>> _programBenchmarks;

        /// <summary>
        /// Gets the programs to run.
        /// </summary>
        private readonly IEnumerable<IProgram> _programs;

        /// <summary>
        /// Gets the amount of runs the program will loop for.
        /// </summary>
        private readonly int _runs;

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkRunner"/> class.
        /// </summary>
        /// <param name="programs">The programs to benchmark.</param>
        /// <param name="runs">The amount of runs to benchmark for.</param>
        public BenchmarkRunner(IEnumerable<IProgram> programs, int runs)
        {
            _programBenchmarks = new Dictionary<Type, IList<Benchmark>>();
            _programs = programs;
            _runs = runs;
        }

        /// <summary>
        /// Runs the benchmark.
        /// </summary>
        public void Run(bool displayTimings = false, bool displayEvents = false)
        {
            Console.WriteLine("Running...");
            for (int i = 0; i < _runs; i++)
            {
                foreach (IProgram program in _programs)
                {
                    if (!_programBenchmarks.ContainsKey(program.GetType()))
                        _programBenchmarks.Add(program.GetType(), new List<Benchmark>());

                    Benchmark setupBenchmark = BenchmarkAction(program.Setup, displayTimings, displayEvents);
                    _programBenchmarks[program.GetType()].Add(setupBenchmark);

                    Benchmark runBenchmark = BenchmarkAction(program.Run, displayTimings, displayEvents);
                    _programBenchmarks[program.GetType()].Add(runBenchmark);
                }
            }
            Console.WriteLine("Finished!");
            foreach (IProgram program in _programs)
            {
                DisplayProgramInformation(program);
                Console.WriteLine($"Runs: {_runs:N0}");
                DisplayBenchmarkInformation(program, actionName: ((Action)program.Setup).Method.Name);
                DisplayBenchmarkInformation(program, actionName: ((Action)program.Run).Method.Name);
            }
        }

        /// <summary>
        /// Displays the benchmark information in the <see cref="Console"/>.
        /// </summary>
        /// <param name="program">The program to display the benchmark information for.</param>
        /// <param name="actionName">The name of the action to display the benchmark information for.</param>
        private void DisplayBenchmarkInformation(IProgram program, string actionName)
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
        private IEnumerable<Benchmark> GetBenchmarksFor(IProgram program, string actionName)
        {
            return _programBenchmarks[program.GetType()].Where(bench => bench.Action.Equals(actionName));
        }

        /// <summary>
        /// Benchmark the specified <see cref="Action"/>.
        /// </summary>
        /// <param name="action">The action to be benchmarked.</param>
        /// <param name="displayTimings">A value indicating whether the benchmark timings should be written to the <see cref="Console"/>.</param>
        /// <param name="displayEvents">A value indicating whether the events should be written to the <see cref="Console"/>.</param>
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
        /// Displays the program information in the <see cref="Console"/>.
        /// </summary>
        /// <param name="program">The program to display the information for.</param>
        private static void DisplayProgramInformation(IProgram program)
        {
            Console.WriteLine();
            Console.WriteLine(program.Name);
            Console.WriteLine(program.Description);
            Console.WriteLine();
        }

        /// <summary>
        /// Displays timing of a stopwatch in the <see cref="Console"/>.
        /// </summary>
        /// <param name="stopwatch">The stopwatch the timings should be displayed for.</param>
        private static void DisplayTimings(Stopwatch stopwatch)
        {
            Console.WriteLine($"Time elapsed (s): {stopwatch.Elapsed.TotalSeconds}");
            Console.WriteLine($"Time elapsed (ms): {stopwatch.Elapsed.TotalMilliseconds}");
            Console.WriteLine($"Time elapsed (ns): {stopwatch.Elapsed.TotalMilliseconds * 1000000}");
        }
    }
}
