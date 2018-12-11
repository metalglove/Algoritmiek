using System;
using System.Collections.Generic;
using System.Text;

namespace Algoritmiek.Containervervoer
{
    public class DryContainer : Container, IContainer
    {
        public DryContainer(Freight freight) : base(freight)
        {
        }
    }
}
