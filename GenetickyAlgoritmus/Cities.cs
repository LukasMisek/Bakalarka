﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Cities
    {
        // Seznam měst, mezi kterými bud jezdit
        public char[] cities;

        private int[,] lengthMatrix;

        public int cityCount;

        public Cities()
        {
            this.cityCount = Algorithm.LENGTH;
            cities = new char[cityCount];
            lengthMatrix = new int[cityCount, cityCount];
            generateCityNames();
            generateMatrix();
            //loadFromFile();
            //saveMAtrix();

        }

        public Cities(string sequence)
        {
            this.cityCount = sequence.Length;
            cities = new char[cityCount];
            lengthMatrix = new int[cityCount, cityCount];
            generateCityNames(sequence);
            generateMatrix();
            //loadFromFile();
            //saveMAtrix();

        }

        /// <summary>
        /// Vygeneruje nazvy měst. Města nazvu jako písmena. Začnu od A.
        /// </summary>
        public void generateCityNames()
        {
            for (int i = 0; i < this.cityCount; i++) this.cities[i] = Convert.ToChar(32 + i);
        }

        public void generateCityNames(string s)
        {
            for (int i = 0; i < s.Length; i++) this.cities[i] = s[i];
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
        public string getCityNames()
        {
            string output = null;

            foreach (char s in cities) output = output + s;

            return output;
        }

        /// <summary>
        /// Vrátí vzdálenost mezi 2 městy jako INT.
        /// Vstupní argumenty jsou názvy měst.
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns>Int Distance</returns>
        public int getCityDistance(char cityA, char cityB)
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
        public char getCityName(int i)
        {
            return cities[i];
        }

        /// <summary>
        /// Sestaví a zobrazí matici vzdáleností pomocí Console.WriteLine
        /// </summary>
        public void showMatrix()
        {
            string cityNames = getCityNames();

            string s = "\t";

            foreach (char city in cityNames) s = s + city + "\t";

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
        
    }
}
