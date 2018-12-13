using System;
using System.Linq;

namespace Algoritmiek.Circustrein
{
    /// <summary>
    /// Provides methods to print a <see cref="Train"/> object to the console.
    /// </summary>
    public static class TrainPrinter
    {
        private const string TableSplitter = "|";

        /// <summary>
        /// Prints the train to the console.
        /// </summary>
        /// <param name="train">The train to print</param>
        public static void Print(Train train)
        {
            Console.WriteLine("Train");
            string header = $"{TableSplitter} Number {TableSplitter} Animals {TableSplitter} Cost {TableSplitter}";
            Console.WriteLine("".PadRight(header.Length, '-'));
            Console.WriteLine(header);
            Console.WriteLine("".PadRight(header.Length, '-'));
            for (int i = 0; i < train.Carriages.Count; i++)
            {
                string count = train.Carriages[i].Animals.Count.ToString();
                string cost = train.Carriages[i].Animals.Sum(anml => (int) anml.Size).ToString();
                Console.WriteLine($"{TableSplitter} {(i + 1).ToString().PadRight(6,' ')} {TableSplitter} {count.PadRight(7, ' ')} {TableSplitter} {cost.PadRight(4, ' ')} {TableSplitter}");
            }
            Console.WriteLine("".PadRight(header.Length, '-'));
            Console.WriteLine($"{TableSplitter} Totals:{"".PadRight(header.Length - 11, ' ')} {TableSplitter}");
            Console.WriteLine("".PadRight(header.Length, '-'));
            string animalsCount = train.Carriages.SelectMany(carriage => carriage.Animals).Count().ToString();
            string animalsCost = train.Carriages.SelectMany(carriage => carriage.Animals).Sum(anml => (int)anml.Size).ToString();
            Console.WriteLine($"{TableSplitter} {train.Carriages.Count.ToString().PadRight(6, ' ')} {TableSplitter} {animalsCount.PadRight(7, ' ')} {TableSplitter} {animalsCost.PadRight(4, ' ')} {TableSplitter}");
            Console.WriteLine("".PadRight(header.Length, '-'));

            Console.WriteLine();

            foreach (TrainCarriage trainCarriage in train.Carriages)
            {
                PrintTrainCarriage(trainCarriage);
            }
        }

        /// <summary>
        /// Prints the train carriage to the console.
        /// </summary>
        /// <param name="trainCarriage">the train carriage to print.</param>
        private static void PrintTrainCarriage(TrainCarriage trainCarriage)
        {
            
            string header = $"{TableSplitter} EatingBehaviour {TableSplitter} Size   {TableSplitter} Count {TableSplitter} Cost {TableSplitter}";
            Console.WriteLine("".PadRight(header.Length, '-'));
            Console.WriteLine(header);
            Console.WriteLine("".PadRight(header.Length, '-'));
            foreach (var groupedAnimals in trainCarriage.Animals.GroupBy(animals => new { animals.EatingBehaviour, animals.Size }))
            {
                string eatingBehaviour = groupedAnimals.First().EatingBehaviour.ToString();
                string size = groupedAnimals.First().Size.ToString();
                string count = groupedAnimals.Count().ToString();
                string cost = groupedAnimals.Sum(xd => (int) xd.Size).ToString();
                Console.WriteLine($"{TableSplitter} {eatingBehaviour.PadRight(15,' ')} {TableSplitter} {size.PadRight(6, ' ')} {TableSplitter} {count.PadRight(5, ' ')} {TableSplitter} {cost.PadRight(4, ' ')} {TableSplitter}");    
            }
            Console.WriteLine("".PadRight(header.Length, '-'));
            Console.WriteLine($"{TableSplitter} Total count: {trainCarriage.Animals.Count}, Total cost: {trainCarriage.Animals.Sum(animal => (int)animal.Size)}".PadRight(header.Length - 1, ' ') + TableSplitter);
            Console.WriteLine("".PadRight(header.Length, '-'));
            Console.WriteLine();
        }
    }
}
