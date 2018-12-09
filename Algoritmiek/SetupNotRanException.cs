using System;

namespace Algoritmiek
{
    /// <summary>
    /// Represents an exception when the <see cref="IProgram.Run"/> is called and <see cref="IProgram.Run"/> has not been run before.
    /// </summary>
    public class SetupNotRanException : Exception
    {
        public SetupNotRanException()
        {
        }
    }
}
