using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Algorithm
    {
        // Velikost populace (počet jedinců)
        public int POPULATION_SIZE = 10;

        // Počet generací (Počet cyklů)
        public int GENERATION_COUNT = 5;

        // Šance ke křížení (80 = 80%)
        public int P_CROSSOVER = 80;

        // Šance k mutaci (10 = 10%)
        public int P_MUTATION = 10;

        // Délka genů (počet alel/genů/měst)
        public int LENGTH = 80;

        // Bod křížení (10 = na místě 10. jedince provedu křížení)
        public int CROSSIN_POINT = 5;

        // Císlová vzdálenost mezi všemi městy (jak jedna vzdálenost je menší než cílová, tak algoritmus končí)
        public double GOAL_DISTANCE = 0;

        // Města mezi kterými počítám vzdálenosti
        public string[] cities;

        /// <summary>
        /// Ukončovací funkce
        /// Přebírá fitness nejlepšího jedince (vzdálenost) a porovná ji s ALGORITHM.GOAL_DISTANCE
        /// Pokud je nejlepší dosažená vzdálenost menší než ALGORITHM.GOAL_DISTANCE, tak vrátí true
        /// </summary>
        /// <param name="bestDistance"></param>
        /// <param name="goalDistance"></param>
        /// <returns></returns>
        public static bool algorithmEnd(double bestDistance, double goalDistance)
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
