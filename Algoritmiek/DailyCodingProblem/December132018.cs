using System;
using System.Linq;

namespace Algoritmiek.DailyCodingProblem
{
    public class December132018 : IProgram
    {
        /// <summary>
        /// Gets the array of integers.
        /// </summary>
        public int[] Numbers { get; private set; }

        /// <summary>
        /// Gets the output.
        /// </summary>
        public int Output { get; private set; }

        /// <inheritdoc />
        public string Name { get; } = "DailyCodingProblem - December-13-2018";

        /// <inheritdoc />
        public string Description { get; } =
            "This problem was asked by Stripe.\n\nGiven an array of integers find the first missing positive integer in linear time and constant space. " +
            "in other words, find the lowest positive integer that does not exist in the array. The array can contain duplicates and negative numbers as well. " +
            "\n\nFor example, the input [3, 4, -1, 1] should give 2. The input [1, 2, 0] should give 3. \nYou can modify the input array in-place.";

        /// <inheritdoc />
        public bool HasSetupRan { get; private set; }

        /// <inheritdoc />
        public void Run()
        {
            if (!HasSetupRan)
                throw new SetupNotRanException();
            int highestNumber = 1;
            for (int i = 0; i < Numbers.Length; i++)
            {
                highestNumber = Numbers[i] > highestNumber ? Numbers[i] : highestNumber;
                for (int j = 1; j < Numbers.Length; j++)
                {
                    if (Numbers.Length == i + 1 || Numbers[i] + j == Numbers[i + 1] && Numbers[i] + j > 0) break;
                    Output = Numbers[i] + j > 0 && Numbers[i] + j < Output || Output == 0 && Numbers[i] + j > 0 ? Numbers[i] + j : Output;
                }
            }
            Output = Output == 0 ? highestNumber + 1 : Output;
        }

        /// <inheritdoc />
        public void Setup()
        {
            Numbers = new int[] { -4, -2, 6, 9 };
            //Numbers = new int[] { 0, 1, -1 };
            HasSetupRan = true;
        }

        /// <inheritdoc />
        public void Print()
        {
            Console.Write($"[{Numbers[0]}");
            Array.ForEach(Numbers.Skip(1).Take(Numbers.Length - 2).ToArray(), (i) => Console.Write($", {i}"));
            Console.Write((Numbers.Length > 1 ? ", " + Numbers[Numbers.Length - 1] : "") + "]");
            Console.WriteLine($"\nSolution is: {Output}");
        }
    }
}
