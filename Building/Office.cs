using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    class Office
    {
        readonly List<Visitor> visitors = new List<Visitor>();

        public readonly int MaxCapacity;
        public bool IsThereSpace { get { return (CurrentCapacity < MaxCapacity); } }
        public int Number { get; private set; }
        public int DisplayNumber => Number + 1;
        public int CurrentCapacity
        {
            get
            {
                return visitors.Count;
            }
        }
        Random random = new Random();
        

        public Office(int MaxCapacity, int number)
        {
            this.MaxCapacity = MaxCapacity;
            this.Number = number;
            Console.WriteLine("Office number " + (number + 1) + " has been created");
        }

        public void Enter(Visitor visitor)
        {
            visitors.Add(visitor);
        }

        public Visitor Exit()
        {
            Visitor servedVisitor = visitors.ElementAt(random.Next(visitors.Count));
            servedVisitor.Served = true;
            visitors.Remove(servedVisitor);
            return servedVisitor;
        }
    }
}
