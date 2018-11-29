using BvM.Controllers;
using System;

namespace BvM
{
    class StartUp
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            try
            {
                string writeResult = ui.Record(false, "temp", "testing timestamp and Record method overwriting");
                Console.WriteLine(writeResult);
                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Oops!");
            }
        }
    }
}
