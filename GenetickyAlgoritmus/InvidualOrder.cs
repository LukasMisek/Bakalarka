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

        private int fitness = 0;

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

            if (a == 0)
            {
                this.ordersListIds = Functions.mixListsString(i1.getOrdersId(), i2.getOrdersId());
            }
                
            else
            {
                this.ordersListIds = Functions.mixListsString(i2.getOrdersId(), i1.getOrdersId());
            }

            calculateValue();
            calculateFitness();

            fixMe();
        }

        private void calculateValue()
        {
            int value = 0;
            for (int i = 0; i < ordersListIds.Count; i++)
            {
                string[] sequence = ordersListIds[i].Split('-');
                for (int j = 0; j < sequence.Length; j++) value = value + Convert.ToInt32(Controller.activeOrdersTable.Rows[j]["Count"].ToString());

                ordersListIds[i] = ordersListIds[i] + ":" + value;
                value = 0;
            }

        }

        public void fixMe()
        {
            // sezenu seznam vsech mest
            List<string> unusedCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);

            List<string> unusedCitiesId = new List<string>();

            List<string> usedCitiesId = new List<string>();

            foreach (string city in unusedCities) unusedCitiesId.Add(Cities.getId(city));

            // prepisu pouzita mesta do lepsiho formatu
            foreach (string route in ordersListIds)
            {
                string[] routeSplit = route.Split(':');
                string[] cities = routeSplit[0].Split('-');

                foreach (string city in cities) usedCitiesId.Add(city);
            }

            // Smažu použitá města
            for (int i = 0; i < usedCitiesId.Count; i++)
                for (int j = 0; j < unusedCitiesId.Count; j++)
                    if (unusedCitiesId[j] == usedCitiesId[i]) unusedCitiesId.RemoveAt(j);

            // pridam ty nepouzita mesta


            Console.WriteLine("toto budu opravovat:");
            Functions.showList(ordersListIds);
            Console.ReadLine();

            Console.WriteLine("takto to napisu:");
            Functions.showList(usedCitiesId);
            Console.ReadLine();

            Console.WriteLine("toto jsou skutecne nepouzita:");
            Functions.showList(unusedCitiesId);
            Console.ReadLine();

            Console.WriteLine("tady mam vsechna mesta:");
            Functions.showList(unusedCities);
            Console.ReadLine();

            Console.WriteLine("tady mam vsechna mesta ID:");
            Functions.showList(unusedCitiesId);
            Console.ReadLine();

            List<string> l_out = new List<string>();


            // sezenu seznam pouzitych mest
            // vypocitam seznam nepouzitych mest
            // odeberu duplicitni mesta
            // pridam chybejici mesta
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

        public void calculateFitness()
        {
            List<string> usedCities = new List<string>();

            // duplicity cities
            foreach (string id in ordersListIds)
            {
                if (usedCities.Contains(id))
                {
                    this.fitness = this.fitness + 1000; 
                }
                else
                {
                    usedCities.Add(id);
                }
            }
            
            List<string> unusedCities = InputOutput.getUniqueColumnsValuesActiveOrders(1);

            // missing cities
            foreach (string unusedCity in unusedCities)
            {
                if (!usedCities.Contains(unusedCity))
                {
                    this.fitness = this.fitness + 1000;
                }
            }
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

        /// <summary>
        /// Vrátí setříděnou sekvenci jako List
        /// </summary>
        /// <returns></returns>
        public List<Invidual> getOrderListSorted()
        {
            return sortedOrder;
        }

    }
}
