using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minage.Mines.MineThreads
{
    class MinageThreads
    {
        private static Random rand;
        public static void HelloWorld()
        {
            Console.WriteLine("HelloWorld");
        }

        public static int MinagePepitesRandom(string mineur)
        {
            rand = new Random(NameToInt(mineur) + (int)DateTime.Now.Ticks);
            return rand.Next(1, 5);
        }

        public static int NameToInt(string name)
        {
            int number = 0;
            foreach (char c in name)
            {
                number += (int)c;
            }
            return number;
        }


    }
}
