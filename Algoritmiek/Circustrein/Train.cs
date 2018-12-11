using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmiek.Circustrein
{
    /// <summary>
    /// Represents a train for the train carriages to be put on to. 
    /// </summary>
    public class Train
    {
        /// <summary>
        /// Gets the list of train carriages.
        /// </summary>
        public IList<TrainCarriage> Carriages { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Train"/> class.
        /// </summary>
        public Train()
        {
            Carriages = new List<TrainCarriage>();
        }

        /// <summary>
        /// Sorts the animals into train carriages and adds new train carriages where needed.
        /// </summary>
        /// <param name="animals">The animals to be sorted into train carriages.</param>
        public void Sort(Queue<Animal> animals)
        {
            if (!animals.Any())
                throw new ArgumentException("The queue of animals cannot be empty.");
            Carriages.Add(new TrainCarriage(animals.Dequeue()));
            while (animals.Any())
            {
                Animal animal = animals.Dequeue();
                if (!TryToAddAnimalToAnyOfTheTrainCarriages(animal))
                    Carriages.Add(new TrainCarriage(animal));
            }
        }

        /// <summary>
        /// Tries to add an animal into any of the train carriages if possible.
        /// </summary>
        /// <param name="animal">The animal to be added into a train carriage.</param>
        /// <returns><c>true</c> if the animal was added into a train carriage; otherwise, <c>false</c>.</returns>
        private bool TryToAddAnimalToAnyOfTheTrainCarriages(Animal animal) 
            => Carriages.Any(carriage => carriage.TryAddAnimal(animal));
    }
}