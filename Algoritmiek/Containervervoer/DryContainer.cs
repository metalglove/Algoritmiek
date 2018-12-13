namespace Algoritmiek.Containervervoer
{
    /// <summary>
    /// Represents a dry container also known as an intermodal container.
    /// </summary>
    public class DryContainer : Container
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DryContainer"/> class.
        /// </summary>
        /// <param name="freight">The freight for the container.</param>
        public DryContainer(Freight freight) : base(freight)
        {
        }
    }
}
