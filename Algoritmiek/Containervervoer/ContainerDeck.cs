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
            set => _containerDeckIn3D[x, y, z] = value;
        }

        /// <summary>
        /// Gets or sets a container from/on the container deck by <see cref="Location"/>
        /// </summary>
        /// <param name="location">The location for the container.</param>
        /// <returns>The container on the given x, y & z-coordinate using <see cref="Location"/>.</returns>
        private Container this[Location location]
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

        /// <summary>
        /// Tries to add dry containers on top of the other dry containers.
        /// </summary>
        /// <param name="dryContainers">The dry containers to add on top of the other dry containers.</param>
        /// <param name="minimumContainersToPlace">The minimum containers it needs to place.</param>
        /// <param name="minValuableContainers">The minimum valuable containers it needs to leave space for.</param>
        public void TryAddDryContainersOnTop(ref List<DryContainer> dryContainers, int minimumContainersToPlace, int minValuableContainers)
        {
            int maxSpaces = Width * Length;
            if (minValuableContainers + minimumContainersToPlace > maxSpaces)
            {
                // valuable containers are probably worth more... :)
                minimumContainersToPlace = maxSpaces - minValuableContainers;
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

                if (locationToDelete.Equals(default))
                    unPlacableDryContainers.Add(dryContainer);
                else
                    freeLocations.Remove(locationToDelete);
            }

            foreach (DryContainer placedDryContainer in placedDryContainers)
                dryContainers.Remove(placedDryContainer);
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

        public void TryAddValuableContainersOnTopOfReeferContainers(ref List<ValuableContainer> valuableContainers, int minimumContainersToPlace, int minReeferContainers)
        {
            throw new NotImplementedException();
        }

        public void TryAddLastRequiredReeferContainers(ref List<ReeferContainer> reeferContainers, int minimumContainersToPlace)
        {
            throw new NotImplementedException();
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
        /// Tries to add all containers to the container deck.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalContainers"></param>
        public void TryAddAllContainers<T>(ref List<T> originalContainers) where T : Container
        {
            // TODO: Check for originalContainers < 2 
            if (!originalContainers.Any())
                return;
            List<Container> originalContainersCopyToCheckFrom = new List<Container>(originalContainers);
            List<Container> originalContainersCopyToUse = new List<Container>(originalContainers);
            Dictionary<bool, IEnumerable<Container>> groups = originalContainersCopyToUse.Select((item, index) => new IndexedContainer { Container = item, Index = index})
                .GroupBy(x => x.Index % 2 == 0)
                .ToDictionary(g => g.Key, g => g.Select(groupedItem => groupedItem.Container));
            List<Container> left = groups[false].ToList();
            List<Container> right = groups[true].ToList();

            int currentHeight = 0;
            while (originalContainersCopyToUse.Any() && currentHeight < Height - 1)
            {
                List<Location> locations;
                
                if (typeof(T) == typeof(DryContainer))
                {
                    locations = GetLocationsForHeight(currentHeight).ToList();
                }
                else if(typeof(T) == typeof(ReeferContainer))
                {
                    locations = GetLocationsForLengthAndHeight(Length - 1, currentHeight).ToList();
                }
                else if (typeof(T) == typeof(ValuableContainer))
                {
                    locations = GetHighestFreeLocationsWithoutReeferLocations().ToList();
                }
                else
                {
                    throw new ArgumentException();
                }
                List<Location> rightLocations;
                List<Location> leftLocations;
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
                    List<Container> middleContainers = currentHeight % 2 == 0 
                        ? new List<Container>(left.Take(lengthHalf).Union(right.Take(lengthHalf + (Length % 2 == 0 ? 1 : 0)).ToList())) 
                        : new List<Container>(right.Take(lengthHalf).Union(left.Take(lengthHalf + (Length % 2 == 0 ? 1 : 0)).ToList()));

                    List<Container> containersToRemove = new List<Container>(middleContainers);
                    if (middleContainers.Any())
                        TryAddContainers(ref originalContainersCopyToUse, ref middleContainers, currentHeight, middleLocations);

                    foreach (Container dryContainer in containersToRemove.Except(middleContainers))
                    {
                        left.Remove(dryContainer);
                        right.Remove(dryContainer);
                    }
                }
                if (left.Any())
                    TryAddContainers(ref originalContainersCopyToUse, ref left, currentHeight, leftLocations);
                if (right.Any())
                    TryAddContainers(ref originalContainersCopyToUse, ref right, currentHeight, rightLocations);
                currentHeight++;
            }

            foreach (Container container in originalContainersCopyToCheckFrom.Except(originalContainersCopyToUse))
            {
                originalContainers.Remove(container as T);
            }
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
                if (currentHeight > 0)
                {
                    double weightOnTopOfContainer = 0;
                    for (int i = currentHeight; i > 0; i--)
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
            }
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
        /// <param name="length">The given height.</param>
        /// <param name="height">The given length.</param>
        /// <returns>The locations for the given height and length.</returns>
        private IEnumerable<Location> GetLocationsForLengthAndHeight(int length, int height)
        {
            for (int y = 0; y < Width; y++)
                yield return new Location(length, y, height);
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
            /// Initializes a new instance of the <see cref="Location"/> class.
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
