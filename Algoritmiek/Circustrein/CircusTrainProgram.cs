using System;
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
        /// Gets the object used for generating random numbers.
        /// </summary>
        private readonly static Random _random = new Random();

        /// <summary>
        /// Gets or sets the queue of animals.
        /// </summary>
        public Queue<Animal> Animals { get; }

        /// <summary>
        /// Gets or sets the train for the circus animals to be put on to.
        /// </summary>
        public Train Train { get; }

        /// <inheritdoc cref="IProgram.Name"/>
        public string Name => GetType().Name;

        /// <inheritdoc cref="IProgram.Description"/>
        public string Description => "Sort animals into train carriages accordingly.";

        /// <inheritdoc cref="IProgram.Description"/>
        public bool HasSetupRan { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CircusTrainProgram"/> class.
        /// </summary>
        public CircusTrainProgram()
        {
            Animals = new Queue<Animal>();
            Train = new Train();
        }

        /// <summary>
        /// Sets up a queue of 50 Animals.
        /// </summary>
        public void Setup()
        {
            Size[] sizes = (Size[])Enum.GetValues(typeof(Size));
            EatingBehaviour[] eatingBehaviours = (EatingBehaviour[])Enum.GetValues(typeof(EatingBehaviour));

            for (int i = 0; i < 50; i++)
                Animals.Enqueue(new Animal(sizes[_random.Next(sizes.Length)], eatingBehaviours[_random.Next(eatingBehaviours.Length)]));

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
