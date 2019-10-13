using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Building.Common;

namespace Building
{
    public class ElevatedFloor : ILocation

    {
        public int Number { get; private set; }
        public int DisplayNumber => Number + 1;
        internal WelcomeRoom welcomeRoom = new WelcomeRoom();
        readonly Office[] offices = new Office[10];
        Random random = new Random();
        readonly int MaxCapacity;
        public bool IsThereSpace { get { return CurrentCapacity < MaxCapacity; } }
        public int OfficesCurrentCapacity //returns the current capacity of all offices
        {
            get
            {
                int officesCurrentCapacity = 0;
                for (int i = 0; i < offices.Length; i++)
                    officesCurrentCapacity += offices[i].CurrentCapacity;
                return officesCurrentCapacity;
            }
        }
        public int CurrentCapacity //returns the current capacity of all offices and entrance
        {
            get
            {
                return OfficesCurrentCapacity + welcomeRoom.CurrentCapacity;
            }
        }

        public ElevatedFloor(int MaxCapacity, int number, int nOffice)
        {
            this.Number = number;
            this.MaxCapacity = MaxCapacity;
            Console.WriteLine("A floor has been created with number " + DisplayNumber);
            constructOffices(nOffice);
        }
        
        private void constructOffices(int nOffice)
        {
            int[] officesMaxCapacity = generateOfficesCapacity(nOffice);
            int i = 0;
            while (i < 10)
            {
                offices[i] = new Office(officesMaxCapacity[i], i);
                i++;
            }
            Console.ReadKey();
        }

        private int[] generateOfficesCapacity(int nOffice)
        {
            random = new Random();
            var getCapacities = new int[10];
            while (getCapacities.Min() == 0)
            {
                int maxCap = nOffice;
                for (int i = 0; i < getCapacities.Length; i++)
                {
                    getCapacities[i] = random.Next(maxCap);
                    maxCap -= getCapacities[i];
                }
            }
            return getCapacities;
        }

        public void Enter(Visitor visitor)
        {
            int i = visitor.OfficeNumber;
            if (offices[i].IsThereSpace)
            {
                offices[i].Enter(visitor);
                Console.WriteLine("Entering floor number " + DisplayNumber + " (office " + (i + 1) + ")" + ", priority number " + visitor.PriorityNumber);
            }
            else
            {
                welcomeRoom.Enter(visitor);
                Console.WriteLine("Please wait outside for entrance in office number " + (offices[i].DisplayNumber) + ", priority number " + visitor.PriorityNumber);
            }

        }

        private void welcomeRoomVisitorsEnter() 
        {
            var entranceToOfficeVisitors = new List<Visitor>(welcomeRoom.Visitors);
            foreach (Visitor visitor in entranceToOfficeVisitors.OrderBy(x => x.PriorityNumber))
                for (int i = 0; i < offices.Length; i++)
                    if (visitor.OfficeNumber == offices[i].Number)
                    {
                        if (offices[i].IsThereSpace)
                        {
                            offices[i].Enter(visitor);
                            welcomeRoom.Visitors.Remove(visitor);
                            Console.WriteLine("Entering office " + (offices[i].DisplayNumber) + ", priority number " + visitor.PriorityNumber);
                        }
                        else
                            Console.WriteLine("Please wait outside for entrance in office number " + (offices[i].Number + 1) + ", priority number " + visitor.PriorityNumber);
                    }
        }


        public Visitor Exit()
        {
            int i = random.Next(0, offices.Length);
            while (offices[i].CurrentCapacity == 0)
                i = random.Next(0, offices.Length);
            Visitor visitor = offices[i].Exit();
            welcomeRoomVisitorsEnter();
            return visitor;
        }

    }
}
