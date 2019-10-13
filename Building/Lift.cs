using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public class Lift: ILocation
    {
        public int MaxCapacity;
        public bool IsThereSpace { get { return CurrentCapacity < MaxCapacity; } }
        public int CurrentCapacity
        {
            get
            {
                return Visitors.Count;
            }
        }
        public List<Visitor> Visitors = new List<Visitor>();

        public Lift(int maxCapacity)
        {
            Console.WriteLine("A lift has been created!");
            this.MaxCapacity = maxCapacity;
        }

        public void Enter(Visitor visitor)
        {            
            if (IsThereSpace)
            {
                Visitors.Add(visitor);
                Console.Write("Visitor in the lift " + visitor.PriorityNumber + Environment.NewLine);
            }
            else
                Console.Write("You are not allowed to enter " + visitor.PriorityNumber + Environment.NewLine);
        }

        public Visitor Exit()
        {
            var VisitorLeaving = Visitors.OrderBy(x => x.PriorityNumber).First();
            Visitors.Remove(VisitorLeaving);
            return VisitorLeaving;
        }
    }
}
