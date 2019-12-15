using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenetickyAlgoritmus
{
    class Algorithm
    {
        // Velikost populace (počet jedinců)
        public static int POPULATION_SIZE = 10;

        // Počet generací (Počet cyklů)
        public static int GENERATION_COUNT = 5;

        // Šance ke křížení (80 = 80%)
        public static int P_CROSSOVER = 80;

        // Šance k mutaci (10 = 10%)
        public static int P_MUTATION = 10;

        // Délka genů (počet alel/genů/měst)
        public static int LENGTH = 80;

        // Bod křížení (10 = na místě 10. jedince provedu křížení)
        public static int CROSSIN_POINT = 5;

        // Císlová vzdálenost mezi všemi městy (jak jedna vzdálenost je menší než cílová, tak algoritmus končí)
        public static int GOAL_DISTANCE = 0;

        // Města mezi kterými počítám vzdálenosti
        public static Cities cities;

        public static int ORDER_CAPACITY = 64;

        public Algorithm()
        {
            cities = new Cities();
        }

        public Algorithm(string sequence, int length)
        {
            LENGTH = sequence.Length;
            CROSSIN_POINT = sequence.Length / 2;
            cities = new Cities(sequence);
        }


        public Invidual start()

        {
            // Objekt s městy a maticí vzdáleností
            /*
            cities.showMatrix();
            Console.WriteLine("mesta jsem udelal uspesne");
            Console.ReadLine();
            */

            // Objekt s populací            
            Population population = new Population();

            /*
            Console.WriteLine("populaci jsem udelal uspesne");
            population.showMe();
            Console.ReadLine();
            */

            for (int i = 0; i < Algorithm.GENERATION_COUNT; i++)
            {
                population.improve();

                /*
                Console.WriteLine("Generation = " + i);
                population.showMe();
                */

                if (algorithmEnd(population.GetBest().getDistance(), Algorithm.GOAL_DISTANCE)) break;

            }
            
            return population.GetBest();
            
        }

        /// <summary>
        /// Ukončovací funkce
        /// Přebírá fitness nejlepšího jedince (vzdálenost) a porovná ji s ALGORITHM.GOAL_DISTANCE
        /// Pokud je nejlepší dosažená vzdálenost menší než ALGORITHM.GOAL_DISTANCE, tak vrátí true
        /// </summary>
        /// <param name="bestDistance"></param>
        /// <param name="goalDistance"></param>
        /// <returns></returns>
        private static bool algorithmEnd(int bestDistance, int goalDistance)
        {
            if (bestDistance <= goalDistance) return true;
            else return false;
        }

    }
}
