using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Cities
    {
        // Seznam měst, mezi kterými bud jezdit
        public string[] cities;

        private int[,] lengthMatrix;

        public int cityCount;
        
        public Cities()
        {
            this.cityCount = Algorithm.LENGTH;
            cities = new string[cityCount];
            lengthMatrix = new int[cityCount, cityCount];
            generateCityNames();
            generateMatrix();
            //loadFromFile();
            //saveMAtrix();

        }

        public Cities(string sequence)
        {
            this.cityCount = sequence.Length;
            cities = new string[cityCount];
            lengthMatrix = new int[cityCount, cityCount];
            generateCityNames(sequence);
            generateMatrix();
            //loadFromFile();
            //saveMAtrix();

        }

        public Cities(string[] sequence)
        {
            this.cityCount = sequence.Length;
            cities = new string[cityCount];
            generateCityNames(sequence);
        }
        
        /// <summary>
        /// Vygeneruje nazvy měst. Města nazvu jako písmena. Začnu od A.
        /// </summary>
        public void generateCityNames()
        {
            for (int i = 0; i < this.cityCount; i++) this.cities[i] = Convert.ToChar(32 + i)+"";
        }

        public void generateCityNames(string s)
        {
            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i]+"";
        }

        public void generateCityNames(string[] s)
        {
            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i] + "";
        }


        /// <summary>
        /// Načtu ze soubory matici vzdáleností ze souboru. Matice je nazvaná lengthMatrix.
        /// Adresa souboru je statická: @"c:\text.txt"
        /// Tato matice obsahuje nejkratší vzdálenosti mezi jednotlivými body.
        /// Např:
        ///   A B C
        /// A 0 1 2     z A do B je vzdálenost 1
        /// B 1 0 3     z B do C je vzdálenost 3 
        /// C 2 3 0     z A do C je vzdálenost 2
        /// </summary>
        public void loadFromFile()
        {
            string address = Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "LengthMatrix.txt";

            string[] lines = File.ReadAllLines(@address);

            int i = 0;
            foreach (string line in lines)
            {
                string[] columns = line.Trim().Split('\t');
                for (int j = 0; j < columns.Length; j++) lengthMatrix[i, j] = Convert.ToInt32(columns[j]);

                i++;
            }

        }

        /// <summary>
        /// Vrátí seznam měst jako 1 string
        /// </summary>
        /// <returns></returns>
        public string[] getCityNames()
        {
            List<string> tmpList = new List<string>();

            foreach (string s in cities) tmpList.Add(s);

            string[] output = tmpList.ToArray();

            return output;
        }

        /// <summary>
        /// Vrátí vzdálenost mezi 2 městy jako INT.
        /// Vstupní argumenty jsou názvy měst.
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns>Int Distance</returns>
        public int getCityDistance(string cityA, string cityB)
        {
            int a = 0;
            int b = 0;
            for( int i = 0; i < cities.Length; i++)
            {
                if (cityA == cities[i]) a = i;
                if (cityB == cities[i]) b = i;
            }

            return lengthMatrix[a, b];
        }

        /// <summary>
        /// Vrátí název města podle jeho indexu
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string getCityName(int i)
        {
            return cities[i];
        }

        /// <summary>
        /// Sestaví a zobrazí matici vzdáleností pomocí Console.WriteLine
        /// </summary>
        public void showMatrix()
        {
            string[] cityNames = getCityNames();

            string s = "\t";

            foreach (string city in cityNames) s = s + city + "\t";

            Console.WriteLine(s);

            for (int i = 0; i < lengthMatrix.GetLength(0); i++)
            {
                s = cities[i] + "\t";

                for (int j = 0; j < lengthMatrix.GetLength(1); j++) s = s + lengthMatrix[i, j] + "\t";

                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Vygeneruje náhodnou matici vzdáleností
        /// </summary>
        public void generateMatrix()
        {
            var rnd = new Random();

            for (int i = 0; i < this.cities.Length; i++)
            {
                for (int j = 0; j < this.cities.Length; j++)
                {
                    lengthMatrix[i, j] = rnd.Next(1, 10);
                    lengthMatrix[j, i] = lengthMatrix[i, j];
                }
            }

            for (int i = 0; i < this.cities.Length; i++) lengthMatrix[i, i] = 0;

        }

        public void generateMatrix(string s)
        {
            var rnd = new Random();

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    lengthMatrix[i, j] = rnd.Next(1, 10);
                    lengthMatrix[j, i] = lengthMatrix[i, j];
                }
            }

            for (int i = 0; i < s.Length; i++) lengthMatrix[i, i] = 0;

        }

        /// <summary>
        /// Uloží matici vzdáleností do souboru
        /// Cesta do složky s projektem: ...Projekt\GenetickyAlgoritmus\bin\Debug\netcoreapp2.0.\
        /// Příklad: C:\Users\Lukáš Míšek\Desktop\Bakalarka\Program\GenetickyAlgoritmus\bin\Debug\netcoreapp2.0\LengthMatrix.txt
        /// </summary>
        public void saveMAtrix()
        {
            System.IO.TextWriter writeFile = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "LengthMatrix.txt");

            string s = null;

            for (int i = 0; i < this.cities.Length; i++)
            {
                for (int j = 0; j < this.cities.Length; j++) s = s + lengthMatrix[i, j] + "\t";
                s = s + "\n";
            }

            writeFile.Write(s);
            writeFile.Flush();
            writeFile.Close();
            writeFile = null;
        }
        
        /// <summary>
        /// Vypocte vzdalenost mezi 2 body. x1 a y1 je bod 1. x2 a y2 je bod 2.
        /// Vysledek je zaokrouhlen na 4 desetinna mista.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static double getDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Round(Math.Pow(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2), 0.5), 4);

        }

        /// <summary>
        /// Vrátí vzdálenost mezi 2 městy. Vstupem je název měst
        /// Výsledek zaokrouhlen na 4 desetinná místa
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns></returns>
        public static double getDistance(string city1, string city2)
        {
            int index1 = getIndex(city1);
            int index2 = getIndex(city2);

            int x1 = Convert.ToInt32(Controller.allCitiesTable.Rows[index1]["X"].ToString());
            int x2 = Convert.ToInt32(Controller.allCitiesTable.Rows[index2]["X"].ToString());
            int y1 = Convert.ToInt32(Controller.allCitiesTable.Rows[index1]["Y"].ToString());
            int y2 = Convert.ToInt32(Controller.allCitiesTable.Rows[index2]["Y"].ToString());

            return Math.Round(Math.Pow(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2), 0.5), 4);

        }


        /// <summary>
        /// Vrátí index města, podle kterého je možné v tabulce allCitiesTable najít jeho X a Y souřadnice
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static int getIndex(string city)
        {
            int coordinate = 0;
            for (int i = 0; i < Controller.allCitiesTable.Rows.Count; i++)
                if (Controller.allCitiesTable.Rows[i]["Obec"].ToString() == city && 
                    Controller.allCitiesTable.Rows[i]["Kraj"].ToString() == Controller.selectedFile &&
                    Controller.allCitiesTable.Rows[i]["Okres"].ToString() == Controller.selectedArea)
                    coordinate = i;

            return coordinate;
        }        
        
    }
}
