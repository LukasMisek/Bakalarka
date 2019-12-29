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

        // Seznam jedinců. Tohle je výsledek. Každý jedinec je sekvence měst.
        private List<Invidual> sortedOrder = new List<Invidual>();


        public static int counter = 0;

        public Orders()
        {
            generateRandomOrders();
        }

        public void generateRandomOrders()
        {
            List<string> remainingCities = InputOutput.getUniqueColumnsValuesActiveOrders(0);

            var rnd = new Random();
            int a = rnd.Next(0, remainingCities.Count);

            string newOrder = Controller.activeOrdersTable.Rows[a]["Obec"].ToString();
            remainingCities.RemoveAt(a);
            int newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());

            for (int i = 0; i < remainingCities.Count; i++)
            {
                a = rnd.Next(0, remainingCities.Count);
                if (newOrderValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString()) < 30)
                {
                    newOrder = newOrder + "-" + Controller.activeOrdersTable.Rows[a]["Obec"].ToString();
                    remainingCities.RemoveAt(a);
                    newOrderValue = newOrderValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
                }
                else
                {
                    newOrder = newOrder + ":" + newOrderValue;
                    this.ordersList.Add(newOrder);
                    newOrder = Controller.activeOrdersTable.Rows[a]["Obec"].ToString();
                    remainingCities.RemoveAt(a);
                    newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a]["Count"].ToString());
                }
            }

            if (newOrder.Length > 0)
            {
                newOrder = newOrder + ":" + newOrderValue;
                this.ordersList.Add(newOrder);
            }
        }

        public void showMe()
        {
            foreach(string s in ordersList) Console.WriteLine(s);
        }

        public string[] getOrderList(int index)
        {
            string s = "";
            string[] tmpArray = this.ordersList[index].Split(':');

            return tmpArray[0].Split('-');
        }

        /*
        public void calculateRoutes()
        {
            Algorithm algoritmus;
            Invidual result;

            for (int i = 0; i < this.orders.Count; i++)
            {
                algoritmus = new Algorithm(this.orders[i], this.orders[i].Length);
                result = algoritmus.start();
                ordersRoutes.Add(result);
            }
        }
        */




    }
}
