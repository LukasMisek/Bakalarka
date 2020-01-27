using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Orders
    {
        public static int ORDER_CAPACITY = 64;

        // Finální seznam objednávek. Formát = "Město1-Město2-Město3:hodnota nákladu"
        private List<string> ordersList = new List<string>();

        // Úspornější verze seznamu objednávek. Formát = "Id1-Id2-Id3:hodnota nákladu"
        private List<string> ordersListIds = new List<string>();

        // Seznam jedinců. Tohle je výsledek. Každý jedinec je sekvence měst.
        private List<Invidual> sortedOrder = new List<Invidual>();

        // Celková cena za objednávku
        private double cost = 0;

        // Cena za kilometr
        public static int DISTANCE_COST = 10;

        // Cena za řidiče
        public static int CAR_COST = 10000;

        /// <summary>
        /// Konstruktor, zavolám generování objednávek
        /// </summary>
        public Orders()
        {
            generateRandomOrders();
        }

        /// <summary>
        /// Vygeneruji náhodné objednávky
        /// </summary>
        public void generateRandomOrders()
        {
            List<string> remainingCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);

            var rnd = new Random();
            int a = rnd.Next(0, remainingCities.Count);
            string city = remainingCities[a];
            int cityCount = remainingCities.Count-1;

            string newOrder = remainingCities[a];
            string newOrderId = Cities.getId(remainingCities[a]);
            remainingCities.RemoveAt(a);
            int newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
            for (int i = 0; i < cityCount; i++)
            {
                a = rnd.Next(0, remainingCities.Count);
                if (newOrderValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString()) < ORDER_CAPACITY)
                {
                    newOrder = newOrder + "-" + remainingCities[a];
                    newOrderId = newOrderId + "-" + Cities.getId(remainingCities[a]);
                    remainingCities.RemoveAt(a);
                    newOrderValue = newOrderValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
                }
                else
                {
                    newOrder = newOrder + ":" + newOrderValue;
                    newOrderId = newOrderId + ":" + newOrderValue;
                    this.ordersList.Add(newOrder);
                    this.ordersListIds.Add(newOrderId);
                    newOrder = remainingCities[a];
                    newOrderId = Cities.getId(remainingCities[a]);
                    remainingCities.RemoveAt(a);
                    newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
                }
            }

            if (newOrder.Length > 0)
            {
                newOrder = newOrder + ":" + newOrderValue;
                newOrderId = newOrderId + ":" + newOrderValue;
                this.ordersList.Add(newOrder);
                this.ordersListIds.Add(newOrderId);
            }

        }

        /// <summary>
        /// Zobrazí všechny sekvence (vsechny cesty)
        /// </summary>
        public void showMe()
        {
            foreach(string sequence in ordersList)
            {
                string[] s = sequence.Split(':');
                Console.WriteLine("{0,-25}{1,0}",("Hodnota nákladu: " + s[1]), s[0]);
            }
            
        }

        /// <summary>
        /// Zobrazí jednu sekvenci (jednu cestu)
        /// </summary>
        /// <param name="i"></param>
        public void showMe(int i)
        {
            Console.WriteLine(ordersList[i]);
        }

        /// <summary>
        /// Zobrazi všechny sekvence jako ID měst (všechny cesty jako ID)
        /// </summary>
        public void showMeId()
        {
            foreach (string s in ordersListIds) Console.WriteLine(s);
        }

        /// <summary>
        /// Vrátí jednu sekvenci jako String[]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string[] getOrderList(int index)
        {
            string[] tmpArray = this.ordersList[index].Split(':');

            return tmpArray[0].Split('-');
        }

        /// <summary>
        /// Vrátí jednu sekvenci ID jako String[]
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string[] getOrderListId(int index)
        {
            string[] tmpArray = this.ordersListIds[index].Split(':');

            return tmpArray[0].Split('-');
        }

        /// <summary>
        /// Vrátí setříděnou sekvenci jako List
        /// </summary>
        /// <returns></returns>
        public List<Invidual> getOrderListSorted()
        {
            return sortedOrder;
        }

        /// <summary>
        /// Spustí algoritmus pro konkrétní cestu
        /// </summary>
        /// <param name="x"></param>
        public void calculateRoutes(int x)
        {
            Algorithm algoritmus;
            Invidual result;

            algoritmus = new Algorithm(getOrderList(x));
            result = algoritmus.start();
            sortedOrder.Add(result);

        }

        /// <summary>
        /// Spustí algoritmus pro všechny cesty
        /// </summary>
        public void calculateRoutes()
        {
            Algorithm algoritmus;
            Invidual result;

            for (int i = 0; i < this.ordersListIds.Count; i++)
            {
                algoritmus = new Algorithm(getOrderListId(i));
                result = algoritmus.start();
                sortedOrder.Add(result);

            }
        }

        /// <summary>
        /// Přeloží sekvence cest z ID na Města
        /// </summary>
        public void translateRoutes()
        {

            foreach (Invidual invidual in sortedOrder) invidual.translateSequence();

        }

        /// <summary>
        /// Vypočítá cenu ke každé cestě
        /// </summary>
        public void calculatePrice()
        {
            this.cost = 0;
            foreach (Invidual invidual in sortedOrder) this.cost = this.cost + (invidual.getDistanceString() * DISTANCE_COST);

            this.cost = this.cost + (sortedOrder.Count * CAR_COST);
        }

        /// <summary>
        /// Vrátí cenu cesty
        /// </summary>
        /// <returns></returns>
        public double getCost()
        {
            return this.cost;
        }

        /// <summary>
        /// Vrátí počet cest
        /// </summary>
        /// <returns></returns>
        public int getOrdersCount()
        {
            return this.ordersList.Count;
        }


    }
}
