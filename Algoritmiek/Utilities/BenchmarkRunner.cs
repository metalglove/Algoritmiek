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
            // TODO: fix Task based or either Thread based benchmarking...
            List<Task> tasks = new List<Task>();
            Console.WriteLine(Stopwatch.IsHighResolution
                ? "Operations timed using the system's high-resolution performance counter."
                : "Operations timed using the DateTime class.");
            Console.WriteLine($"  Timer frequency in ticks per second = {Stopwatch.Frequency}");
            Console.WriteLine("  Timer is accurate within {0} nanoseconds", (1000L * 1000L * 1000L) / Stopwatch.Frequency);
            Console.WriteLine("Running...");
            foreach (IProgram program in _programs)
            {
                if (!_concurrentDictionary.ContainsKey(program.GetType()))
                    _concurrentDictionary.TryAdd(program.GetType(), new List<Benchmark>());
                int j = 1;
                for (int i = 0; i < _runs + WarmupRuns; i++)
                {
                    int j1 = j == 9 ? (j = 1) : j;
                    tasks.Add(Task.Run(() =>
                    {
                        IProgram newProgram = (IProgram)Activator.CreateInstance(program.GetType());
                        BenchmarkAction(newProgram.GetType(), newProgram.Setup, j1, false, false, displayTimings, displayEvents)
                            .ContinueWith(task =>
                                BenchmarkAction(newProgram.GetType(), newProgram.Run, j1, false, false, displayTimings, displayEvents))
                            .ConfigureAwait(false);
                    }));
                    j++;
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
            long nanoSecondsPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            double averageBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Average(bench => bench.TotalMilliseconds);
            double averageBenchmarkInTicks = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Average(bench => bench.Ticks);
            double maxBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Max(bench => bench.TotalMilliseconds);
            long maxBenchmarkInTicks = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Max(bench => bench.Ticks);
            double minBenchmarkInMilliseconds = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Min(bench => bench.TotalMilliseconds);
            long minBenchmarkInTicks = GetBenchmarksFor(program, actionName).Skip(WarmupRuns).Min(bench => bench.Ticks);
            Console.WriteLine($"{actionName} timing:");
            Console.WriteLine($"Max: {maxBenchmarkInMilliseconds}ms, {maxBenchmarkInTicks * nanoSecondsPerTick}ns, {maxBenchmarkInTicks}ticks"); 
            Console.WriteLine($"Min: {minBenchmarkInMilliseconds}ms, {minBenchmarkInTicks * nanoSecondsPerTick}ns, {minBenchmarkInTicks}ticks");
            Console.WriteLine($"Average: {averageBenchmarkInMilliseconds}ms, {averageBenchmarkInTicks * nanoSecondsPerTick}ns, {averageBenchmarkInTicks}ticks");
        }

        /// <summary>
        /// Gets the benchmarks for the given <see cref="IProgram"/> and action that has ran.
        /// </summary>
        /// <param name="program">The program that the benchmarks need to be get for.</param>
        /// <param name="actionName">The action name within the program the benchmarks need to be get for.</param>
        /// <returns>The benchmarks for the given <see cref="IProgram"/> and action that has ran.</returns>
        private IEnumerable<Benchmark> GetBenchmarksFor(IProgram program, string actionName)
        {
            return _concurrentDictionary[program.GetType()].Where(bench => bench.Action == actionName);
        }

        /// <summary>
        /// Benchmarks the given <see cref="Action"/>.
        /// </summary>
        /// <param name="programType">The type of program that is being benchmarked.</param>
        /// <param name="action">The action to be benchmarked.</param>
        /// <param name="processorAffinityCore">The core the process should be running on.</param>
        /// <param name="useHighPriorityOnThreads">A value indicating whether the thread should run with high priority.</param>
        /// <param name="useGarbageCollection">A value indicating whether the benchmark action should be run after a garbage collection.</param>
        /// <param name="displayTimings">A value indicating whether the benchmark timings should be written to the <see cref="Console"/>.</param>
        /// <param name="displayEvents">A value indicating whether the events should be written to the <see cref="Console"/>.</param>
        private Task BenchmarkAction(Type programType, Action action, int processorAffinityCore = 2, bool useHighPriorityOnThreads = false, bool useGarbageCollection = false, bool displayTimings = false, bool displayEvents = false)
        {
            if (displayEvents)
                Console.WriteLine($"Started: {action.Method.DeclaringType.Name}.{action.Method.Name}");

            if (useHighPriorityOnThreads)
            {
                Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(processorAffinityCore);
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
            }
            
            if (useGarbageCollection)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

            Stopwatch stopwatch = Stopwatch.StartNew();
            action.Invoke();
            stopwatch.Stop();

            if (displayTimings)
                DisplayTimings(stopwatch);

            if (displayEvents)
                Console.WriteLine($"Finished: {action.Method.DeclaringType.Name}.{action.Method.Name}");

            _concurrentDictionary[programType].Add(new Benchmark(action: action.Method.Name, totalMilliseconds: stopwatch.Elapsed.TotalMilliseconds, ticks: stopwatch.ElapsedTicks));
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
            Console.WriteLine($"Time elapsed (ticks): {stopwatch.Elapsed.Ticks}");
        }
    }
}
