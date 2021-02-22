using Microsoft.Owin.Hosting;
using System;
using System.Linq;

namespace OwinHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8008/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine();
                Console.WriteLine("Owin-based superhero service running.");
                Console.ReadLine();
            }
        }
    }
}
