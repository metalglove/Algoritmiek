using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Algoritmiek.Containervervoer
{
    public class ContainerDeck
    {
        /// <summary>
        /// The container deck represented as a 3D array of containers.
        /// </summary>
        private readonly Container[,,] _containerDeckIn3D;

        /// <summary>
        /// Gets the maximum length of the container deck.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the maximum width of the container deck.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the maximum height of the container deck.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Gets the total weight on the container deck.
        /// </summary>
        public double Weight
        {
            get
            {
                double returnValue = 0;
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        for (int k = 0; k < Height; k++)
                        {
                            if (this[i, j, k] != null)
                                returnValue += this[i, j, k].Weight;
                        }
                    }
                }

                return returnValue;
            } 
        }

        /// <summary>
        /// Gets or sets a container from/on the container deck by x, y & z-coordinates.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="z">The z-coordinate.</param>
        /// <returns>The container on the specified x, y & z-coordinate.</returns>
        public Container this[int x, int y, int z]
        {
            get
            {
                //if (x > Length || y > Width || z > Height)
                //    throw new IndexOutOfRangeException();
                return _containerDeckIn3D[x, y, z];
            }
            set
            {
                //if (x > Length || y > Width || z > Height)
                //    throw new IndexOutOfRangeException();
                _containerDeckIn3D[x, y, z] = value;
            }
        }

        /// <summary>
        /// Gets or sets a container from/on the container deck by <see cref="Location"/>
        /// </summary>
        /// <param name="location">The location for the container.</param>
        /// <returns>The container on the specified x, y & z-coordinate using <see cref="Location"/>.</returns>
        public Container this[Location location]
        {
            get => this[location.X, location.Y, location.Z];
            set => this[location.X, location.Y, location.Z] = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerDeck"/> class.
        /// </summary>
        /// <param name="length">the maximum length of the container deck.</param>
        /// <param name="width">the maximum width of the container deck.</param>
        /// <param name="height">the maximum height of the container deck.</param>
        public ContainerDeck(int length, int width, int height)
        {
            Length = length;
            Width = width;
            Height = height;
            _containerDeckIn3D = new Container[length, width, height];
        }

        public void TryAddAllDryContainers(ref List<DryContainer> dryContainers)
        {
            SplitContainers(dryContainers, out List<Container> leftDryContainers, out List<Container> rightDryContainers);

            int currentHeight = 0;
            while (dryContainers.Any() && currentHeight < Height)
            {
                List<Location> locations = GetLocationsForHeight(currentHeight).ToList();
                List<Location> leftLocations = locations.Where(location => location.Y >= Width / 2).ToList();
                List<Location> rightLocations = locations.Where(location => location.Y < Width / 2).ToList();
                if (leftDryContainers.Any())
                    TryAddDryContainers(ref dryContainers, ref leftDryContainers, currentHeight, leftLocations);
                if (rightDryContainers.Any())
                    TryAddDryContainers(ref dryContainers, ref rightDryContainers, currentHeight, rightLocations);

                currentHeight++;
            }
        }
        public void TryAddAllReeferContainers(ref List<ReeferContainer> reeferContainers)
        {
            SplitContainers(reeferContainers, out List<Container> leftReeferContainers, out List<Container> rightReeferContainers);
            int currentHeight = 0;
            while (reeferContainers.Any() && currentHeight < Height)
            {
                List<Location> locations = GetLocationsForLengthAndHeight(Length, currentHeight).ToList();
                List<Location> leftLocations = locations.Where(location => location.Y >= Width / 2).ToList();
                List<Location> rightLocations = locations.Where(location => location.Y < Width / 2).ToList();
                if (leftReeferContainers.Any())
                    TryAddReeferContainers(ref reeferContainers, ref leftReeferContainers, currentHeight, leftLocations);
                if (rightReeferContainers.Any())
                    TryAddReeferContainers(ref reeferContainers, ref rightReeferContainers, currentHeight, rightLocations);
                currentHeight++;
            }
        }
        public void TryAddAllValuableContainers(ref List<ValuableContainer> valuableContainers)
        {
            foreach (Location location in GetHighestFreeLocations())
            { 
                ValuableContainer valuableContainer = valuableContainers.FirstOrDefault();
                if (valuableContainer == default) return;
                this[location] = valuableContainer;
            }
        }

        public Container[,] GetContainersFromHeight(int height)
        {
            Container[,] containers = new Container[Width, Length];
            for (int y = 0; y < Length; y++)
            for (int x = 0; x < Width; x++)
            {
                containers[x, y] = this[x, y, height];
            }

            return containers;
        }

        private static void SplitContainers(IEnumerable<Container> containers, out List<Container> leftContainers, out List<Container> rightContainers)
        {
            leftContainers = new List<Container>();
            rightContainers = new List<Container>();
            foreach (Container container in containers)
            {
                // implement the 20% marge here
                if (leftContainers.Sum(cntnr => cntnr.Weight) >= rightContainers.Sum(cntnr => cntnr.Weight))
                {
                    rightContainers.Add(container);
                }
                else
                {
                    leftContainers.Add(container);
                }
            }
        }
        private void TryAddDryContainers(ref List<DryContainer> dryContainers, ref List<Container> sideDryContainers, int currentHeight, List<Location> sideLocations)
        {
            foreach (Location location in sideLocations)
            {
                DryContainer dryContainer = (DryContainer)sideDryContainers.FirstOrDefault();
                if (dryContainer == default) break;
                if (currentHeight > 0)
                {
                    double weightOnTopOfContainer = 0;
                    for (int i = currentHeight; i > 0; i--)
                    {
                        Container cn = this[location.X, location.Y, i] ?? default;
                        if (cn == default)
                        {
                            continue;
                        }
                        weightOnTopOfContainer += this[location.X, location.Y, i].Weight;
                    }
                    if (weightOnTopOfContainer > 90_000) continue; // 120_000 is max so now we can also add a valuable container on top.
                }
                this[location] = dryContainer;
                dryContainers.Remove(dryContainer);
            }
        }
        private void TryAddReeferContainers(ref List<ReeferContainer> reeferContainers, ref List<Container> sideDryContainers, int currentHeight, List<Location> sideLocations)
        {
            foreach (Location location in sideLocations)
            {
                ReeferContainer reeferContainer = (ReeferContainer)sideDryContainers.FirstOrDefault();
                if (reeferContainer == default) break;
                if (currentHeight > 0)
                {
                    double weightOnTopOfContainer = 0;
                    for (int i = currentHeight; i > 0; i--)
                    {
                        weightOnTopOfContainer += this[location.X, location.Y, i].Weight;
                    }
                    if (weightOnTopOfContainer > 120_000) continue; // 120_000 is max so now we can also add a valuable container on top.
                }
                this[location] = reeferContainer;
                reeferContainers.Remove(reeferContainer);
            }
        }

        private IEnumerable<Location> GetHighestFreeLocations()
        {
            for (int x = 0; x < Length - 1; x++)
            for (int y = 0; y < Width; y++)
            for (int z = 0; z < Height; z++)
            {
                if (this[x, y, z] != null) continue;
                yield return new Location(x, y, z);
                z = Height;
            }
        }
        private IEnumerable<Location> GetLocationsForHeight(int height)
        {
            for (int x = 0; x < Length - 1; x++)
            for (int y = 0; y < Width; y++)
                yield return new Location(x, y, height);
        }
        private IEnumerable<Location> GetLocationsForLengthAndHeight(int length, int height)
        {
            for (int y = 0; y < Width; y++)
                yield return new Location(Length, y, height);
        }
    }
    public class Location
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        public Location(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
