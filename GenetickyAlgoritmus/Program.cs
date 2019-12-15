using System;
using System.Collections.Generic;

namespace GenetickyAlgoritmus
{
    class Program
    {

        static void Main(string[] args)
        {
            // Města mezi kterými počítám vzdálenosti

            Algorithm algoritmus = new Algorithm();
            Invidual result;

            // Objekt s objednavkami

            // Pole jedinců, kteří tvoří objednávku (10 variant objednávek)
            Orders[] vysledek = new Orders[10];

            // Zde je seznam všech variant objednávky
            List<string> orderList = new List<string>();

            // Vytvořím 10 skupin objednávek - 10 variant, kudy mohou vést cesty
            for (int i = 0; i < vysledek.Length; i++)
            {
                vysledek[i] = new Orders();
            }

            // Vytvořím jeden dlouhý seznam cest ze všech objednávek (Každá objednávka má několik cest)
            for (int i = 0; i < vysledek.Length; i++)
            {
                orderList.AddRange(vysledek[i].getOrdersList());
            }

            // Ukážu všechny Jedince (každou skupinu objednávek)
            for (int i = 0; i < vysledek.Length; i++)
            {
                Console.WriteLine("Jedinec cislo "+i+":");
                vysledek[i].showMatrix();
            }

            Console.ReadLine();

            for (int i = 0; i < orderList.Count; i++)
            {
                algoritmus = new Algorithm(orderList[i], orderList[i].Length);
                result = algoritmus.start();
                Console.WriteLine("Nejlepší jedinec: " + result.getSequence() + "\tVzdálenost:" + result.getDistance());
            }

            Console.ReadLine();

            /*
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

        }
    }
}
