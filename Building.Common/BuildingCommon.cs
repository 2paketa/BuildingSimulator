using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Building;

namespace Building.Common
{
    public static class BuildingCommon
    {
        public static int GetValidInt(this string e)
        {
            int number = 0;
            var regexItem = new Regex(@"^\d+$");
            while (number == 0) 
            {
                if (regexItem.IsMatch(e)) { number = int.Parse(e); }
                else
                {
                    Console.WriteLine("Please enter an integer");
                    e = Console.ReadLine();
                }
            }
            return number;
        }


        public static int UserInput(int comparable, string error)
        {
            int input = Console.ReadLine().GetValidInt();

            while (input > comparable || input == 0)
            {
                Console.WriteLine(error + comparable + " : ");
                input = Console.ReadLine()
                    .GetValidInt();
            }
            return input;
        }

        public static int UserInput(int comparable, string error, Func<int, bool> condition)
        {
            int input = Console.ReadLine().GetValidInt();

            while (condition(input))
            {
                Console.WriteLine(error);
                input = Console.ReadLine()
                               .GetValidInt();
            }
            return input;
        }

    }
}
