using System;
using System.Collections.Generic;

namespace GenetickyAlgoritmus
{
    class Program
    {

        static void Main(string[] args)
        {
            // Města mezi kterými počítám vzdálenosti

            Algorithm algoritmus;
            Invidual result;

            // Objekt s objednavkami

            // Pole jedinců, kteří tvoří objednávku (10 variant objednávek)
            Orders[] vysledek = new Orders[10];

            // Zde je seznam všech variant objednávky
            List<string> orderList = new List<string>();

            // Seznam názvů měst
            string cities = "!#$%&()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~";

            // Vytvořím 10 skupin objednávek - 10 variant, kudy mohou vést cesty
            for (int i = 0; i < vysledek.Length; i++) vysledek[i] = new Orders(cities);

            // Vytvořím jeden dlouhý seznam cest ze všech objednávek (Každá objednávka má několik cest)
            for (int i = 0; i < vysledek.Length; i++) orderList.AddRange(vysledek[i].getOrdersList());

            // Ukážu všechny Jedince (každou skupinu objednávek)
            for (int i = 0; i < vysledek.Length; i++)
            {
                Console.WriteLine("Jedinec cislo "+i+":");
                vysledek[i].showMatrix();
            }
            Console.ReadLine();

            int counter = 0;

            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].Length > 1)
                {
                    algoritmus = new Algorithm(orderList[i], orderList[i].Length);
                    result = algoritmus.start();
                    Console.WriteLine(counter + " Nejlepší jedinec: " + result.getSequence() + "\tVzdálenost:" + result.getDistance());
                    counter++;
                }
                else
                {
                    Console.WriteLine(counter + " Nejlepší jedinec: " + orderList[i]);
                    counter++;
                }

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
