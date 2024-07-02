using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Globals;

namespace Order_Generator
{
    public class OrderGeneratorMain
    {
        private static bool status = true;
        private static string directory = @"C:\Users\comma\Documents\Orders";
        private static Timer timer;

        static void Main(string[] args)
        {
            timer = new Timer(Start, null, 0, 5000);
            Console.WriteLine("Program running, press any key to stop");
            Console.ReadKey();
        }


        private static void Start(object timerCallback)
        {
            Globals.Order order = new Globals.Order { Status = status };
            Order.Serialize(order, directory);
            status = !status;
        }
    }
}
