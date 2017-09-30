using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMicro
{
    class Program
    {
        static void Main(string[] args)
        {
            SMCIPMITool ipmi = new SMCIPMITool(AppDomain.CurrentDomain.BaseDirectory.Replace("\\\\", "\\").Replace("\\\\", "\\")+"SMCIPMITool\\SMCIPMITool.exe", "172.30.0.21", "ADMIN", "ADMIN");
            Console.WriteLine(ipmi.Connect());
            Console.WriteLine(ipmi.SetFanMode(SMCIPMITool.FanMode.StandartSpeed));
            Console.WriteLine(ipmi.SetPower(SMCIPMITool.Power.up));
            Console.WriteLine(ipmi.SetFanMode(SMCIPMITool.FanMode.FullSpead));
            Console.ReadKey();
        }
    }
}
