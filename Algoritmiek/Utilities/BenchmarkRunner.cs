using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Algoritmiek.Utilities
{
    /// <summary>
    /// Provides benchmarks for programs.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class BenchmarkRunner
    {
        /// <summary>
        /// Gets the programs to run.
        /// </summary>
        private readonly IEnumerable<IProgram> _programs;

        /// <summary>
        /// Gets the dictionary that holds benchmarks per Type of Program that has ran.
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<Benchmark>> _concurrentDictionary;

        /// <summary>
        /// Gets the amount of runs the program will loop for.
        /// </summary>
        private readonly int _runs;

        /// <summary>
        /// Gets the amount of warm up runs the program will run.
        /// </summary>
        private const int WarmupRuns = 10_000;

        /// <summary>
        /// Initializes a new instance of the <see cref="BenchmarkRunner"/> class.
        /// </summary>
        /// <param name="programs">The programs to benchmark.</param>
        /// <param name="runs">The amount of runs to benchmark for.</param>
        public BenchmarkRunner(IEnumerable<IProgram> programs, int runs)
        {
            _concurrentDictionary = new ConcurrentDictionary<Type, List<Benchmark>>();
            _programs = programs;
            _runs = runs;
        }

        /// <summary>
        /// Runs the benchmark.
        /// </summary>
        public void Run(bool displayTimings = false, bool displayEvents = false)
        {
            List<Task> tasks = new List<Task>();
            Console.WriteLine("Running...");
            foreach (IProgram program in _programs)
            {
                if (!_concurrentDictionary.ContainsKey(program.GetType()))
                    _concurrentDictionary.TryAdd(program.GetType(), new List<Benchmark>());

                for (int i = 0; i < _runs + WarmupRuns; i++)
                {
                    var i1 = i;
                    tasks.Add(Task.Run(() =>
                    {
                        IProgram newProgram = (IProgram)Activator.CreateInstance(program.GetType());
                        BenchmarkAction(newProgram.GetType(), newProgram.Setup, displayTimings, displayEvents)
                            .ContinueWith(task =>
                                BenchmarkAction(newProgram.GetType(), newProgram.Run, displayTimings, displayEvents))
                            .ConfigureAwait(false);
                    }));
                }
            }

            Task.WaitAll(tasks.ToArray());

            foreach (IProgram program in _programs)
            {
                program.Setup();
                program.Run();
                DisplayProgramInformation(program);
                Console.WriteLine($"Runs: {_runs:N0}");
                DisplayBenchmarkInformation(program, actionName: ((Action)program.Setup).Method.Name);
                DisplayBenchmarkInformation(program, actionName: ((Action)program.Run).Method.Name);
                Console.WriteLine("Finished!");
            }
        }

        /// <summary>
        /// Displays the benchmark information in the <see cref="Console"/>.
        /// </summary>
        /// <param name="program">The program to display the benchmark information for.</param>
        /// <param name="actionName">The name of the action to display the benchmark information for.</param>
        private void DisplayBenchmarkInformation(IProgram program, string actionName)
        {
            double averageBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Average(bench => bench.TotalMilliseconds);
            double maxBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Max(bench => bench.TotalMilliseconds);
            double minBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Min(bench => bench.TotalMilliseconds);
            Console.WriteLine($"{actionName} timing:");
            Console.WriteLine($"Max: {maxBenchmarkInMilliseconds}ms"); 
            Console.WriteLine($"Min: {minBenchmarkInMilliseconds}ms");
            Console.WriteLine($"Average: {averageBenchmarkInMilliseconds}ms");
        }

        /// <summary>
        /// Gets the benchmarks for the given <see cref="IProgram"/> and action that has ran.
        /// </summary>
        /// <param name="program">The program that the benchmarks need to be get for.</param>
        /// <param name="actionName">The action name within the program the benchmarks need to be get for.</param>
        /// <returns>The benchmarks for the given <see cref="IProgram"/> and action that has ran.</returns>
        private IEnumerable<Benchmark> GetBenchmarksFor(IProgram program, string actionName)
        {
            return _concurrentDictionary[program.GetType()].Where(bench => bench.Action.Equals(actionName));
        }

        /// <summary>
        /// Benchmarks the given <see cref="Action"/>.
        /// </summary>
        /// <param name="programType"></param>
        /// <param name="action">The action to be benchmarked.</param>
        /// <param name="displayTimings">A value indicating whether the benchmark timings should be written to the <see cref="Console"/>.</param>
        /// <param name="displayEvents">A value indicating whether the events should be written to the <see cref="Console"/>.</param>
        private Task BenchmarkAction(Type programType, Action action, bool displayTimings, bool displayEvents = false)
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
            _concurrentDictionary[programType].Add(new Benchmark(action: action.Method.Name, totalMilliseconds: stopwatch.Elapsed.TotalMilliseconds));
            return Task.CompletedTask;
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
            program.Print();
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
            Console.WriteLine($"Time elapsed (ns): {stopwatch.Elapsed.TotalMilliseconds * 1_000_000}");
        }
    }
}
