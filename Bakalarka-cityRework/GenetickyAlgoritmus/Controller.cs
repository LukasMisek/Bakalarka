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

        public Controller()
        {
            InputOutput.start();

            /*
            Functions.showDataActiveOrdersTable();
            Console.ReadLine();
            */
            
            /*
            Functions.showDataAllCitiesTable();
            Console.ReadLine();
            */
            /*
            List<string> testList = Functions.getUniqueColumnsValuesFile(1, selectedFile, selectedArea);

            Functions.showList(testList);

            Console.ReadLine();
            */
            Orders[]orders = new Orders[10];

            for (int i = 0; i < 10; i++)
            {
                orders[i] = new Orders();
            }

            /*
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("\nORDER ZOBRAZUJI. CISLO:  " + i + "\n");
                orders[i].showMe();
                orders[i].showMeId();
            }

            Console.ReadLine();
            */

            /*
            Algorithm alg = new Algorithm(s);
            result = alg.start();
            Console.WriteLine("Invidual top: " + "(" +result.getDistance() + ")\t" + result.showSequence());
            Console.ReadLine();
            */


            /*
            Console.WriteLine("x:");
            int x = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("y:");
            int y = Convert.ToInt32(Console.ReadLine());

            orders[x].calculateRoutes(y);
            List<Invidual> testList = orders[x].getOrderListSorted();

            Console.WriteLine("Invidual top: " + "(" + testList[0].getDistance() + ")\t" + testList[0].showSequence());

            Console.ReadLine();
            */

            /*
            for (int i = 0; i < orders.Length; i++)
            {
                Console.WriteLine("zahajuji algoritmus pro: " + i);
                orders[z].calculateRoutes(i);
                orders[z].showMe(i);
                Console.WriteLine("vysledek pro: " + i);
                List<Invidual> results = orders[z].getOrderListSorted();
                foreach (Invidual inv in results) Console.WriteLine("Invidual top: " + "(" + inv.getDistance() + ")\t" + inv.showSequence());
            }

            Console.ReadLine();
            */

            for (int i = 0; i < orders.Length; i++) orders[i].calculateRoutes();

            for (int i = 0; i < orders.Length; i++)
            {

                Console.WriteLine("\nORDER ZOBRAZUJI. CISLO:  " + i + "\n");
                Console.WriteLine("Nesetříděné objednávky:");
                orders[i].showMe();

                List<Invidual> resultList;
                /*
                resultList = orders[i].getOrderListSorted();

                foreach (Invidual result in resultList)
                {
                    Console.WriteLine("Invidual top: " + "(" + result.getDistance() + ")\t" + result.showSequence());
                }
                */
                orders[i].translateRoutes();

                resultList = orders[i].getOrderListSorted();
                Console.WriteLine("Roztříděné objednávky:");
                for (int j = 0; j < resultList.Count; j++)
                {
                    Console.WriteLine("{0,-25}{1,0}", ("Vzdálenost: "+ resultList[j].getDistanceString()), resultList[j].showSequence());
                }

                orders[i].calculatePrice();

                Console.WriteLine("Cena varianty: " + orders[i].getCost());

                /*Console.WriteLine("Pokračovat?");
                Console.ReadLine();*/


            }
            Console.WriteLine("Konec!");
            Console.ReadLine();


            // Test section


            /*
            Trida nec = new Trida();

            Trida dalsi = new Trida(2, "baba");

            nec.changeMe(10, "gbhregh");

            double distance;

            int index = Cities.getIndex("Barchov");
            Console.WriteLine(index + "");

            index = Cities.getIndex("Bezděkov");
            Console.WriteLine(index + "");

            distance = Cities.getDistance("Barchov", "Bezděkov");
            Console.WriteLine(distance + "");

            Console.ReadLine();
            */
        }
    }
}
