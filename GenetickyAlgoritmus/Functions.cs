using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GenetickyAlgoritmus
{
    public static class Functions
    {
        /// <summary>
        /// Převede znak z Dec do Hex soustavy
        /// Vrátí jako char
        /// </summary>
        /// <param name="decValue"></param>
        /// <returns></returns>
        private static char hexChar(int decValue)
        {
            char[] hexValues = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            if (decValue > 0) return hexValues[decValue];
            else return '0';
        }

        /// <summary>
        /// Převede číslo z Dec do Hex soustavy
        /// </summary>
        /// <param name="decNumber"></param>
        /// <returns></returns>
        public static string toHex(int decNumber)
        {
            string output = "";
            int x = 0;
            int tmp = decNumber;
            for (int i = 3; i > 0; i--)
            {
                x = (int)(tmp / Math.Pow(16, i));
                tmp = tmp - (int)(x * Math.Pow(16, i));
                output = output + hexChar(x);
            }
            return output + hexChar(tmp);
        }

        /// <summary>
        /// Vrati sloupec z konkrétního souboru
        /// Sloupec je vrácen jako List<string>
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<string> getUniqueColumnsValuesFile(int columnIndex, string filename, string area)
        {
            List<string> values = new List<string>();

            string address = Controller.pathDistinctAreasTable + filename + ".txt";
            string[] lines = File.ReadAllLines(@address);

            lines = lines.Skip(1).ToArray();

            foreach (string line in lines)
            {
                string[] columns = line.Split('\t');
                if (!values.Contains(columns[columnIndex])) values.Add(columns[columnIndex]);
            }

            return values;

        }

        /// <summary>
        /// Zobrazí do konzole všechny prvky v listu
        /// </summary>
        /// <param name="list"></param>
        public static void showList(List<string> list)
        {
            foreach (string s in list) Console.WriteLine(s);
        }

        /// <summary>
        /// Zobrazí do konzole všechny prvky v listu
        /// </summary>
        /// <param name="list"></param>
        public static void showList(List<int> list)
        {
            foreach (int s in list) Console.WriteLine(s + " ");
        }

        /// <summary>
        /// Zobrazi do konzole vsechny prvky v poli array
        /// </summary>
        /// <param name="array"></param>
        public static void showArray(string[] array)
        {
            foreach (string s in array) Console.WriteLine(s + " ");
        }

        /// <summary>
        /// Zobrazím všechny data z tabulky allCitiesTable do Konzole
        /// </summary>
        public static void showDataAllCitiesTable()
        {
            Console.WriteLine(
                    "|{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}|{5,-25}|{6,-25}|",
                    "Id", "Obec", "Okres", "Kraj", "PSČ", "X", "Y");
            for (int i = 0; i < Controller.allCitiesTable.Rows.Count; i++)
            {
                Console.WriteLine(
                    "|{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}|{5,-25}|{6,-25}|",
                    Controller.allCitiesTable.Rows[i][0].ToString(),
                    Controller.allCitiesTable.Rows[i][1].ToString(),
                    Controller.allCitiesTable.Rows[i][2].ToString(),
                    Controller.allCitiesTable.Rows[i][3].ToString(),
                    Controller.allCitiesTable.Rows[i][4].ToString(),
                    Controller.allCitiesTable.Rows[i][5].ToString(),
                    Controller.allCitiesTable.Rows[i][6].ToString());
            }
        }

        /// <summary>
        /// Zobrazím všechny data z tabulky activeOrdersTable do Konzole
        /// </summary>
        public static void showDataActiveOrdersTable()
        {
            Console.WriteLine(
                "|{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}",
                "CustomerId", "Obec", "Okres", "Kraj", "Count");
            for (int i = 0; i < Controller.activeOrdersTable.Rows.Count; i++)
            {
                Console.WriteLine(
                    "|{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}",
                    Controller.activeOrdersTable.Rows[i][0].ToString(),
                    Controller.activeOrdersTable.Rows[i][1].ToString(),
                    Controller.activeOrdersTable.Rows[i][2].ToString(),
                    Controller.activeOrdersTable.Rows[i][3].ToString(),
                    Controller.activeOrdersTable.Rows[i][4].ToString());
            }
        }

    }
}
