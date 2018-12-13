namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents a valuable container.
    /// </summary>
    public class ValuableContainer : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValuableContainer"/> class.
        /// </summary>
        /// <param name="freight">The freight for the container.</param>
        public ValuableContainer(Freight freight) : base(freight)
        {
        }
    }
}
