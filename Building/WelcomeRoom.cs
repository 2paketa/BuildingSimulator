using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class WelcomeRoom
    {
        public List<Visitor> Visitors = new List<Visitor>();
        public int CurrentCapacity
        {
            get
            {
                return Visitors.Count;
            }
        }

        public void Enter(Visitor visitor)
        {
            Visitors.Add(visitor);
        }


    }
}
