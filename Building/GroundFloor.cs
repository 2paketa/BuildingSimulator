using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Building.Common;

namespace Building
{
    public class GroundFloor
    {
        public int PriorityNumber = 0;
        internal WelcomeRoom welcomeRoom = new WelcomeRoom();
        public bool IsThereSpace { get { return CurrentCapacity < MaxCapacity; } }
        public int CurrentCapacity
        {
            get
            {
                return welcomeRoom.CurrentCapacity;
            }
        }
        readonly int MaxCapacity;

        public GroundFloor(int MaxCapacity)
        {
            this.MaxCapacity = MaxCapacity;
        }

        public void Enter(Visitor visitor)
        {
            wait(visitor);
            welcomeRoom.Enter(visitor);
        }

        private void wait(Visitor visitor)
        {
            this.PriorityNumber++;
            visitor.PriorityNumber = this.PriorityNumber;
            Console.WriteLine("Waiting for the Lift " + visitor.PriorityNumber);
        }

        public Visitor Exit()
        {
            Visitor VisitorLeaving =  welcomeRoom.Visitors.OrderBy(x => x.PriorityNumber).First();
            welcomeRoom.Visitors.Remove(VisitorLeaving);
            return VisitorLeaving;
        }

    }
}
