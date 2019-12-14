using System;
using System.Collections.Generic;

namespace GenetickyAlgoritmus
{
    class Program
    {

        static void Main(string[] args)
        {
            // Města mezi kterými počítám vzdálenosti



            Invidual result = Algorithm.start();

            Console.WriteLine("Nejlepší jedinec: " + result.getSequence() + "\tVzdálenost:" + result.getDistance());

            // Objekt s objednavkami

            /*
        Orders[] vysledek = new Orders[10];

            List<string> orderList = new List<string>();

            // tvorba prikazu
            for (int i = 0; i < vysledek.Length; i++)
            {
                vysledek[i] = new Orders();
            }

            for (int i = 0; i <vysledek.Length; i++)
            {
                orderList.AddRange(vysledek[i].getOrdersList());
            }

            foreach (string s in orderList)
            {
                Console.WriteLine(s.Trim());
                Algorithm.start(s.Trim());
                Console.ReadLine();
            }

    */

            Console.ReadLine();

        }
    }
}
