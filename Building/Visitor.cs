using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Visitor
    {
        public readonly int FloorToMove;
        public readonly int OfficeNumber;
        public bool Served = false;

        public int PriorityNumber { get; set; }

        public Visitor(int floorToMove, int officeNumber)
        {
            this.FloorToMove = floorToMove;
            this.OfficeNumber = officeNumber;
        }


    }
}
