using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Orders
    {


        // Města mezi kterými počítám vzdálenosti
        public static Cities C_cities;

        public static int ORDER_CAPACITY = 64;

        private string[] cities;
        private int[] cityOrders;

        private List<string> orders = new List<string>();
        private List<int> ordersValue = new List<int>();
        private List<Invidual> ordersRoutes = new List<Invidual>();

        public static int counter = 0;

        public Orders()
        {
            cities = new string[Algorithm.LENGTH];
            cityOrders = new int[Algorithm.LENGTH];
            C_cities = new Cities();
            setCities();
            generateCitiyValues();
            generateOrders();
        }

        public Orders(string sequence)
        {
            cities = new string[sequence.Length];
            cityOrders = new int[sequence.Length];
            C_cities = new Cities(sequence);
            setCities();
            generateCitiyValues();
            generateOrders();
        }

        public string getCitiesString()
        {
            string s = "";
            for (int i = 0; i < this.cities.Length; i++) s = s + this.cities[i];
            return s;
        }

        /// <summary>
        /// Vygeneruje požadavky pro města
        /// cityOrders[0] = 5   => město index 0 požaduje dodat 5 jednotek
        /// </summary>
        private void generateCitiyValues()
        {

            for (int i = 0; i < cityOrders.Length; i++)
            {
                var rnd = new Random();
                int a = rnd.Next(1, 11);
                this.cityOrders[i] = a;
            }

        }


        /// <summary>
        /// Nahraje si mesta z tridy Cities
        /// </summary>
        private void setCities()
        {
            string[] s = C_cities.getCityNames();

            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i]+"";
        }

        private void setCities(string s)
        {

            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i]+"";
        }

        private void generateOrders()
        {
            string[] unused = new string[cities.Length];
            unused = this.cities;
            int[] unusedValues = new int[cities.Length];
            unusedValues = this.cityOrders;

            var rnd = new Random();

            string newOrder = "";
            int newValue = 0;
            int a = 0;

            for (int i = 0; i < unused.Length; i++)
            {
                a = rnd.Next(0, unused.Length);
                while (unused[a] == " ") a = rnd.Next(0, unused.Length);

                if (newValue + unusedValues[a] < ORDER_CAPACITY)
                {
                    newOrder = newOrder + unused[a];
                    newValue = newValue + unusedValues[a];
                    unused[a] = " ";

                }
                else
                {
                    this.orders.Add(newOrder);
                    this.ordersValue.Add(newValue);
                    newOrder = unused[a] + "";
                    newValue = unusedValues[a];
                    unused[a] = " ";
                }
            }

            if (newOrder.Length > 0)
            {
                this.orders.Add(newOrder);
                this.ordersValue.Add(newValue);
            }

        }

        public void showData()
        {
            string s = "";

            for (int i = 0; i < this.cities.Length; i++) s = s + this.cities[i] + "\t";
            Console.WriteLine(s);

            s = "";
            for (int i = 0; i < this.cityOrders.Length; i++) s = s + this.cityOrders[i] + "\t";
            Console.WriteLine(s);
        }

        public void showMatrix()
        {
            for (int i = 0; i < this.orders.Count; i++)
            {
                Console.WriteLine(counter + " " + this.orders[i] + " \tHodnota nákladu: " + this.ordersValue[i]);
                counter++;
            }
        }

        public string[] getOrders()
        {
            string[] output = new string[this.orders.Count];
            for (int i = 0; i < this.orders.Count; i++) output[i] = this.orders[i];

            return output;
        }

        public List<string> getOrdersList()
        {
            return orders;
        }

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

        public void showBest()
        {
            Console.WriteLine("jedinec:");
            for(int i = 0; i < ordersRoutes.Count; i++)
            {
                Console.WriteLine(" Nejlepší jedinec: " + ordersRoutes[i].getSequence() + "\tVzdálenost:" + ordersRoutes[i].getDistance());
            }
            
        }

        public int orderCount()
        {
            return orders.Count;
        }

    }
}
