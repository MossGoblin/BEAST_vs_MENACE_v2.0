using BvM.Controllers;
using System;

namespace BvM
{
    class StartUp
    {
        static void Main(string[] args)
        {
            InterfaceManager im = new InterfaceManager();
            SwitchBoard sb = new SwitchBoard(im);
            try
            {
                //string writeResult = sb.IManager.Record(false, temp, "throwing the Interface Manager to the SwitchBoard");
                string writeResult = sb.IManager.Record(false, FileNameList.temp, "Check InterfaceManager - setting up files");
                Console.WriteLine(writeResult);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops!: {ex.Message}");
            }
        }
    }
}
