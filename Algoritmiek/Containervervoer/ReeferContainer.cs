namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents a refrigerated container also known as a reefer container.
    /// </summary>
    public class ReeferContainer : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReeferContainer"/> class.
        /// </summary>
        /// <param name="freight">The freight for the container.</param>
        public ReeferContainer(Freight freight) : base(freight)
        {
        }
    }
}
