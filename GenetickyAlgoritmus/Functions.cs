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

        /// <summary>
        /// Zkombinuje 2 Listy. Vždy 50% a 50%
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static List<string> mixLists(List<string> l1, List<string> l2)
        {
            int length = 0;
            if (l1.Count > l2.Count) length = l1.Count;
            else length = l2.Count;

            List<string> l_out = new List<string>();

            for (int i = 0; i < length/2; i++) l_out.Add(l1[i]);
            for (int i = length/2; i < l2.Count; i++) l_out.Add(l2[i]);

            Console.WriteLine("namichal jsem list");
            Functions.showList(l_out);
            Console.ReadLine();

            return l_out;
        }

        /// <summary>
        /// Zkombinuje 2 stringy a vrátí výsledek
        /// Kombinace je vždy 50% na 50%
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string mixStrings(string s1, string s2)
        {
            string[] s1SplitArray = s1.Split(':');
            string[] s2SplitArray = s2.Split(':');
            string[] s1Split = s1SplitArray[0].Split('-');
            string[] s2Split = s2SplitArray[0].Split('-');

            int length = 0;
            if (s1Split.Length > s2Split.Length) length = s2Split.Length;
            else length = s1Split.Length;

            string s_out = s1Split[0];

            for (int i = 1; i < length / 2; i++) s_out = s_out + "-"+ s1Split[i];
            for (int i = length / 2; i < s2Split.Length; i++) s_out = s_out +"-"+ s2Split[i];

            return s_out;
        }

        /// <summary>
        /// Zkombinuji 2 listy náhodným způsobem.
        /// Kombinuji každý řádek
        /// Pokud listy nejsou stejně dlouhé, tak zbytek přidám na konec
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static List<string> mixListsString(List<string> l1, List<string> l2)
        {
            var rnd = new Random();
            int a = rnd.Next(0, 2);

            List<string> l_out = new List<string>();
            int length = 0;

            if (l1.Count > l2.Count) length = l2.Count;
            else length = l1.Count;

            for (int i = 0; i < length; i++)
            {
                a = rnd.Next(0, 2);
                if (a == 0) l_out.Add(mixStrings(l1[i], l2[i]));
                else l_out.Add(mixStrings(l2[i], l1[i]));
            }

            if (l1.Count > l2.Count) for (int i = length; i < l1.Count; i++)
                {
                    string[] s_tmp = l1[i].Split(':');
                    l_out.Add(s_tmp[0]);
                }
            if (l2.Count > l1.Count) for (int i = length; i < l2.Count; i++)
                {
                    string[] s_tmp = l2[i].Split(':');
                    l_out.Add(s_tmp[0]);
                }
            return l_out;
        }

        public static string[] getArrayCities(string s)
        {
            string[] sSplit = s.Split(':');
            return sSplit[0].Split('-');
        }

        public static string createIdList(string[] s, string value)
        {
            string s_out = s[0];
            for (int i = 1; i < s.Length; i++)
            {
                s_out = s_out + "-" + s[i];
            }
            s_out = s_out + ":" + value;
            return s_out;
        }

        public static string[] removeDuplicityStringArray(string[] s)
        {
            List<string> l_tmp = new List<string>();

            for (int i = 0; i < s.Length; i++) if (!l_tmp.Contains(s[i])) l_tmp.Add(s[i]);

            return l_tmp.ToArray();
        }
    }
}
