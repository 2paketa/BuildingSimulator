using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    interface ILocation
    {
        bool IsThereSpace { get; }

        void Enter(Visitor visitor);
        Visitor Exit();
    }
}
