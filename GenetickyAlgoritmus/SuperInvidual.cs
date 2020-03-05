using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class SuperInvidual
    {
        public static int ORDER_CAPACITY = 64;

        public static int CAR_COST = 500;

        public static int KM_COST = 10;

        public static int OVERLOAD_PENALTY = 100;

        public static int CROSSIN_POINT = InputOutput.getActiveOrdersCount() / 2;

        public int driverCount = 1;

        public int[] routes = new int[InputOutput.getActiveOrdersCount()];

        public List<Invidual> finalRoutes = new List<Invidual>();

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

            this.driverCount = driver;

            startAlgorithm();

        }

        /// <summary>
        /// Vytvorim noveho jedince z 2 super jedincu (krizeni)
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        public SuperInvidual(SuperInvidual s1, SuperInvidual s2)
        {
            var rnd = new Random();
            int a = rnd.Next(0, 2);

            if (a == 0)
            {
                for (int i = 0; i < CROSSIN_POINT; i++) this.routes[i] = s1.routes[i];
                for (int i = CROSSIN_POINT; i < routes.Length; i++) this.routes[i] = s2.routes[i];
            }
            else
            {
                for (int i = 0; i < CROSSIN_POINT; i++) this.routes[i] = s2.routes[i];
                for (int i = CROSSIN_POINT; i < routes.Length; i++) this.routes[i] = s1.routes[i];
            }

            this.driverCount = 0;
            for (int i = 0; i < routes.Length; i++) if (routes[i] > this.driverCount) this.driverCount = routes[i];

            startAlgorithm();

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

        /// <summary>
        /// Vrati hodnotu prelozeni pro celeho super jedince
        /// Vrati 20 => 20 palet se nevleze
        /// </summary>
        /// <returns>Int Duplicity</returns>
        public int checkOverload()
        {

            int[] routesTmp = routes;
            int max = 0;
            int value = 0;
            int score = 0;

            for (int i = 1; i < routes.Length; i++) if (max < routes[i]) max = routes[i];

            for (int i = 1; i < max + 1; i++)
            {
                for (int j = 0; j < routes.Length; j++)
                {
                    if (routes[j] == i) value = value + Convert.ToInt32(Controller.activeOrdersTable.Rows[j]["Count"].ToString());

                }
                if (value > ORDER_CAPACITY) score = score + value - ORDER_CAPACITY;
                value = 0;
                    
            }

            return score;

        }

        /// <summary>
        /// Vrati hodnotu prelozeni pro 1 ridice
        /// Int i = cislo ridice
        /// Return 20 => 20 palet se nevleze
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int checkOverload(int i)
        {
            int[] routesTmp = routes;
            int value = 0;
            int score = 0;

            for (int j = 0; j < routes.Length; j++) if (routes[j] == i) value = value + Convert.ToInt32(Controller.activeOrdersTable.Rows[j]["Count"].ToString());

            if (value > ORDER_CAPACITY) score = score + value - ORDER_CAPACITY;

            return score;

        }
        
        /// <summary>
        /// Vrati sekvenci mest zapsanych jako ID pro ridice cislo "driver"
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public string[] getSequence(int driver)
        {
            int cityCount = 0;
            for (int i = 0; i < this.routes.Length; i++) if (this.routes[i] == driver) cityCount++;

            string[] cities = new string[cityCount];
            int j = 0;

            for (int i = 0; i < this.routes.Length; i++)
            {
                if (this.routes[i] == driver)
                {
                    cities[j] = Cities.getId(Controller.activeOrdersTable.Rows[i][1].ToString());
                    j++;
                }
            }

            return cities;
        }

        /// <summary>
        /// Vrati pocet palet, ktere ridic cislo "driver" veze
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public int getDriverPallets(int driver)
        {
            int count = 0;
            for (int i = 0; i < this.routes.Length; i++) if (this.routes[i] == driver) count++;

            return count;
        }

        /// <summary>
        /// Pustim algoritmus pro kazdeho ridice
        /// </summary>
        public void startAlgorithm()
        {
            finalRoutes.Clear();
            for (int i = 1; i <this.driverCount +1; i++)
            {
                Algorithm algoritmus = new Algorithm(getSequence(i));
                finalRoutes.Add(algoritmus.start()); 
            }
        }

        /// <summary>
        /// Ukaze vysledne cesty po vypoctu algoritmu
        /// </summary>
        public void showMeRoutes()
        {
            foreach (Invidual invidual in finalRoutes) invidual.showMeFull();

        }

        /// <summary>
        /// Vrati fitness celeho super jedince
        /// To znamena soucet vzdalenosti km + cena za kazde auto
        /// </summary>
        /// <returns></returns>
        public int getFitness()
        {
            int fitness = 0;
            foreach (Invidual invidual in finalRoutes) fitness = fitness + Convert.ToInt32(invidual.getDistance()) * KM_COST;

            fitness = fitness + checkOverload() * OVERLOAD_PENALTY;

            fitness = fitness + finalRoutes.Count * CAR_COST;

            return fitness;
        }

        /// <summary>
        /// Vrati pocet ridicu jako INT
        /// </summary>
        /// <returns></returns>
        public int getDriverCount()
        {
            return driverCount;
        }

    }
}
