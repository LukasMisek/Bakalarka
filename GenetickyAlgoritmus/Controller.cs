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

            
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("\nORDER ZOBRAZUJI. CISLO:  " + i + "\n");
                orders[i].showMe();
            }

            Console.ReadLine();
            
            string[] s = orders[1].getOrderList(1);

            Console.WriteLine("Pole, ktere chci pouzit:");
            Functions.showArray(s);

            Console.ReadLine();

            Invidual result;

            Population pop = new Population(s);
            result = pop.startAlgotithm();
            
            Console.WriteLine("Invidual top: " + "(" +result.getDistance() + ")\t" + result.showSequence());
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
