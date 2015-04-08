using System;
using Microsoft.Owin.Hosting;
using Owin;

namespace ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
             int portNumber;

            if (args.Length < 1)
            {
                Console.WriteLine("No arguments specified therefore no port number specified");
                return;
            }

            if (!int.TryParse(args[0], out portNumber))
            {
                Console.WriteLine("Thats not a valid port number {0}", args[0]);
                return;
            }

            var url = string.Format("http://+:{0}", portNumber);

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running Language Detection Service on port {0}, press enter to exit", portNumber);
                Console.ReadLine();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}
