using Algoritmiek.Circustrein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmiekTests.Assignments.Circustrein
{
    [TestClass]
    public class TrainCarriageWithBigHerbivoreTests
    {
        public TrainCarriage TrainCarriageWithBigHerbivore { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            TrainCarriageWithBigHerbivore = new TrainCarriage(new Animal(Size.Big, EatingBehaviour.Herbivore));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Return_True_For_Big_Herbivore_Using_TrainCarriageWithBigHerbivore()
        {
            Assert.IsTrue(TrainCarriageWithBigHerbivore.TryAddAnimal(new Animal(Size.Big, EatingBehaviour.Herbivore)));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Return_True_For_Medium_Herbivore_Using_TrainCarriageWithBigHerbivore()
        {
            Assert.IsTrue(TrainCarriageWithBigHerbivore.TryAddAnimal(new Animal(Size.Medium, EatingBehaviour.Herbivore)));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Return_True_For_Small_Herbivore_Using_TrainCarriageWithBigHerbivore()
        {
            Assert.IsTrue(TrainCarriageWithBigHerbivore.TryAddAnimal(new Animal(Size.Small, EatingBehaviour.Herbivore)));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Return_False_For_Big_Carnivore_Using_TrainCarriageWithBigHerbivore()
        {
            Assert.IsFalse(TrainCarriageWithBigHerbivore.TryAddAnimal(new Animal(Size.Big, EatingBehaviour.Carnivore)));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Return_True_For_Medium_Carnivore_Using_TrainCarriageWithBigHerbivore()
        {
            Assert.IsTrue(TrainCarriageWithBigHerbivore.TryAddAnimal(new Animal(Size.Medium, EatingBehaviour.Carnivore)));
        }

        [TestMethod]
        public void TryAddAnimal_Should_Return_True_For_Small_Carnivore_Using_TrainCarriageWithBigHerbivore()
        {
            Assert.IsTrue(TrainCarriageWithBigHerbivore.TryAddAnimal(new Animal(Size.Small, EatingBehaviour.Carnivore)));
        }
    }
}
