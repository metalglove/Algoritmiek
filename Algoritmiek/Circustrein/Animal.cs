namespace Algoritmiek.Circustrein
{
    /// <summary>
    /// Represents an animal for the "Circustrein" assignment.
    /// </summary>
    public struct Animal
    {
        /// <summary>
        /// Gets the size of the animal.
        /// </summary>
        public readonly Size Size;

        /// <summary>
        /// Gets the eating behaviour of the animal.
        /// </summary>
        public readonly EatingBehaviour EatingBehaviour;

        /// <summary>
        /// Initializes the a new instance of the <see cref="Animal"/> struct.
        /// </summary>
        /// <param name="size">The size of the animal.</param>
        /// <param name="eatBehaviour">The eating behaviour of the animal.</param>
        public Animal(Size size, EatingBehaviour eatingBehaviour)
        {
            Size = size;
            EatingBehaviour = eatingBehaviour;
        }
    }
}
