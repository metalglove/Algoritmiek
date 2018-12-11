using System.Collections.Generic;
using System.Linq;
using Algoritmiek.Circustrein;
using AlgoritmiekTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmiekTests.Assignments.Circustrein
{
    [TestClass]
    public class TrainCarriageTests
    {
        [TestMethod]
        public void TrainCarriage_Should_Contain_1_Animal_Up_On_Creation()
        {
            TrainCarriage trainCarriage = new TrainCarriage(new Animal(Size.Big, EatingBehaviour.Carnivore));
            Assert.IsTrue(trainCarriage.Animals.Count.Equals(1));
        }

        [TestMethod]
        public void TrainCarriage_Should_Be_Able_To_Hold_2_Big_Herbivores()
        {
            TrainCarriage trainCarriage = new TrainCarriage(new Animal(Size.Big, EatingBehaviour.Herbivore));
            Assert.IsTrue(trainCarriage.TryAddAnimal(new Animal(Size.Big, EatingBehaviour.Herbivore)));
            Assert.IsTrue(trainCarriage.Animals.Count.Equals(2));
            Assert.IsTrue(trainCarriage.Animals.Where(anml => anml.Size.Equals(Size.Big) && anml.EatingBehaviour.Equals(EatingBehaviour.Herbivore)).ToList().Count.Equals(2));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Not_Be_Able_To_Add_Anything_To_A_Full_TrainCarriage()
        {
            TrainCarriage trainCarriage = new TrainCarriage(new Animal(Size.Big, EatingBehaviour.Herbivore));
            trainCarriage.TryAddAnimal(new Animal(Size.Big, EatingBehaviour.Herbivore));
            Assert.IsFalse(trainCarriage.TryAddAnimal(new Animal(Size.Big, EatingBehaviour.Herbivore)));
            Assert.IsFalse(trainCarriage.TryAddAnimal(new Animal(Size.Medium, EatingBehaviour.Herbivore)));
            Assert.IsFalse(trainCarriage.TryAddAnimal(new Animal(Size.Small, EatingBehaviour.Herbivore)));
            Assert.IsFalse(trainCarriage.TryAddAnimal(new Animal(Size.Big, EatingBehaviour.Carnivore)));
            Assert.IsFalse(trainCarriage.TryAddAnimal(new Animal(Size.Medium, EatingBehaviour.Carnivore)));
            Assert.IsFalse(trainCarriage.TryAddAnimal(new Animal(Size.Small, EatingBehaviour.Carnivore)));
        }
    }
}
