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
        public static int CAR_COST = 2000;

        public Orders()
        {
            generateRandomOrders();
        }

        public void generateRandomOrders()
        {
            List<string> remainingCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);

            var rnd = new Random();
            int a = rnd.Next(0, remainingCities.Count);
            string city = remainingCities[a];

            string newOrder = remainingCities[a];
            string newOrderId = Cities.getId(remainingCities[a]);
            remainingCities.RemoveAt(a);
            int newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());

            for (int i = 0; i < remainingCities.Count; i++)
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

        public void showMe()
        {
            foreach(string sequence in ordersList)
            {
                string[] s = sequence.Split(':');
                Console.WriteLine("{0,-25}{1,0}",("Hodnota nákladu: " + s[1]), s[0]);
            }
            
        }

        public void showMe(int i)
        {
            Console.WriteLine(ordersList[i]);
        }

        public void showMeId()
        {
            foreach (string s in ordersListIds) Console.WriteLine(s);
        }

        public string[] getOrderList(int index)
        {
            string[] tmpArray = this.ordersList[index].Split(':');

            return tmpArray[0].Split('-');
        }

        public string[] getOrderListId(int index)
        {
            string[] tmpArray = this.ordersListIds[index].Split(':');

            return tmpArray[0].Split('-');
        }

        public List<Invidual> getOrderListSorted()
        {
            return sortedOrder;
        }

        public void calculateRoutes(int x)
        {
            Algorithm algoritmus;
            Invidual result;

            algoritmus = new Algorithm(getOrderList(x));
            result = algoritmus.start();
            sortedOrder.Add(result);

        }

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

        public void translateRoutes()
        {

            foreach (Invidual invidual in sortedOrder) invidual.translateSequence();

        }

        public void calculatePrice()
        {
            this.cost = 0;
            foreach (Invidual invidual in sortedOrder) this.cost = this.cost + (invidual.getDistanceString() * DISTANCE_COST);

            this.cost = this.cost + (sortedOrder.Count * CAR_COST);
        }

        public double getCost()
        {
            return this.cost;
        }


    }
}
