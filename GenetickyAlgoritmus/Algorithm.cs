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
        public static int GENERATION_COUNT = 1000;

        // Šance ke křížení (80 = 80%)
        public static int P_CROSSOVER = 80;

        // Šance k mutaci (10 = 10%)
        public static int P_MUTATION = 10;

        // Délka genů (počet alel/genů/měst)
        public static int LENGTH = 20;

        // Bod křížení (10 = na místě 10. jedince provedu křížení)
        public static int CROSSIN_POINT = 10;

        // Císlová vzdálenost mezi všemi městy (jak jedna vzdálenost je menší než cílová, tak algoritmus končí)
        public static int GOAL_DISTANCE = 10;

        // Města mezi kterými počítám vzdálenosti
        public static Cities cities = new Cities();

        public static Invidual start()

        {
            // Objekt s městy a maticí vzdáleností
           

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

        private static bool algorithmEnd(int bestDistance, int goalDistance)
        {
            if (bestDistance <= goalDistance) return true;
            else return false;
        }

    }
}
