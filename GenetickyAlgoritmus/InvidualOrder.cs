using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class InvidualOrder
    {
        public static int ORDER_CAPACITY = 64;

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

        private string[] sequence = new string[Algorithm.LENGTH];

        /// <summary>
        /// Vygeneruji jedince na začátku -> náhodně
        /// </summary>
        public InvidualOrder()
        {
            List<string> remainingCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);

            var rnd = new Random();
            int a = rnd.Next(0, remainingCities.Count);
            string city = remainingCities[a];
            int cityCount = remainingCities.Count - 1;

            string newOrderId = Cities.getId(remainingCities[a]);
            remainingCities.RemoveAt(a);
            int newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a+1]["Count"].ToString());

            for (int i = 0; i < cityCount; i++)
            {
                a = rnd.Next(0, remainingCities.Count);
                if (newOrderValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a+1]["Count"].ToString()) < ORDER_CAPACITY)
                {
                    newOrderId = newOrderId + "-" + Cities.getId(remainingCities[a]);
                    remainingCities.RemoveAt(a);
                    newOrderValue = newOrderValue + Convert.ToInt32(Controller.activeOrdersTable.Rows[a+1]["Count"].ToString());
                }
                else
                {
                    newOrderId = newOrderId + ":" + newOrderValue;
                    this.ordersListIds.Add(newOrderId);
                    newOrderId = Cities.getId(remainingCities[a]);
                    remainingCities.RemoveAt(a);
                    newOrderValue = Convert.ToInt32(Controller.activeOrdersTable.Rows[a+1]["Count"].ToString());
                }
            }

            if (newOrderId.Length > 0)
            {
                newOrderId = newOrderId + ":" + newOrderValue;
                this.ordersListIds.Add(newOrderId);
            }

        }
        
        /// <summary>
        /// Vygeneruji jedince ze 2 jiných jedinců => rodičů
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        public InvidualOrder(InvidualOrder i1, InvidualOrder i2)
        {
            var rnd = new Random();
            int a = rnd.Next(0, 2);

            if (a == 0) this.ordersListIds = Functions.mixListsString(i1.getOrdersId(), i2.getOrdersId());
                
            else this.ordersListIds = Functions.mixListsString(i2.getOrdersId(), i1.getOrdersId());

            /*
            Console.WriteLine("ted jsem kzombinoval jedince");
            Functions.showList(this.ordersListIds);
            Console.ReadLine();
            */

            if (getDuplicity() > 0)
            {
                fixMe();
                calculateRoute();
            }

            /*
            Console.WriteLine("ted jsem ho naformatoval");
            Functions.showList(this.ordersListIds);
            Console.ReadLine();
            */

            calculateValue();

            /*
            Console.WriteLine("pridal jsem value");
            Functions.showList(this.ordersListIds);
            Console.ReadLine();
            */
        }

        private void calculateRoute()
        {
            List<string> l_tmp = new List<string>(this.ordersListIds);
            ordersListIds.Clear();
            int value = Cities.getCityCount(Convert.ToInt32(l_tmp[0]));
            string route = l_tmp[0];

            for (int i = 1; i < l_tmp.Count; i++)
            {
                if ((value + Cities.getCityCount(Convert.ToInt32(l_tmp[i]))) < ORDER_CAPACITY)
                {
                    value = value + Cities.getCityCount(Convert.ToInt32(l_tmp[i]));
                    route = route + "-" + l_tmp[i];
                }
                else
                {
                    ordersListIds.Add(route);
                    route = l_tmp[i];
                    value = Cities.getCityCount(Convert.ToInt32(l_tmp[i]));
                }
            }
            if (route.Length > 0) ordersListIds.Add(route);
        }

        private void calculateValue()
        {
            int value = 0;
            for (int i = 0; i < ordersListIds.Count; i++)
            {
                string[] sequence = ordersListIds[i].Split('-');
                for (int j = 0; j < sequence.Length; j++) value = value + Cities.getCityCount(Convert.ToInt32(sequence[j]));

                ordersListIds[i] = ordersListIds[i] + ":" + value;
                value = 0;
            }

        }

        public void fixMe()
        {
            // sezenu seznam vsech mest
            List<string> unusedCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);
            List<string> unusedCitiesId = new List<string>();
            List<string> usedCitiesId = getRouteString();

            foreach (string city in unusedCities) unusedCitiesId.Add(Cities.getId(city));

            // Vyberu nepoužité
            List<string> l_tmp = new List<string>();
            for (int i = 0; i < unusedCitiesId.Count; i++) if (!usedCitiesId.Contains(unusedCitiesId[i])) l_tmp.Add(unusedCitiesId[i]);
            unusedCitiesId = l_tmp;

            // Označím duplicitu křížky
            for (int i = 0; i < usedCitiesId.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < usedCitiesId.Count; j++)
                {
                    if (usedCitiesId[i] == usedCitiesId[j]) counter++;
                    if (counter > 1)
                    {
                        usedCitiesId[j] = "X";
                        counter = 1;
                    }
                }
            }

            // Nahradím křížky za města
            for (int i = 0; i < usedCitiesId.Count; i++)
            {
                if (usedCitiesId[i] == "X")
                { 
                    if (unusedCitiesId.Count > 0)
                    {
                        usedCitiesId[i] = unusedCitiesId[unusedCitiesId.Count - 1];
                        unusedCitiesId.Remove(unusedCitiesId[unusedCitiesId.Count - 1]);
                    }
                    else usedCitiesId.RemoveAt(i);
                }
            }

            // Odeberu zbyvajici krizky
            while (usedCitiesId.Contains("X")) usedCitiesId.Remove("X");

            // pridam zbytek co zustal
            if (unusedCitiesId.Count > 0) usedCitiesId.AddRange(unusedCitiesId);

            this.ordersListIds = usedCitiesId;
        }

        public void showMeId()
        {
            foreach (string sequence in ordersListIds) Console.WriteLine("{0,-25}", sequence);

        }
        /*
        public List<string> getUsedCities()
        {
            //List<string> unusedCities = InputOutput.getUniqueColumnsValuesActiveOrders(0);

            
        }
        */

        public List<string> getOrdersId()
        {
            return this.ordersListIds;
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
        /// Spustí algoritmus pro všechny cesty
        /// </summary>
        public void calculateRoutes()
        {
            Algorithm algoritmus;
            Invidual result;

            sortedOrder.Clear();

            for (int i = 0; i < this.ordersListIds.Count; i++)
            {
                string[] idList = ordersListIds[i].Split(':');

                algoritmus = new Algorithm(Functions.getArrayCities(ordersListIds[i]));
                result = algoritmus.start();
                sortedOrder.Add(result);
                ordersListIds[i] = Functions.createIdList(result.getSequenceArray(), idList[1]);
            }
        }

        public void showSortedOrders()
        {
            foreach (Invidual invidual in sortedOrder)
            {
                invidual.showMe();
            }
        }

        public void showSortedOrdersTranslated()
        {
            foreach (Invidual invidual in sortedOrder)
            {
                invidual.showMeTranslated();
            }
        }

        public void showSortedOrdersFull()
        {
            foreach (Invidual invidual in sortedOrder)
            {
                invidual.showMeFull();
            }
            Console.WriteLine("Celková vzdálenost:" + getFitness() + "\nPočet aut: " + sortedOrder.Count);
        }

        public double getFitness()
        {
            double fitness = 0;
            foreach (Invidual invidual in sortedOrder) fitness = fitness + (invidual.getDistance());
            fitness = (fitness * Algorithm.DISTANCE_COST)+ (sortedOrder.Count * Algorithm.CAR_COST);
            return fitness;
        }

        public void translateIdToCity()
        {
            for (int i = 0; i < this.sequence.Length; i++) this.sequence[i] = Cities.getCityName(Convert.ToInt32(this.sequence[i]));
        }

        /// <summary>
        /// Vypočítá cenu ke každé cestě
        /// </summary>
        public void calculatePrice()
        {
            this.cost = 0;
            foreach (Invidual invidual in sortedOrder) this.cost = this.cost + (invidual.getDistance() * DISTANCE_COST);

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

        public double getDistance()
        {
            double distance = 0;

            for (int i = 0; i < this.sequence.Length - 1; i++)
                //distance = distance + Cities.getDistance(this.sequence[i], this.sequence[i + 1]);
                //distance = distance + Cities.getDistanceId(this.sequence[i], this.sequence[i + 1]);
                distance = distance + Cities.getDistance(Convert.ToInt32(this.sequence[i]), Convert.ToInt32(this.sequence[i + 1]));

            return distance;
        }

        public void translateRoutes()
        {
            foreach (Invidual invidual in sortedOrder) invidual.translateSequence();

        }

        public List<string> getRouteString()
        {
            List<string> l_out = new List<string>();
            // prepisu pouzita mesta do lepsiho formatu
            foreach (string route in ordersListIds)
            {
                string[] cities = route.Split('-');

                foreach (string city in cities) l_out.Add(city);
            }

            return l_out;
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
        /// Ohodnotí jedince a vrátí počet duplicit
        /// Stejný prvek = kladné body
        /// </summary>
        /// <returns>Int Duplicity</returns>
        public int getDuplicity()
        {
            List<string> usedCitiesId = getRouteString();
            int occurences = 0;
            
            for (int i = 0; i < usedCitiesId.Count; i++)
            {
                int counter = 0;
                for (int j = 0; j < usedCitiesId.Count; j++)
                {
                    if (usedCitiesId[i] == usedCitiesId[j]) counter++;
                    if (counter > 1)
                    {
                        counter = 1;
                        occurences++;
                    }
                }
            }

            return occurences/2;

        }
    }
}
