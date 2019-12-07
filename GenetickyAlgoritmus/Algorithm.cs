using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    class Algorithm
    {
        public static int POPULATION_SIZE = 10;

        public static int GENERATION_COUNT = 50;

        public static int P_CROSSOVER = 80;

        public static int P_MUTATION = 20;

        public static int LENGTH = 5;

        public static int CROSSIN_POINT = 2;

        public static Cities cities = new Cities();

        public static Invidual start()

        {
            // Objekt s městy a maticí vzdáleností
           

            // Objekt s populací
            Population population = new Population();

            for (int i = 0; i < Algorithm.GENERATION_COUNT; i++)
            {
                Console.WriteLine("Generation = " + i);
                population.showMe();

                population.improve();

            }

            return population.GetBest();
            
        }

    }
}
