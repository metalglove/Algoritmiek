using System;
using System.Text;

namespace Algoritmiek.Containervervoer
{
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

        public static void PrintFreighter(Freighter freighter)
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
                PrintContainers(containersFromHeight, i);
                Console.WriteLine();
            }
        }

        private static void PrintContainers(Container[,] containers, int z)
        {
            double totalWeight = 0;
            for (int y = 0; y < containers.GetLength(1); y++)
            for (int x = 0; x < containers.GetLength(0); x++)
            {
                if (containers[x, y] == default) continue;
                Console.WriteLine($"X: {x} Y: {y} Z: {z} Type: {containers[x, y].GetType().Name} Weight: {containers[x, y].Weight}");
                totalWeight += containers[x, y].Weight;
                Console.WriteLine($"Total deck {z:00} weight: {totalWeight}");
            }
        }

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
