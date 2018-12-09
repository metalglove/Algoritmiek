using System;
using System.Collections.Generic;
using System.Linq;

namespace Algoritmiek.Circustrein
{
    /// <summary>
    /// Represents a train for the traincarriages to be put on to. 
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
        /// Sorts the animals into train carriages.
        /// </summary>
        /// <param name="animals">The animals to be sorted into train carriages.</param>
        public void Sort(Queue<Animal> animals)
        {
            if (!animals.Any())
                throw new ArgumentException("The queue of animals cannot be empty.");
            Animal animal = animals.Dequeue();
            Carriages.Add(new TrainCarriage(animal));
            while (animals.Any())
            {
                animal = animals.Dequeue();
                if (!TryToAddAnimalIntoAnyOfTheTrainCarriages(animal))
                    Carriages.Add(new TrainCarriage(animal));
            }
        }

        /// <summary>
        /// Tries to add an animal into any of the train carriages if possible.
        /// </summary>
        /// <param name="animal">The animal to be added into a train carriage.</param>
        /// <returns><c>true</c> if the animal was added into a train carriage; otherwise, <c>false</c>.</returns>
        private bool TryToAddAnimalIntoAnyOfTheTrainCarriages(Animal animal)
        {
            for (int counter = 0; counter < Carriages.Count; counter++)
                if (Carriages[counter].TryAddAnimal(animal))
                    return true;
            return false;
        }
    }
}