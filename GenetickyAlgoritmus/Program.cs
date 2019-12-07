using System;

namespace GenetickyAlgoritmus
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Invidual result = Algorithm.start();
            Console.ReadLine();

            Console.WriteLine("Nejlepší jedinec: " + result.getSequence() + "\tVzdálenost:" + result.getDistance());

            Console.ReadLine();

        }
    }
}
