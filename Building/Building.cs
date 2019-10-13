using Building.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Building
{

    public class Building
    {
        readonly List<Visitor> buildingVisitors = new List<Visitor>();
        public static int MaxCapacity;
        GroundFloor groundFloor;
        Lift lift;
        ElevatedFloor[] elevatedFloors;
        public bool IsThereSpace { get { return CurrentCapacity < MaxCapacity;  } }

        public int CurrentCapacity
        {
            get
            {
                int elevatedFloorsCurrentCapacity = 0;
                for (int i = 0; i < elevatedFloors.Length; i++)
                    elevatedFloorsCurrentCapacity += elevatedFloors[i].CurrentCapacity;
                return groundFloor.CurrentCapacity + lift.CurrentCapacity + elevatedFloorsCurrentCapacity + buildingVisitors.Count;
            }
        }

        public int VisitorsServed { get; private set; }

        public Building()
        {
            Console.WriteLine("Enter building capacity: ");
            MaxCapacity = BuildingCommon.UserInput(33, "Building capacity should be more than 33", (x) => x <
            30);
        }

        public void Main()
        {
            Random random = new Random();
            //user input

            //Groundfloor capacity
            Console.WriteLine("Enter Groundfloor capacity: ");
            int nGroundFloor = BuildingCommon.UserInput(MaxCapacity / 2, "Groundfloor capacity should be between 1 and ");
            //Elevatedloor capacity
            Console.WriteLine("Enter floor capacity: ");
            int nFloor = BuildingCommon.UserInput(MaxCapacity / 3, "floor capacity should be between 1 and ");
            //Offices capacity
            Console.WriteLine("Enter office capacity: ");
            int nOffice = BuildingCommon.UserInput(nFloor, "Office capacity should be less than " + nFloor + ": ", (x) => x >= nFloor);
            //Lift capacity
            Console.WriteLine("Enter lift capacity: ");
            int nLift = BuildingCommon.UserInput(nOffice, "lift capacity should be more than " + nOffice + ": ", (x) => x < nOffice);


            //Lift cycles
            Console.WriteLine("Enter lift operating cycles: ");
            int lCycles = Console.ReadLine().GetValidInt();
            //Number of visitors
            Console.WriteLine("Enter number of visitors: ");
            int kVisitors = Console.ReadLine().GetValidInt();

            //initialize objects

            groundFloor = new GroundFloor(nGroundFloor);
            elevatedFloors = new ElevatedFloor[4];
            for (int i = 0; i < elevatedFloors.Length; i++)
                elevatedFloors[i] = new ElevatedFloor(nFloor, i, nOffice);

            lift = new Lift(nLift);
            List<Visitor> newVisitors = new List<Visitor>();
            while (kVisitors > 0)
            {
                newVisitors.Add(new Visitor(random.Next(elevatedFloors.Length), random.Next(0, 9)));
                kVisitors--;
            }

            // Visitors attempt to enter
            AttemptEntry(newVisitors);

            //Lift operating cycles
            while (lCycles > 0)
            {
                OperateLift();
                Console.WriteLine("Operating cycle is done");
                RemainingBuildingVisitors();
                printStats();
                Console.ReadKey();
                lCycles--;
            }
        }

        /// <summary>
        /// Adds remaining building visitors to Groundfloor entrance
        /// </summary>
        public void RemainingBuildingVisitors()
        {
            for (int i = buildingVisitors.Count - 1; i >= 0; i--)
            {
                Enter(buildingVisitors[i]);
                buildingVisitors.RemoveAt(i);
            }

        }

        /// <summary>
        /// Loop that attempts entry of visitors
        /// </summary>
        /// <param name="visitors"></param>
        public void AttemptEntry(List<Visitor> visitors)
        {
            foreach (Visitor visitor in visitors)
                Enter(visitor);
        }
        /// <summary>
        /// Transfers visitors to the groundfloor or building accorindg to capacity
        /// </summary>
        /// <param name="visitor"></param>
        public void Enter(Visitor visitor)
        {
            if (groundFloor.IsThereSpace)
                groundFloor.Enter(visitor);
            else if (IsThereSpace)
                buildingVisitors.Add(visitor);
            else
                Console.WriteLine("Please come tomorrow");
        }

        public void Exit(Visitor visitor)
        {
            Console.WriteLine("I cannot believe I finished! :" + visitor.PriorityNumber );
        }

        /// <summary>
        /// Transfers visitors to floors
        /// </summary>
        private void Stop_up()
        {
            var visitorsEnteringFloor = new List<Visitor>(lift.Visitors);
            for (int i = 0; i < elevatedFloors.Length; i++)
                foreach (Visitor visitor in visitorsEnteringFloor.TakeWhile(x => elevatedFloors[i].IsThereSpace == true).Where(x => i == x.FloorToMove).OrderBy(x => x.PriorityNumber))
                {
                    elevatedFloors[i].Enter(visitor);
                    lift.Visitors.Remove(visitor);
                }
        }
        /// <summary>
        /// Transfers served visitors to Lift
        /// </summary>
        public void Stop_down()
        {
            for (int i = 3; i >= 0; i--)
                while (lift.IsThereSpace && elevatedFloors[i].OfficesCurrentCapacity > 0)
                    lift.Enter(elevatedFloors[i].Exit());
        }
        /// <summary>
        /// Empties elevator and removes visitor from building
        /// </summary>
        public void Empty_all()
        {
            List<Visitor> visitorsExitingBuilding = new List<Visitor>(lift.Visitors);
            foreach (var visitor in visitorsExitingBuilding.Where(x => x.Served))
            {
                VisitorsServed++;
                Exit(visitor);
                lift.Visitors.Remove(visitor);
            }
        }
        /// <summary>
        /// Operates lift
        /// </summary>
        public void OperateLift()
        {
            TransferVisitorsToLift();
            Stop_up();
            Stop_down();
            Empty_all();
        }
        /// <summary>
        /// Moves visitors to lift
        /// </summary>
        private void TransferVisitorsToLift()
        {
            for (var i = groundFloor.welcomeRoom.Visitors.Count - 1; i >= 0; i--)
                if (lift.IsThereSpace)
                    lift.Enter(groundFloor.Exit());
        }

        /// <summary>
        /// Prints capacity of each object except offices and welcomerooms
        /// </summary>
        private void printStats() 
        {
            Console.WriteLine("Visitors in the building = " + CurrentCapacity + ", Visitors in Groundfloor = " + groundFloor.CurrentCapacity + ", Visitors in the Lift = " + lift.CurrentCapacity);
            Console.WriteLine("Visitors served = " + VisitorsServed + ", Remaining visitors = " + buildingVisitors.Count);
            for (var i = 0; i < elevatedFloors.Length; i++)
            {
                Console.WriteLine("Visitors in floor number " + elevatedFloors[i].DisplayNumber + " = " + elevatedFloors[i].CurrentCapacity);
                Console.WriteLine("Visitors in floor Welcome room = " + elevatedFloors[i].welcomeRoom.CurrentCapacity);
            }
        }
    }
}
