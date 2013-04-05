using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CW.Sample.WCF.WcfExampleService.Host;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WcfExampleServiceHost("localhost:10000");
            host.Start();

            Console.WriteLine("Server Started successfully!!");
            Console.WriteLine("");
            Console.WriteLine("Press Enter Key to Terminate the host!!");
            Console.Read();    

        }
    }
}
