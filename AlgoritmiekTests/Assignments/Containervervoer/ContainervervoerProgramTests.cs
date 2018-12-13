using Algoritmiek;
using Algoritmiek.Containervervoer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlgoritmiekTests.Assignments.Containervervoer
{
    [TestClass]
    public class ContainervervoerProgramTests
    {
        [TestMethod]
        public void ContainervervoerProgram_Should_Be_A_Implementation_Of_IProgram()
        {
            Assert.IsTrue(typeof(IProgram).IsAssignableFrom(typeof(ContainervervoerProgram)));
        }

        [TestMethod]
        public void Run_Without_Setup_Should_Throw_SetupNotRanException()
        {
            ContainervervoerProgram containervervoerProgram = new ContainervervoerProgram();
            Assert.ThrowsException<SetupNotRanException>((Action)containervervoerProgram.Run);
        }

        [TestMethod]
        public void Run_With_Setup_Should_Not_Throw_SetupNotRanException()
        {
            ContainervervoerProgram containervervoerProgram = new ContainervervoerProgram();
            containervervoerProgram.Setup();
            containervervoerProgram.Run();
        }

        [TestMethod]
        public void Setup_Should_Set_HasSetupRan_To_True()
        {
            ContainervervoerProgram containervervoerProgram = new ContainervervoerProgram();
            Assert.IsFalse(containervervoerProgram.HasSetupRan);
            containervervoerProgram.Setup();
            Assert.IsTrue(containervervoerProgram.HasSetupRan);
        }
    }
}
