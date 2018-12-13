using System;
using System.Text;

namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Provides methods to print a <see cref="Freighter"/> object to the console.
    /// </summary>
    public static class FreighterPrinter
    {
        private const char CellLeftTop = '┌';
        private const char CellRightTop = '┐';
        private const char CellLeftBottom = '└';
        private const char CellRightBottom = '┘';
        private const char CellHorizontalJointTop = '┬';
        private const char CellHorizontalJointBottom = '┴';
        private const char CellVerticalJointLeft = '├';
        private const char CellTJoint = '┼';
        private const char CellVerticalJointRight = '┤';
        private const char CellHorizontalLine = '─';
        private const char CellVerticalLine = '│';

        /// <summary>
        /// Prints the freighter to the console.
        /// </summary>
        /// <param name="freighter">The freighter to be printed.</param>
        /// <param name="detailed">Determines if the freighter should be printed in detail.</param>
        public static void PrintFreighter(Freighter freighter, bool detailed = false)
        {
            Console.WriteLine();
            Console.WriteLine("D = Dry container");
            Console.WriteLine("R = Reefer container");
            Console.WriteLine("V = Valuable container");
            Console.WriteLine();
            for (int i = 0; i < freighter.ContainerDeck.Height; i++)
            {
                Container[,] containersFromHeight = freighter.ContainerDeck.GetContainersFromHeight(i);
                PrintContainerDeckByHeight(containersFromHeight, i);
                Console.WriteLine();
                if (!detailed) continue;
                PrintDetailedContainersByHeight(containersFromHeight, i);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints detailed container information by height.
        /// </summary>
        /// <param name="containers">The containers for print the detailed information for.</param>
        /// <param name="height">The height of the containers.</param>
        private static void PrintDetailedContainersByHeight(Container[,] containers, int height)
        {
            double totalWeight = 0;
            for (int y = 0; y < containers.GetLength(1); y++)
            for (int x = 0; x < containers.GetLength(0); x++)
            {
                if (containers[x, y] == default) continue;
                Console.WriteLine($"X: {x} Y: {y} Z: {height} Type: {containers[x, y].GetType().Name} Weight: {containers[x, y].Weight}");
                totalWeight += containers[x, y].Weight;
                Console.WriteLine($"Total deck {height:00} weight: {totalWeight}");
            }
        }

        /// <summary>
        /// Prints containers by height.
        /// </summary>
        /// <remarks>
        /// This code should not be reused it truly is magic!
        /// </remarks>
        /// <param name="containers">The containers to print.</param>
        /// <param name="height">The height of the containers.</param>
        private static void PrintContainerDeckByHeight(Container[,] containers, int height)
        {
            StringBuilder stringBuilder = new StringBuilder();
            const int deciderWidth = 2;
            int width = containers.GetLength(1);
            int length = containers.GetLength(0);
            int decider = width * deciderWidth * 2;
            int newDecider = (decider - 8 ) / 2;
            stringBuilder.Append(CellVerticalLine.ToString().PadRight(newDecider, ' ') + $"Deck: {height:00}" + "".PadRight(newDecider, ' '));
            stringBuilder.Append($"{CellVerticalLine}");
            stringBuilder.AppendLine();
            string splitter = CellVerticalJointLeft.ToString().PadRight(stringBuilder.Length - 3, CellHorizontalLine);
            stringBuilder.Append(CellVerticalJointLeft);
            for (int j = 0; j < width; j++)
            {
                stringBuilder.Append($"{CellHorizontalLine}".PadRight(deciderWidth * 2 - 1, CellHorizontalLine) + CellHorizontalJointTop);
            }
            stringBuilder.Insert(0, splitter + CellRightTop + Environment.NewLine);
            stringBuilder.Remove(0, 1);
            stringBuilder.Insert(0, CellLeftTop);
            stringBuilder.Replace(CellHorizontalJointTop, CellVerticalJointRight, stringBuilder.Length - 1, 1);
            stringBuilder.AppendLine();

            for (int x = 0; x < length; x++)
            {
                int y = 0;
                for (; y < width; y++)
                {
                    stringBuilder.Append(containers[x, y] == default
                        ? $"{CellVerticalLine}".PadRight(deciderWidth * 2, ' ')
                        : $"{CellVerticalLine}".PadRight(deciderWidth, ' ') +
                          $"{containers[x, y].GetType().Name[0]}".PadRight(deciderWidth, ' '));
                }
                if (x >= length - 1 && y >= width - 1)
                {
                    stringBuilder.AppendLine(CellVerticalLine.ToString());
                    continue;
                }
                stringBuilder.AppendLine(CellVerticalLine.ToString());
                stringBuilder.Append($"{CellVerticalJointLeft}".PadRight(deciderWidth * 2, CellHorizontalLine));
                for (int j = 1; j < width; j++)
                {
                    stringBuilder.Append($"{CellTJoint}".PadRight(deciderWidth * 2, CellHorizontalLine));
                }
                stringBuilder.AppendLine(CellVerticalJointRight.ToString());
            }

            stringBuilder.Append(CellLeftBottom);
            for (int j = 0; j < width; j++)
            {
                stringBuilder.Append($"{CellHorizontalLine}".PadRight(deciderWidth * 2 - 1, CellHorizontalLine) + CellHorizontalJointBottom);
            }
            stringBuilder.Replace(CellHorizontalJointBottom, CellRightBottom, stringBuilder.Length - 1, 1);
            stringBuilder.AppendLine();
            Console.Write(stringBuilder);
        }
    }
}
