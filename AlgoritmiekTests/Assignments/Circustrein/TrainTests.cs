using System;
using System.Collections.Generic;
using System.Linq;
using Algoritmiek.Circustrein;
using AlgoritmiekTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmiekTests.Assignments.Circustrein
{
    [TestClass]
    public class TrainTests
    {
        [TestMethod]
        public void Train_Should_Have_4_TrainCarriages_After_Sorting_10_Animals()
        {
            Train train = new Train();
            Animal[] animals =
            {
                new Animal(Size.Big, EatingBehaviour.Carnivore),
                new Animal(Size.Big, EatingBehaviour.Herbivore),
                new Animal(Size.Small, EatingBehaviour.Herbivore),
                new Animal(Size.Small, EatingBehaviour.Herbivore),
                new Animal(Size.Medium, EatingBehaviour.Herbivore),
                new Animal(Size.Small, EatingBehaviour.Herbivore),
                new Animal(Size.Big, EatingBehaviour.Herbivore),
                new Animal(Size.Small, EatingBehaviour.Herbivore),
                new Animal(Size.Big, EatingBehaviour.Herbivore),
                new Animal(Size.Medium, EatingBehaviour.Carnivore)
            };
            train.Sort(new Queue<Animal>(animals));
            PrivateObject<Train> privateObject = new PrivateObject<Train>(ref train, "Carriages", PrivateType.Property);
            IList<TrainCarriage> carriages = privateObject.Value;
            Assert.IsTrue(carriages.Count.Equals(4));
        }

        [TestMethod]
        public void Sort_Should_Throw_ArgumentException_With_Empty_Animal_Queue()
        {
            Train train = new Train();
            ArgumentException exception = Assert.ThrowsException<ArgumentException>(() => train.Sort(new Queue<Animal>()));
            Assert.AreEqual(exception.Message, "The queue of animals cannot be empty.");
        }

        [TestMethod]
        public void Carriages_Should_Be_Empty_Up_On_Creation_Of_Train()
        {
            Train train = new Train();
            PrivateObject<Train> privateObject = new PrivateObject<Train>(ref train, "Carriages", PrivateType.Property);
            IList<TrainCarriage> carriages = privateObject.Value;
            Assert.IsFalse(carriages.Any());
        }

        [TestMethod]
        public void Carriages_Should_Not_Be_Empty_After_Calling_Sort_With_Some_Animals()
        {
            Train train = new Train();
            Animal[] animals = 
            {
                new Animal(Size.Big, EatingBehaviour.Carnivore),
                new Animal(Size.Big, EatingBehaviour.Herbivore),
                new Animal(Size.Big, EatingBehaviour.Herbivore),
                new Animal(Size.Big, EatingBehaviour.Herbivore),
                new Animal(Size.Medium, EatingBehaviour.Carnivore)
            };
            train.Sort(new Queue<Animal>(animals));
            PrivateObject<Train> privateObject = new PrivateObject<Train>(ref train, "Carriages", PrivateType.Property);
            IList<TrainCarriage> carriages = privateObject.Value;
            Assert.IsTrue(carriages.Any());
        }

        [TestMethod]
        public void TryToAddAnimalToAnyOfTheTrainCarriages_Should_Return_False_When_Adding_The_Second_Big_Carnivore()
        {
            Train train = new Train();
            Queue<Animal> animals = new Queue<Animal>();
            animals.Enqueue(new Animal(Size.Big, EatingBehaviour.Carnivore));
            train.Sort(animals);
            PrivateObject<Train> privateObject = 
                new PrivateObject<Train>(
                    ref train, 
                    "TryToAddAnimalToAnyOfTheTrainCarriages", 
                    PrivateType.MethodWithReturnValue, 
                    new object[]{ new Animal(Size.Big, EatingBehaviour.Carnivore) });
            bool isAdded = privateObject.Value;
            Assert.IsFalse(isAdded);
        }

        [TestMethod]
        public void TryToAddAnimalToAnyOfTheTrainCarriages_Should_Return_True_When_Adding_The_Second_Big_Herbivore()
        {
            Train train = new Train();
            Queue<Animal> animals = new Queue<Animal>();
            animals.Enqueue(new Animal(Size.Big, EatingBehaviour.Herbivore));
            train.Sort(animals);
            PrivateObject<Train> privateObject =
                new PrivateObject<Train>(
                    ref train,
                    "TryToAddAnimalToAnyOfTheTrainCarriages",
                    PrivateType.MethodWithReturnValue,
                    new object[] { new Animal(Size.Big, EatingBehaviour.Herbivore) });
            bool isAdded = privateObject.Value;
            Assert.IsTrue(isAdded);
        }
    }
}
