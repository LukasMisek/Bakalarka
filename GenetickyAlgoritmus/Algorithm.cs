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
        public static int GENERATION_COUNT = 50;

        // Šance ke křížení (80 = 80%)
        public static int P_CROSSOVER = 80;

        // Šance k mutaci (10 = 10%)
        public static int P_MUTATION = 10;

        // Délka genů (počet alel/genů/měst)
        public static int LENGTH = 10;

        // Bod křížení (10 = na místě 10. jedince provedu křížení)
        public static int CROSSIN_POINT = 5;

        // Císlová vzdálenost mezi všemi městy (jak jedna vzdálenost je menší než cílová, tak algoritmus končí)
        public static int GOAL_DISTANCE = 10;

        // Města mezi kterými počítám vzdálenosti
        public static Cities cities = new Cities();

        public static int ORDER_CAPACITY = 32;

        public static Invidual start()

        {
            // Objekt s městy a maticí vzdáleností
            cities.showMatrix();

            // Objekt s populací            
            Population population = new Population();

            for (int i = 0; i < Algorithm.GENERATION_COUNT; i++)
            {
                population.improve();

                Console.WriteLine("Generation = " + i);
                population.showMe();

                if (algorithmEnd(population.GetBest().getDistance(), Algorithm.GOAL_DISTANCE)) break;

            }
            
            return population.GetBest();
            
        }

        public static void start(string s)
        {
            cities = new Cities(s);

            LENGTH = s.Length;
            CROSSIN_POINT = LENGTH / 2;

            Population population = new Population();

            for (int i = 0; i < Algorithm.GENERATION_COUNT; i++)
            {
                population.improve();

                Console.WriteLine("Generation = " + i);
                population.showMe();

            }
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
