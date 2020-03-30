using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GenetickyAlgoritmus
{
    class Controller
    {
        // Seznam měst, se kterých budu vybírat
        public static System.Data.DataTable allCitiesTable = new DataTable("allCities");
        public static string pathAllCitiesTable = "..\\..\\..\\..\\" + "Coordinates.txt";
        public static string pathDistinctAreasTable = "..\\..\\..\\..\\";

        // Aktivní objednávky, kolik palet, který zákazník požaduje
        public static System.Data.DataTable activeOrdersTable = new DataTable("activeOrders");
        public static string pathActiveOrdersTable = "..\\..\\..\\..\\" + "ActiveOrders.txt";

        // Název Kraje, ze kterého budu počítat matici
        public static string selectedFile = "Pardubický kraj";
        public static string selectedArea = "Pardubice";

        public static string pathTest = "..\\..\\..\\..\\" + "Test.txt";
        public static string pathOutput = "..\\..\\..\\..\\Output\\";

        public Controller()
        {

            double fitness = 0;

            Console.ReadLine();
            InputOutput.start();

            /*
            Console.WriteLine("vylepsuji populaci");
            population.improve();

            population.showResultFull();
            Console.WriteLine("\n\nUkazal jsem vsechny nadjedince prelozene");
            Console.ReadLine();
            */


            /*
            Console.WriteLine("\n\nPokousim se vylepsit populaci");
            population.improve();
            Console.WriteLine("\n\nVylepšil jsem populaci");
            Console.ReadLine();

            population.showResultFull();
            Console.WriteLine("\n\nUkazal jsem vsechny nadjedince prelozene");
            Console.ReadLine();
            */

            

            Console.WriteLine("Pokusim se vylepsit populaci vicekrat");
            Console.ReadLine();

            Console.WriteLine("\n\nUkazal jsem vsechny nadjedince prelozene");
            Console.ReadLine();

            

            /*
            population.updateBest();
            InvidualOrder best = null;
            best = population.GetBest();
            Console.WriteLine("Stáhl jsem si nejlepsiho");
            Console.ReadLine();

            best.showMe();
            Console.WriteLine("ukazuji nejlepsiho");
            Console.ReadLine();
            */

            /*
            population.translateResult(9);
            population.showResultTranslated(9);
            Console.WriteLine("\n\nPřeložil jsem výsledek 9");
            Console.ReadLine();
            */


            /*

            InvidualOrder in1 = new InvidualOrder();
            InvidualOrder in2 = new InvidualOrder();
            List<string> listek = new List<string>();
            Console.WriteLine("ORDER 1:\n");
            in1.showMe();
            Console.WriteLine("ORDER 2:\n");
            in2.showMe();
            Console.WriteLine("KOMBINACE:\n");
            InvidualOrder n = new InvidualOrder(in1, in2);
            n.showMe();
            Console.ReadLine();
            Console.WriteLine("KOMBINACE ID:\n");
            n.showMeId();
            Console.ReadLine();

            */
            /*
            Console.WriteLine("calculate routes"); // spusteni algoritmu pro ordery
            n.calculateRoutes();

            Console.WriteLine("sorted orders"); // vysledek algoritmu
            n.showSortedOrders();
            Console.ReadLine();

            List<Invidual> resultList;
            Console.WriteLine("SHOW ME");
            resultList = n.getOrderListSorted();
            for (int j = 0; j < resultList.Count; j++)
            {
                Console.WriteLine("{0,-25}{1,0}", ("Vzdálenost: " + resultList[j].getDistanceString()), resultList[j].showSequence());
            }
            Console.ReadLine();

            Console.WriteLine("TRANSLATE");
            n.translateRoutes();
            resultList = n.getOrderListSorted();
            for (int j = 0; j < resultList.Count; j++)
            {
                Console.WriteLine("{0,-25}{1,0}", ("Vzdálenost: " + resultList[j].getDistanceString()), resultList[j].showSequence());
            }
            Console.ReadLine();
            */
            /*

            Orders[]orders = new Orders[10];

            for (int i = 0; i < 10; i++)
            {
                orders[i] = new Orders();
            }

            
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("\nORDER ZOBRAZUJI. CISLO:  " + i + "\n");
                orders[i].showMe();
                orders[i].showMeId();
            }

            Console.ReadLine();
            
            for (int i = 0; i < orders.Length; i++) orders[i].calculateRoutes();
                
            Console.ReadLine();


            
            for (int i = 0; i < orders.Length; i++)
            {
                Console.WriteLine("\nORDER ZOBRAZUJI. CISLO:  " + i + "\n");
                Console.WriteLine("Nesetříděné objednávky:");
                orders[i].showMe();

                List<Invidual> resultList;

                resultList = orders[i].getOrderListSorted();
                Console.WriteLine("Nepřeložené objednávky:");
                for (int j = 0; j < resultList.Count; j++)
                {
                    Console.WriteLine("{0,-25}{1,0}", ("Vzdálenost: " + resultList[j].getDistanceString()), resultList[j].showSequence());
                }


                orders[i].translateRoutes();

                resultList = orders[i].getOrderListSorted();
                Console.WriteLine("Roztříděné objednávky:");
                for (int j = 0; j < resultList.Count; j++)
                {
                    Console.WriteLine("{0,-25}{1,0}", ("Vzdálenost: "+ resultList[j].getDistanceString()), resultList[j].showSequence());
                }

                orders[i].calculatePrice();

                Console.WriteLine("Cena varianty: " + orders[i].getCost());
                Console.WriteLine("Pocet aut:" + orders[i].getOrdersCount());


            }
            Console.WriteLine("Konec!");
            Console.ReadLine();
            
    */

        }
    }
}
