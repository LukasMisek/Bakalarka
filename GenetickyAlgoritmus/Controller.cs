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

        // Název Kraje, ze kterého budu počítat matici
        public static string selectedArea = "Pardubický kraj";

        public Controller()
        {
            InputOutput.start();

            List<string> testList = Functions.getUniqueColumnsValuesFile(1, selectedArea);

            Functions.showList(testList);

            double distance = Cities.getDistance(1, 2, 3, 3);
            Console.WriteLine(distance + "");

            int index = Cities.getIndex("Albrechtice");
            Console.WriteLine(index + "");

            index = Cities.getIndex("Biskupice");
            Console.WriteLine(index + "");

            distance = Cities.getDistance("Albrechtice", "Biskupice");
            Console.WriteLine(distance + "");

            Console.ReadLine();
        }

    }
}
