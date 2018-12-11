using System;

namespace Algoritmiek.Containervervoer
{
    public static class FreighterPrinter
    {
        public static void PrintFreighter(Freighter freighter)
        {
            for (int i = 0; i < freighter.ContainerDeck.Height; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"Height: {i}");
                PrintContainers(freighter.ContainerDeck.GetContainersFromHeight(i));
                Console.WriteLine();
            }
        }

        private static void PrintContainers(Container[,] containers)
        {
            for (int i = 0; i < containers.GetUpperBound(0); i++)
            for (int j = 0; j < containers.GetUpperBound(1); j++)
            {
                Console.WriteLine($"X: {i} Y: {j} Type: {containers[i, j].GetType().Name} Weight: {containers[i, j].Weight}");
            }
        }
    }
}
