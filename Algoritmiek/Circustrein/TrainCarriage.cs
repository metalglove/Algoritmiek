﻿using System.Collections.Generic;
using System.Linq;

namespace Algoritmiek.Circustrein
{
    /// <summary>
    /// Represents a train carriage to be put on to a train.
    /// </summary>
    public class TrainCarriage
    {
        /// <summary>
        /// A list of animals.
        /// </summary>
        public List<Animal> Animals { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainCarriage"/> class.
        /// </summary>
        /// <param name="animal">The first animal to be added the train carriage.</param>
        public TrainCarriage(Animal animal)
        {
            Animals = new List<Animal> { animal };
        }

        /// <summary>
        /// Tries to add an animal into the train carriage if possible.
        /// </summary>
        /// <param name="animal">The animal to be added.</param>
        /// <returns><c>true</c> if the animal was added into the train carriage; otherwise, <c>false</c>.</returns>
        public bool TryAddAnimal(Animal animal)
        {
            if (Animals.Sum(anml => (int)anml.Size) + (int)animal.Size > 10)
                return false;
            if (animal.EatingBehaviour.Equals(EatingBehaviour.Carnivore))
            {
                if (Animals.Exists(anml => anml.EatingBehaviour.Equals(EatingBehaviour.Carnivore)))
                    return false;
                if (Animals.Any(anml => (int)anml.Size <= (int)animal.Size))
                    return false;
            }
            else if (Animals.Exists(anml => anml.EatingBehaviour.Equals(EatingBehaviour.Carnivore) && (int)anml.Size >= (int)animal.Size))
                return false;
            Animals.Add(animal);
            return true;
        }
    }
}