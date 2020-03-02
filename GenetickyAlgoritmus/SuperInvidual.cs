using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class SuperInvidual
    {
        public static int ORDER_CAPACITY = 32;

        public int[] routes;

        /// <summary>
        /// Vytvorim cestu nahodne s ohledem na maximalni kapacitu
        /// </summary>
        public SuperInvidual()
        {
            routes = new int[InputOutput.getActiveOrdersCount()];

            List<string> remainingCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);

            // Vybiram nahodne mesto
            var rnd = new Random();
            // Ukazuji na aktualni mesto
            int a = rnd.Next(0, remainingCities.Count);
            // Aktualni ridic
            int driver = 1;

            // aktualni hodnota nakladu
            int routeValue = 0;

            for (int i = 0; i < remainingCities.Count; i++)
            {
                while (remainingCities[a] == "X") a = rnd.Next(0, remainingCities.Count);

                if (routeValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString()) < ORDER_CAPACITY)
                {
                    routeValue = routeValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
                    routes[a] = driver;
                    remainingCities[a] = "X";
                }
                else
                {
                    driver = driver + 1;
                    routeValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
                    routes[a] = driver;
                    remainingCities[a] = "X";
                }
      
            }

        }

        /// <summary>
        /// Ukazu cestu pro kazdeho ridice ve formatu mesto-mesto-mesto:hodnota nakladu
        /// </summary>
        public void showMeFull()
        {
            int[] routesTmp = routes;
            int max = 0;
            int value = 0;
            
            for (int i = 1; i < routes.Length; i++) if (max < routes[i]) max = routes[i];

            string s_out = "";

            for (int i = 1; i < max+1; i++)
            {
                for (int j = 0; j < routes.Length; j++)
                {
                    if (routes[j] == i)
                    {
                        s_out = s_out + Controller.activeOrdersTable.Rows[j]["Obec"].ToString() + "-";
                        value = value + Convert.ToInt32(Controller.activeOrdersTable.Rows[j]["Count"].ToString());
                    }
                        
                }
                s_out = s_out.Substring(0, s_out.Length-1) + ":" + value + "\n";
                value = 0;
            }

            Console.WriteLine(s_out);
        }

        /// <summary>
        /// Ukazu cestu ve formatu mesto-mesto-mesto:hodnota nakladu
        /// pro jednoho ridice
        /// </summary>
        /// <param name="i"></param>
        public void showMeDriver(int i)
        {
            int[] routesTmp = routes;
            int value = 0;

            string s_out = "";

                for (int j = 0; j < routes.Length; j++)
                {
                    if (routes[j] == i)
                    {
                        s_out = s_out + Controller.activeOrdersTable.Rows[j]["Obec"].ToString() + "-";
                        value = value + Convert.ToInt32(Controller.activeOrdersTable.Rows[j]["Count"].ToString());
                    }

                }
                s_out = s_out.Substring(0, s_out.Length - 1) + ":" + value + "\n";

            Console.WriteLine(s_out);
        }


    }
}
