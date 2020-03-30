using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Algorithm
    {
        // Velikost populace (počet jedinců)
        public static int POPULATION_SIZE = 10;

        // Počet generací (Počet cyklů)
        public static int GENERATION_COUNT = 20;

        // Šance ke křížení (80 = 80%)
        public static int P_CROSSOVER = 80;

        // Šance k mutaci (10 = 10%)
        public static int P_MUTATION = 10;

        // Délka genů (počet alel/genů/měst)
        public static int LENGTH = 80;

        // Bod křížení (10 = na místě 10. jedince provedu křížení)
        public static int CROSSIN_POINT = 5;

        // Císlová vzdálenost mezi všemi městy (jak jedna vzdálenost je menší než cílová, tak algoritmus končí)
        public static double GOAL_DISTANCE = 0;

        // Cena za kilometr
        public static int DISTANCE_COST = 10;

        // Cena za řidiče
        public static int CAR_COST = 10000;

        // Města mezi kterými počítám vzdálenosti
        public static string[] cities;

        // Znaky, které reprezentují města
        public static string[] citiesFull;

        public Algorithm(string[] s)
        {
            citiesFull = Functions.removeDuplicityStringArray(s);
            LENGTH = citiesFull.Length;
            CROSSIN_POINT = citiesFull.Length / 2;
            cities = citiesFull;

        }
        /*
        public void generateCityNames(string[] s)
        {
            cities = new string[citiesFull.Length];
            for (int i = 0; i < s.Length; i++) cities[i] = Convert.ToChar(32 + i) + "";
        }
        */

        public Invidual start()

        {
            // Objekt s městy a maticí vzdáleností

            Population population = new Population();
            // Objekt s populací    

            if (cities.Length == 1)
            {
                return population.GetBest();
            }


            for (int i = 0; i < Algorithm.GENERATION_COUNT; i++)
            {

                population.improve();

                /*
                Console.WriteLine("Generation = " + i);
                population.showMe();
                */

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
        private static bool algorithmEnd(double bestDistance, double goalDistance)
        {
            if (bestDistance <= goalDistance) return true;
            else return false;
        }

        public void setLength(int a)
        {
            LENGTH = a;
        }
        public void setCrossingPoint(int a)
        {
            CROSSIN_POINT = a;
        }

        public void setPopulationSize(int a)
        {
            POPULATION_SIZE = a;
        }
        public void setGenerationCount(int a)
        {
            GENERATION_COUNT = a;
        }

        public void setCrossover(int a)
        {
            P_CROSSOVER = a;
        }
        public void setMutation(int a)
        {
            P_MUTATION = a;
        }

        public string[] getCityNames()
        {
            return cities;
        }


    }
}
