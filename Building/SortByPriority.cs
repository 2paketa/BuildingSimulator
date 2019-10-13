using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class SortByPriority: IComparer<Visitor>
    {
        public int Compare(Visitor x, Visitor y)
        {
            if (x.PriorityNumber < y.PriorityNumber)
                return -1;
            else 
                return 1;
        }
    }
}
