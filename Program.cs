using System;
using DesignPatterns.Solid;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // OpenClosurePrinciple.Test();
            DependencyInversion.Test();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

        }
    }
}
