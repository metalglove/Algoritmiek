using System.Collections.Generic;

namespace Algoritmiek.Circustrein
{
    /// <summary>
    /// Represents the implementation for the "Circustrein" assignment. See <see cref="Description"/> for a short description.
    /// <para>
    /// A detailed description can be found under the Assignments folder in "Circustrein.txt".
    /// </para> 
    /// </summary>
    public class CircusTrainProgram : IProgram
    {
        /// <summary>
        /// Gets or sets the queue of animals.
        /// </summary>
        public Queue<Animal> Animals { get; private set; }

        /// <summary>
        /// Gets or sets the train for the circus animals to be put on to.
        /// </summary>
        public Train Train { get; private set; }

        /// <inheritdoc cref="IProgram.Name"/>
        public string Name => GetType().Name;

        /// <inheritdoc cref="IProgram.Description"/>
        public string Description => "Sort animals into train carriages accordingly.";

        /// <inheritdoc cref="IProgram.Description"/>
        public bool HasSetupRan { get; private set; }

        /// <summary>
        /// Sets up a queue of 50 Animals.
        /// </summary>
        public void Setup()
        {
            Animals = new Queue<Animal>();
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Carnivore));
            Animals.Enqueue(new Animal(size: Size.Small, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Medium, eatingBehaviour: EatingBehaviour.Herbivore));
            Animals.Enqueue(new Animal(size: Size.Big, eatingBehaviour: EatingBehaviour.Herbivore));
            Train = new Train();
            HasSetupRan = true;
        }

        /// <summary>
        /// Runs the animal sorting algorithm.
        /// </summary>
        public void Run()
        {
            if (!HasSetupRan)
                throw new SetupNotRanException();
            Train.Sort(Animals);
        }
    }
}
