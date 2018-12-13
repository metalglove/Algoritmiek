using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents the container deck.
    /// </summary>
    public class ContainerDeck
    {
        /// <summary>
        /// The freighter this container deck belongs too.
        /// </summary>
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
        /// <returns>The container on the given x, y & z-coordinate.</returns>
        public Container this[int x, int y, int z]
        {
            get => _containerDeckIn3D[x, y, z];
            private set => _containerDeckIn3D[x, y, z] = value;
        }

        /// <summary>
        /// Gets a container from the container deck by <see cref="Location"/>
        /// </summary>
        /// <param name="location">The location for the container.</param>
        /// <returns>The container on the given x, y & z-coordinate using <see cref="Location"/>.</returns>
        private Container this[Location location]
        {
            set => this[location.X, location.Y, location.Z] = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerDeck"/> class.
        /// </summary>
        /// <param name="freighter">The freighter this container deck belongs too.</param>
        /// <param name="length">The maximum length of the container deck.</param>
        /// <param name="width">The maximum width of the container deck.</param>
        /// <param name="height">The maximum height of the container deck.</param>
        public ContainerDeck(Freighter freighter, int length, int width, int height)
        {
            Length = length;
            Width = width;
            Height = height;
            _freighter = freighter;
            _containerDeckIn3D = new Container[length, width, height];
        }

        /// <summary>
        /// Gets containers from the given height.
        /// </summary>
        /// <param name="height">The given height.</param>
        /// <returns>The containers on the given height.</returns>
        public Container[,] GetContainersFromHeight(int height)
        {
            Container[,] containers = new Container[Length, Width];
            for (int x = 0; x < Length; x++)
            for (int y = 0; y < Width; y++)
                containers[x, y] = this[x, y, height];
            return containers;
        }

        /// <summary>
        /// Gets the left and right weight of the freighter as a tuple.
        /// </summary>
        /// <returns>The left and right weight of the freighter as a tuple</returns>
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

        /// <summary>
        /// Tries to add containers on top of the other containers.
        /// </summary>
        /// <param name="containers">The dry containers to add on top of the other dry containers.</param>
        /// <typeparam name="T">The type of container.</typeparam>
        /// <param name="minimumContainersToPlace">The minimum containers it needs to place.</param>
        public void TryAddContainersOnTop<T>(ref List<T> containers, int minimumContainersToPlace) where T : Container
        {
            List<T> implacableContainers = new List<T>();
            List<T> placedContainers = new List<T>();
            List<Location> freeLocations;

            if (typeof(T) == typeof(DryContainer))
                freeLocations = GetHighestFreeLocationsFullLength().ToList();
            else if (typeof(T) == typeof(ValuableContainer) || typeof(T) == typeof(ReeferContainer))
                freeLocations = GetHighestFreeLocationsForLength(Length - 1).ToList();
            else
                throw new ArgumentException();

            foreach (T container in containers.Take(minimumContainersToPlace))
            {
                Location locationToDelete = default;
                foreach (Location location in freeLocations)
                {
                    if (_freighter.CheckIfContainerExceedsMaximumWeight(container)) return;
                    double weightOnTopOfContainer = 0;
                    if (this[location.X, location.Y, location.Z - 1].GetType() == typeof(ValuableContainer))
                    {
                        locationToDelete = location;
                        break;
                    }
                    for (int i = location.Z; i > 1; i--)//
                    {
                        if (this[location.X, location.Y, i] == default) continue;
                        weightOnTopOfContainer += this[location.X, location.Y, i].Weight;
                    }
                    if (weightOnTopOfContainer + container.Weight > 120_000) continue;
                    this[location] = container;
                    placedContainers.Add(container);
                    locationToDelete = location;
                    break;
                }

                if (locationToDelete.Equals(default))
                    implacableContainers.Add(container);
                else
                    freeLocations.Remove(locationToDelete);
            }

            foreach (T placedDryContainer in placedContainers)
                containers.Remove(placedDryContainer);
        }

        /// <summary>
        /// Tries to add all containers on to the container deck.
        /// </summary>
        /// <typeparam name="T">The type of container.</typeparam>
        /// <param name="originalContainers">The original list of containers.</param>
        public void TryAddAllContainers<T>(ref List<T> originalContainers) where T : Container
        {
            if (!originalContainers.Any())
                return;
            List<Container> originalContainersCopyToCheckFrom = new List<Container>(originalContainers);
            List<Container> originalContainersCopyToUse = new List<Container>(originalContainers);
            SplitContainers(originalContainersCopyToUse, out List<Container> left, out List<Container> right);

            int currentHeight = 0;
            while (originalContainersCopyToUse.Any() && currentHeight < Height - 1)
            {
                if (Width % 2 == 0)
                    TryAddContainersBasedOnEvenWidth<T>(ref originalContainersCopyToUse, ref left, ref right, currentHeight);
                else
                    TryAddContainersBasedOnOddWidth<T>(ref originalContainersCopyToUse, ref left, ref right, currentHeight);
                currentHeight++;
            }

            foreach (Container container in originalContainersCopyToCheckFrom.Except(originalContainersCopyToUse))
            {
                originalContainers.Remove(container as T);
            }
        }

        /// <summary>
        /// Tries to add containers based on the odd width.
        /// </summary>
        /// <typeparam name="T">The typeof container to add.</typeparam>
        /// <param name="containers">The containers to add.</param>
        /// <param name="containersForLeftLocations">The containers for the left locations.</param>
        /// <param name="containersForRightLocations">The containers for the right locations.</param>
        /// <param name="currentHeight">The current height of the locations.</param>
        private void TryAddContainersBasedOnOddWidth<T>(ref List<Container> containers, ref List<Container> containersForLeftLocations, ref List<Container> containersForRightLocations, int currentHeight) where T : Container
        {
            int widthLeft = (int)Math.Floor((double)Width / 2) - 1;
            int middle = widthLeft + 1;
            int widthRight = middle + 1;
            List<Location> rightLocations = GetFreeLocationsBasedOn<T>(currentHeight).Where(location => location.Y >= widthRight).OrderByDescending(location => location.Y).ToList();
            List<Location> leftLocations = GetFreeLocationsBasedOn<T>(currentHeight).Where(location => location.Y <= widthLeft).OrderBy(location => location.Y).ToList();
            List<Location> middleLocations = GetFreeLocationsBasedOn<T>(currentHeight).Where(location => location.Y == middle).ToList();

            int lengthHalf = (int)Math.Floor((double)Length / 2);
            List<Container> middleContainers = currentHeight % 2 == 0
                ? new List<Container>(containersForLeftLocations.Take(lengthHalf).Union(containersForRightLocations.Take(lengthHalf).ToList()))
                : new List<Container>(containersForRightLocations.Take(lengthHalf).Union(containersForLeftLocations.Take(lengthHalf).ToList()));

            List<Container> containersToRemove = new List<Container>(middleContainers);
            if (middleContainers.Any())
                TryAddContainers(ref containers, ref middleContainers, currentHeight, middleLocations);

            foreach (Container dryContainer in containersToRemove.Except(middleContainers))
            {
                containersForLeftLocations.Remove(dryContainer);
                containersForRightLocations.Remove(dryContainer);
            }
            TryAddContainersToLeftAndRightSide(ref containers, ref containersForLeftLocations, ref containersForRightLocations, currentHeight, rightLocations, leftLocations);
        }

        /// <summary>
        /// Tries to add containers to the left and right side of the freighter.
        /// </summary>
        /// <param name="containers">The containers to add.</param>
        /// <param name="containersForLeftLocations">The containers for the left locations.</param>
        /// <param name="containersForRightLocations">The containers for the right locations.</param>
        /// <param name="currentHeight">The current height of the locations.</param>
        /// <param name="rightLocations">The locations for the right side.</param>
        /// <param name="leftLocations">The locations for the left side.</param>
        private void TryAddContainersToLeftAndRightSide(ref List<Container> containers, ref List<Container> containersForLeftLocations, ref List<Container> containersForRightLocations, int currentHeight, IEnumerable<Location> rightLocations, IEnumerable<Location> leftLocations)
        {
            if (containersForLeftLocations.Any())
                TryAddContainers(ref containers, ref containersForLeftLocations, currentHeight, leftLocations);
            if (containersForRightLocations.Any())
                TryAddContainers(ref containers, ref containersForRightLocations, currentHeight, rightLocations);
        }

        /// <summary>
        /// Tries to add containers based on the even width.
        /// </summary>
        /// <typeparam name="T">The type of container to add.</typeparam>
        /// <param name="containers">The containers to add.</param>
        /// <param name="containersForLeftLocations">The containers for the left locations.</param>
        /// <param name="containersForRightLocations">The containers for the right locations.</param>
        /// <param name="currentHeight">The current height of the locations.</param>
        private void TryAddContainersBasedOnEvenWidth<T>(ref List<Container> containers, ref List<Container> containersForLeftLocations, ref List<Container> containersForRightLocations, int currentHeight) where T : Container
        {
            List<Location> rightLocations = GetFreeLocationsBasedOn<T>(currentHeight).Where(location => location.Y >= Width / 2).OrderByDescending(location => location.Y).ToList();
            List<Location> leftLocations = GetFreeLocationsBasedOn<T>(currentHeight).Where(location => location.Y < Width / 2).OrderBy(location => location.Y).ToList();
            TryAddContainersToLeftAndRightSide(ref containers, ref containersForLeftLocations, ref containersForRightLocations, currentHeight, rightLocations, leftLocations);
        }
        
        /// <summary>
        /// Tries to add containers to the given locations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="containers">The original containers to remove the added containers from.</param>
        /// <param name="sideContainers">The containers to add to the given locations.</param>
        /// <param name="currentHeight">The current height of the containers.</param>
        /// <param name="sideLocations">The locations to add the containers to.</param>
        private void TryAddContainers<T>(ref List<T> containers, ref List<T> sideContainers, int currentHeight, IEnumerable<Location> sideLocations) where T : Container
        {
            foreach (Location location in sideLocations)
            {
                T container = sideContainers.FirstOrDefault();
                if (container == default) return;
                if (_freighter.CheckIfContainerExceedsMaximumWeight(container)) return;
                if (location.Z > 0)
                    if (this[location.X, location.Y, location.Z - 1].GetType() == typeof(ValuableContainer))
                        goto IsValuable;
                if (currentHeight > 0)
                {
                    double weightOnTopOfContainer = 0;
                    for (int i = currentHeight; i > 1; i--)
                    {
                        if (this[location.X, location.Y, i] == default) continue;
                        weightOnTopOfContainer += this[location.X, location.Y, i].Weight;
                    }
                    if (typeof(T) == typeof(DryContainer) && weightOnTopOfContainer > 90_000) continue; // 90_000 is max so now we can also add a valuable container on top.
                    if ((typeof(T) == typeof(ReeferContainer) || typeof(T) == typeof(ValuableContainer)) && weightOnTopOfContainer > 120_000) continue; // 120_000 is max so now we stop.
                }
                this[location] = container;
                containers.Remove(container);
                sideContainers.Remove(container);
                IsValuable:
                    continue;
            }
        }

        /// <summary>
        /// Gets the free locations based on the generic type and the given height.
        /// </summary>
        /// <typeparam name="T">The type of container.</typeparam>
        /// <param name="currentHeight">The current height.</param>
        /// <exception cref="ArgumentException">If the typeof t is not found an argument exceptions is thrown.</exception>
        /// <returns>The list of locations.</returns>
        private IEnumerable<Location> GetFreeLocationsBasedOn<T>(int currentHeight) where T : Container
        {
            if (typeof(T) == typeof(DryContainer))
                return GetLocationsForHeight(currentHeight);
            if (typeof(T) == typeof(ReeferContainer))
                return GetLocationsForLengthAndHeight(Length - 1, currentHeight);
            if (typeof(T) == typeof(ValuableContainer))
                return GetHighestFreeLocationsWithoutReeferLocations();
            throw new ArgumentException(nameof(T));
        }

        /// <summary>
        /// Splits the containers by even and odd for left and right.
        /// </summary>
        /// <param name="containers">The containers to split by even and odd for left and right.</param>
        /// <param name="left">The left output list of containers.</param>
        /// <param name="right">The right output list of containers.</param>
        private static void SplitContainers(IEnumerable<Container> containers, out List<Container> left, out List<Container> right)
        {
            Dictionary<bool, IEnumerable<Container>> groups = containers.Select((item, index) => new IndexedContainer { Container = item, Index = index })
                .GroupBy(x => x.Index % 2 == 0)
                .ToDictionary(g => g.Key, g => g.Select(groupedItem => groupedItem.Container));
            left = groups.ContainsKey(false) ? groups[false].ToList() : new List<Container>();
            right = groups.ContainsKey(true) ? groups[true].ToList() : new List<Container>();
        }

        /// <summary>
        /// Gets the highest free locations without filtering (so full length).
        /// </summary>
        /// <returns>The highest free locations without filtering (so full length).</returns>
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

        /// <summary>
        /// Gets the highest free locations with filtering (so without the reefer locations).
        /// </summary>
        /// <returns>The highest free locations with filtering (so without the reefer locations).</returns>
        private IEnumerable<Location> GetHighestFreeLocationsWithoutReeferLocations()
        {
            return GetHighestFreeLocationsFullLength().Where(location => location.X != Length - 1);
        }

        /// <summary>
        /// Gets locations for the given height.
        /// </summary>
        /// <param name="height">The given height.</param>
        /// <returns>The locations for the given height.</returns>
        private IEnumerable<Location> GetLocationsForHeight(int height)
        {
            // Length - 1, so the Reefers have more space
            for (int x = 0; x < Length - 1; x++)
            for (int y = 0; y < Width; y++)
                yield return new Location(x, y, height);
        }

        /// <summary>
        /// Gets the locations for the given height and length.
        /// </summary>
        /// <param name="length">The given length.</param>
        /// <param name="height">The given height.</param>
        /// <returns>The locations for the given height and length.</returns>
        private IEnumerable<Location> GetLocationsForLengthAndHeight(int length, int height)
        {
            for (int y = 0; y < Width; y++)
                yield return new Location(length, y, height);
        }

        /// <summary>
        /// Gets the highest free locations for the given length.
        /// </summary>
        /// <param name="length">The given length.</param>
        /// <returns>The free locations for the given height and length.</returns>
        private IEnumerable<Location> GetHighestFreeLocationsForLength(int length)
        {
            for (int y = 0; y < Width; y++)
            for (int z = 0; z < Height; z++)
            {
                if (this[length, y, z] != null) continue;
                yield return new Location(length, y, z);
                z = Height;
            }
        }

        /// <summary>
        /// Represents an indexed container.
        /// </summary>
        private struct IndexedContainer
        {
            public int Index { get; set; }
            public Container Container { get; set; }
        }

        /// <summary>
        /// Represents a location in the 3d container array.
        /// </summary>
        [DebuggerDisplay("{ToString()}")]
        private struct Location
        {
            /// <summary>
            /// Gets the X coordinate in 3d space.
            /// </summary>
            public int X { get; }

            /// <summary>
            /// Gets the Y coordinate in 3d space.
            /// </summary>
            public int Y { get; }

            /// <summary>
            /// Gets the Z coordinate in 3d space.
            /// </summary>
            public int Z { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Location"/> struct.
            /// </summary>
            /// <param name="x">The X coordinate in 3d space.</param>
            /// <param name="y">The Y coordinate in 3d space.</param>
            /// <param name="z">The Z coordinate in 3d space.</param>
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
}
