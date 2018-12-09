using System;
using Algoritmiek;
using Algoritmiek.Circustrein;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlgoritmiekTests
{
    [TestClass]
    public class CircusTrainProgramTests
    {
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

        // Herbivore mag niet bij carnivore als de Herbivore kleiner of even groot is

        // Carnivores mogen niet bij elkaar

        // De maximale grootte van een wagon is 10 in punten (de 'Size' van de dieren bij elkaar opgeteld mag niet meer zijn dan 10)

        // Sorteer 10 animals en bekijk het resultaat goed is
    }
}
