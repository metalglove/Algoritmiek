using System;
using Algoritmiek;
using Algoritmiek.Circustrein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmiekTests.Assignments.Circustrein
{
    [TestClass]
    public class CircusTrainProgramTests
    {
        [TestMethod]
        public void CircusTrainProgram_Should_Be_A_Implementation_Of_IProgram()
        {
            Assert.IsTrue(typeof(IProgram).IsAssignableFrom(typeof(CircusTrainProgram)));
        }

        [TestMethod]
        public void Run_Without_Setup_Should_Throw_SetupNotRanException()
        {
            CircusTrainProgram circusTrainProgram = new CircusTrainProgram();
            Assert.ThrowsException<SetupNotRanException>((Action)circusTrainProgram.Run);
        }

        [TestMethod]
        public void Run_With_Setup_Should_Not_Throw_SetupNotRanException()
        {
            CircusTrainProgram circusTrainProgram = new CircusTrainProgram();
            circusTrainProgram.Setup();
            circusTrainProgram.Run();
        }

        [TestMethod]
        public void Setup_Should_Set_HasSetupRan_To_True()
        {
            CircusTrainProgram circusTrainProgram = new CircusTrainProgram();
            Assert.IsFalse(circusTrainProgram.HasSetupRan);
            circusTrainProgram.Setup();
            Assert.IsTrue(circusTrainProgram.HasSetupRan);
        }

        [TestMethod]
        public void Setup_Should_Create_Queue_With_50_Animals()
        {
            CircusTrainProgram circusTrainProgram = new CircusTrainProgram();
            Assert.IsNull(circusTrainProgram.Animals);
            circusTrainProgram.Setup();
            Assert.IsNotNull(circusTrainProgram.Animals);
            Assert.IsTrue(circusTrainProgram.Animals.Count.Equals(50));
        }

        [TestMethod]
        public void Setup_Should_Create_Train()
        {
            CircusTrainProgram circusTrainProgram = new CircusTrainProgram();
            Assert.IsNull(circusTrainProgram.Train);
            circusTrainProgram.Setup();
            Assert.IsNotNull(circusTrainProgram.Train);
        }

        [TestMethod]
        public void Name_And_Description_Should_Be_Set()
        {
            CircusTrainProgram circusTrainProgram = new CircusTrainProgram();
            Assert.IsTrue(circusTrainProgram.Name.Equals("CircusTrainProgram"));
            Assert.IsTrue(circusTrainProgram.Description.Equals("Sort animals into train carriages accordingly."));
        }
    }
}
