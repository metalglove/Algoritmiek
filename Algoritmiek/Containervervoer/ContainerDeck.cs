using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algoritmiek.Containervervoer
{
    public class ContainerDeck
    {
        private readonly Freighter _freighter;

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
                for (int j = 0; j < Width; j++)
                for (int k = 0; k < Height; k++)
                    if (this[i, j, k] != null)
                        returnValue += this[i, j, k].Weight;

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
            get => _containerDeckIn3D[x, y, z];
            set => _containerDeckIn3D[x, y, z] = value;
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
        /// <param name="freighter">The freighter this container deck belongs too.</param>
        /// <param name="length">the maximum length of the container deck.</param>
        /// <param name="width">the maximum width of the container deck.</param>
        /// <param name="height">the maximum height of the container deck.</param>
        public ContainerDeck(Freighter freighter, int length, int width, int height)
        {
            Length = length;
            Width = width;
            Height = height;
            _freighter = freighter;
            _containerDeckIn3D = new Container[length, width, height];
        }

        public void TryAddDryContainersOnTop(ref List<DryContainer> dryContainers, int minimumContainersToPlace, int minValuableContainers)
        {
            int maxPlaces = Width * Length;
            if (minValuableContainers + minimumContainersToPlace > maxPlaces)
            {
                // valuable containers are probably worth more... :)
                minimumContainersToPlace = maxPlaces - minValuableContainers;
            }

            List<DryContainer> unPlacableDryContainers = new List<DryContainer>();
            List<DryContainer> placedDryContainers = new List<DryContainer>();

            List<Location> freeLocations = GetHighestFreeLocationsFullLength().ToList();
            foreach (DryContainer dryContainer in dryContainers.Take(minimumContainersToPlace))
            {
                Location locationToDelete = default;
                foreach (Location location in freeLocations)
                {
                    if (_freighter.CheckIfContainerExceedsMaximumWeight(dryContainer)) return;
                    double weightOnTopOfContainer = 0;
                    for (int i = location.Z; i > 0; i--)
                    {
                        if (this[location.X, location.Y, i] == default) continue;
                        weightOnTopOfContainer += this[location.X, location.Y, i].Weight;
                    }
                    if (weightOnTopOfContainer + dryContainer.Weight > 120_000) continue;
                    this[location] = dryContainer;
                    placedDryContainers.Add(dryContainer);
                    locationToDelete = location;
                    break;
                }

                if (locationToDelete == default)
                    unPlacableDryContainers.Add(dryContainer);
                else
                    freeLocations.Remove(locationToDelete);
            }

            foreach (DryContainer placedDryContainer in placedDryContainers)
                dryContainers.Remove(placedDryContainer);
        }

        public Tuple<double, double> GetLeftAndRightWeights()
        {
            double left = 0;
            double right = 0;

            if (Width % 2 == 0)
            {
                int width = Width / 2;
                for (int i = 0; i < Length; i++)
                for (int k = 0; k < Height; k++)
                {
                    for (int j = 0; j < width; j++)
                        if (this[i, j, k] != null)
                            left += this[i, j, k].Weight;
                    for (int j2 = width; j2 < Width; j2++)
                        if (this[i, j2, k] != null)
                            right += this[i, j2, k].Weight;
                }
            }
            else
            {
                int widthLeft = (int)Math.Floor((double)Width / 2) - 1;
                int middle = widthLeft + 1;
                int widthRight = middle + 1;
                for (int i = 0; i < Length; i++)
                for (int k = 0; k < Height; k++)
                {
                    for (int j = 0; j <= widthLeft; j++)
                        if (this[i, j, k] != null)
                            left += this[i, j, k].Weight;
                    for (int j2 = widthRight; j2 < Width; j2++)
                        if (this[i, j2, k] != null)
                            right += this[i, j2, k].Weight;

                    if (this[i, middle, k] == null) continue;
                    double halfWeight = (this[i, middle, k].Weight) / 2;
                    left += halfWeight;
                    right += halfWeight;
                }
            }

            return new Tuple<double, double>(left, right);
        }

        internal void TryAddValuableContainersOnTopOfReeferContainers(ref List<ValuableContainer> valuableContainers, int minimumContainersToPlace, int minReeferContainers)
        {
            throw new NotImplementedException();
        }

        internal void TryAddLastRequiredReeferContainers(ref List<ReeferContainer> reeferContainers, int minimumContainersToPlace)
        {
            throw new NotImplementedException();
        }

        private struct GroupedItem
        {
            public int Index { get; set; }
            public Container Container { get; set; }
        }

        public void TryAddAllDryContainers(ref List<DryContainer> dryContainers)
        {
            if (!dryContainers.Any())
                return;
            // also create check for 1 container in reeferContainers
            Dictionary<bool, IEnumerable<DryContainer>> groups = dryContainers.Select((item, index) => new GroupedItem { Container = item, Index = index})
                .GroupBy(x => x.Index % 2 == 0)
                .ToDictionary(g => g.Key, g => g.Select(groupedItem => groupedItem.Container as DryContainer));
            List<DryContainer> left = groups[false].ToList();
            List<DryContainer> right = groups[true].ToList();

            int currentHeight = 0;
            List<Location> rightLocations;
            List<Location> leftLocations;
            while (dryContainers.Any() && currentHeight < Height - 1)
            {
                List<Location> locations = GetLocationsForHeight(currentHeight).ToList();
                if (Width % 2 == 0)
                {
                    rightLocations = locations.Where(location => location.Y >= Width / 2).OrderByDescending(location => location.Y).ToList();
                    leftLocations = locations.Where(location => location.Y < Width / 2).OrderBy(location => location.Y).ToList();
                }
                else
                {
                    int widthLeft = (int)Math.Floor((double)Width / 2) - 1;
                    int middle = widthLeft + 1;
                    int widthRight = middle + 1;
                    rightLocations = locations.Where(location => location.Y >= widthRight).OrderByDescending(location => location.Y).ToList();
                    leftLocations = locations.Where(location => location.Y <= widthLeft).OrderBy(location => location.Y).ToList();
                    List<Location> middleLocations = locations.Where(location => location.Y == middle).ToList();

                    int lengthHalf = (int)Math.Floor((double)Length / 2);
                    List<DryContainer> middleContainers = currentHeight % 2 == 0 
                        ? new List<DryContainer>(left.Take(lengthHalf).Union(right.Take(lengthHalf + (Length % 2 == 0 ? 1 : 0)).ToList())) 
                        : new List<DryContainer>(right.Take(lengthHalf).Union(left.Take(lengthHalf + (Length % 2 == 0 ? 1 : 0)).ToList()));

                    List<DryContainer> containersToRemove = new List<DryContainer>(middleContainers);
                    if (middleContainers.Any())
                        TryAddContainers(ref dryContainers, ref middleContainers, currentHeight, middleLocations);

                    foreach (DryContainer dryContainer in containersToRemove.Except(middleContainers))
                    {
                        left.Remove(dryContainer);
                        right.Remove(dryContainer);
                    }
                }
                if (left.Any())
                    TryAddContainers(ref dryContainers, ref left, currentHeight, leftLocations);
                if (right.Any())
                    TryAddContainers(ref dryContainers, ref right, currentHeight, rightLocations);
                currentHeight++;
            }
        }
        public void TryAddAllValuableContainers(ref List<ValuableContainer> valuableContainers)
        {
            // TODO: make sure they are divided even
            foreach (Location location in GetHighestFreeLocationsWithoutReeferLocations())
            {
                ValuableContainer valuableContainer = valuableContainers.FirstOrDefault();
                if (valuableContainer == default) return;
                if (_freighter.CheckIfContainerExceedsMaximumWeight(valuableContainer)) return;
                this[location] = valuableContainer;
                valuableContainers.Remove(valuableContainer);
            }
        }
        public void TryAddAllReeferContainers(ref List<ReeferContainer> reeferContainers)
        {
            if (!reeferContainers.Any() || reeferContainers.Count <= 2)
                throw new ArgumentException("reeferContainers is too small.");
            // TODO: also create check for 1 container in reeferContainers
            Dictionary<bool, IEnumerable<ReeferContainer>> groups = reeferContainers.Select((item, index) => new GroupedItem { Container = item, Index = index })
                .GroupBy(x => x.Index % 2 == 0)
                .ToDictionary(g => g.Key, g => g.Select(groupedItem => groupedItem.Container as ReeferContainer));
            List<ReeferContainer> left = groups[false].ToList();
            List<ReeferContainer> right = groups[true].ToList();

            int currentHeight = 0;
            List<Location> leftLocations;
            List<Location> rightLocations;
            while (reeferContainers.Any() && currentHeight < Height)
            {
                List<Location> locations = GetLocationsForLengthAndHeight(Length - 1, currentHeight).ToList();
                if (Width % 2 == 0)
                {
                    rightLocations = locations.Where(location => location.Y >= Width / 2).OrderByDescending(location => location.Y).ToList();
                    leftLocations = locations.Where(location => location.Y < Width / 2).OrderBy(location => location.Y).ToList();
                }
                else
                {
                    int widthLeft = (int)Math.Floor((double)Width / 2) - 1;
                    int middle = widthLeft + 1;
                    int widthRight = middle + 1;
                    rightLocations = locations.Where(location => location.Y >= widthRight).OrderByDescending(location => location.Y).ToList();
                    leftLocations = locations.Where(location => location.Y <= widthLeft).OrderBy(location => location.Y).ToList();
                    List<Location> middleLocations = locations.Where(location => location.Y == middle).ToList();

                    int lengthHalf = (int)Math.Floor((double)Length / 2);
                    List<ReeferContainer> middleContainers = currentHeight % 2 == 0
                        ? new List<ReeferContainer>(left.Take(lengthHalf).Union(right.Take(lengthHalf + (Length % 2 == 0 ? 1 : 0)).ToList()))
                        : new List<ReeferContainer>(right.Take(lengthHalf).Union(left.Take(lengthHalf + (Length % 2 == 0 ? 1 : 0)).ToList()));

                    List<ReeferContainer> containersToRemove = new List<ReeferContainer>(middleContainers);
                    if (middleContainers.Any())
                        TryAddContainers(ref reeferContainers, ref middleContainers, currentHeight, middleLocations);

                    foreach (ReeferContainer dryContainer in containersToRemove.Except(middleContainers))
                    {
                        left.Remove(dryContainer);
                        right.Remove(dryContainer);
                    }
                }
                if (left.Any())
                    TryAddContainers(ref reeferContainers, ref left, currentHeight, leftLocations);
                if (right.Any())
                    TryAddContainers(ref reeferContainers, ref right, currentHeight, rightLocations);
                currentHeight++;
            }
        }
        public Container[,] GetContainersFromHeight(int height)
        {
            Container[,] containers = new Container[Length, Width];
            for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++)
            {
                //
                containers[x, y] = this[x, y, height];
            }
            return containers;
        }
        private void TryAddContainers<T>(ref List<T> containers, ref List<T> sideContainers, int currentHeight, IEnumerable<Location> sideLocations) where T : Container
        {
            foreach (Location location in sideLocations)
            {
                T container = sideContainers.FirstOrDefault();
                if (container == default) return;
                if (_freighter.CheckIfContainerExceedsMaximumWeight(container)) return;
                if (currentHeight > 0)
                {
                    double weightOnTopOfContainer = 0;
                    for (int i = currentHeight; i > 0; i--)
                    {
                        if (this[location.X, location.Y, i] == default) continue;
                        weightOnTopOfContainer += this[location.X, location.Y, i].Weight;
                    }
                    if (weightOnTopOfContainer > 90_000) continue; // 90_000 is max so now we can also add a valuable container on top.
                    //if (typeof(T) == typeof(DryContainer) && weightOnTopOfContainer > 90_000) continue; // 90_000 is max so now we can also add a valuable container on top.
                    //if (typeof(T) == typeof(ReeferContainer) && weightOnTopOfContainer > 120_000) continue; // 120_000 is max so now we stop.
                }
                this[location] = container;
                containers.Remove(container);
                sideContainers.Remove(container);
            }
        }
        private IEnumerable<Location> GetHighestFreeLocationsFullLength()
        {
            for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++)
            for (int z = 0; z < Height; z++)
            {
                if (this[x, y, z] != null) continue;
                yield return new Location(x, y, z);
                z = Height;
            }
        }
        private IEnumerable<Location> GetHighestFreeLocationsWithoutReeferLocations()
        {
            // Length - 1, so the Reefers have more space
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
            // Length - 1, so the Reefers have more space
            for (int x = 0; x < Length - 1; x++)
            for (int y = 0; y < Width; y++)
                yield return new Location(x, y, height);
        }
        private IEnumerable<Location> GetLocationsForLengthAndHeight(int length, int height)
        {
            for (int y = 0; y < Width; y++)
                yield return new Location(length, y, height);
        }
    }
    [DebuggerDisplay("{ToString()}")]
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

        /// <inheritdoc />
        public override string ToString() => $"X {X}, Y: {Y}, Z: {Z}";
    }
}
