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
                orders[i] = new Orders(true);
                Console.WriteLine("ORDEN VYGENEROVAN. CISLO:  " + i);
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("\nORDEN ZOBRAZUJI. CISLO:  " + i + "\n");
                orders[i].showMe();
            }




            Console.ReadLine();

            double distance;

            int index = Cities.getIndex("Barchov");
            Console.WriteLine(index + "");

            index = Cities.getIndex("Bezděkov");
            Console.WriteLine(index + "");

            distance = Cities.getDistance("Barchov", "Bezděkov");
            Console.WriteLine(distance + "");

            Console.ReadLine();

            // Cities cities = new Cities[]
        }
    }
}
