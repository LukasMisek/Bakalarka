using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Orders
    {

        private char[] cities = new char[Algorithm.LENGTH];
        private int[] cityOrders = new int[Algorithm.LENGTH];

        private List<string> orders = new List<string>();
        private List<int> ordersValue = new List<int>();

        public Orders()
        {
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
            string s = Algorithm.cities.getCityNames();

            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i];
        }

        private void setCities(string s)
        {

            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i];
        }

        private void generateOrders()
        {
            char[] unused = new char[cities.Length];
            unused = this.cities;
            int[] unusedValues = new int[cities.Length];
            unusedValues = this.cityOrders;

            var rnd = new Random();

            string newOrder = "";
            int newValue = 0;
            int a = 0;
            
            for(int i = 0; i < unused.Length; i++)
            {
                a = rnd.Next(0, Algorithm.LENGTH);
                while (unused[a] == ' ') a = rnd.Next(0, Algorithm.LENGTH);

                if (newValue + unusedValues[a] < Algorithm.ORDER_CAPACITY)
                {
                    newOrder = newOrder + unused[a];
                    newValue = newValue + unusedValues[a];
                    unused[a] = ' ';

                }
                else
                {
                    this.orders.Add(newOrder);
                    this.ordersValue.Add(newValue);
                    newOrder = unused[a] + "";
                    newValue = unusedValues[a];
                    unused[a] = ' ';
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
            string cities = Algorithm.cities.getCityNames();

            for (int i = 0; i < this.orders.Count; i++)
            {
                Console.WriteLine(this.orders[i] + " Hodnota nákladu: " + this.ordersValue[i]);
            }
        }

        public string[] getOrders()
        {
            string[] output = new string[this.orders.Count];
            for (int i = 0; i < this.orders.Count; i++) output[i] = this.orders[i];

            return output;
        }

        public void calculateRoute()
        {
            foreach(string order in this.orders)
            {

                Console.WriteLine("order = " + order);

            }

        }

        public List<string> getOrdersList()
        {
            return orders;
        }

    }
}
