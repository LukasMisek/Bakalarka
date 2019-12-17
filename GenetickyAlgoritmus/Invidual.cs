﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenetickyAlgoritmus
{
    /// <summary>
    /// Třída Inviduál obsahuje konstruktor, ohodnovací funkce a gettery
    /// </summary>
    public class Invidual
    {
        private char[] sequence = new char[Algorithm.LENGTH];

        /// <summary>
        /// Konstruktor bez argumentu -> Náhodna tvorba jedince (Používá se na začátku)
        /// </summary>
        public Invidual()
        {
            string stringCities = Algorithm.cities.getCityNames();
            List<char> unusedCities = new List<char>();

            foreach (char city in stringCities) unusedCities.Add(city);

            var rnd = new Random();

            for (int i = 0; i < sequence.Length; i++)
            {
                int a = rnd.Next(0, unusedCities.Count);
                this.sequence[i] = unusedCities[a];
                unusedCities.RemoveAt(a);
            }
 
        }

        /// <summary>
        /// Projde celou sekvenci a vypočítá vzdálenost mezi sekvencí měst.
        /// Používám funkci cities.getCityDistance(město 1, město2)
        /// </summary>
        /// <returns>int Distance</returns>
        public int getDistance()
        {
            int distance = 0;

            for (int i = 0; i < this.sequence.Length - 1; i++)
                distance = distance + Algorithm.cities.getCityDistance(this.sequence[i], this.sequence[i + 1]);

            return distance;
        }

        /// <summary>
        /// Operátor mutace. 2 náhodné geny jsou prohozeny
        /// </summary>
        public void mutate()
        {
            var rnd = new Random();
            
            int a = rnd.Next(0, this.sequence.Length);
            int b = rnd.Next(0, this.sequence.Length);
            while (a == b) b = rnd.Next(0, this.sequence.Length);

            char tmp;
            tmp = this.sequence[a];
            this.sequence[a] = this.sequence[b];
            this.sequence[b] = tmp;
        }

        /// <summary>
        /// Konstruktor s argumentem -> Tvorba jedince ze 2 rodičů
        /// </summary>
        /// <param name="p1">Rodič 1</param>
        /// <param name="p2">Rodič 2</param>
        public Invidual(Invidual p1, Invidual p2)
        {
            var rnd = new Random();
            int a = rnd.Next(0, 1);

            if (a == 0)
            {
                for (int i = 0; i < Algorithm.CROSSIN_POINT; i++) this.sequence[i] = p1.sequence[i];
                for (int i = Algorithm.CROSSIN_POINT; i < Algorithm.LENGTH; i++) this.sequence[i] = p2.sequence[i];
            }
            else
            {
                for (int i = 0; i < Algorithm.CROSSIN_POINT; i++) this.sequence[i] = p2.sequence[i];
                for (int i = Algorithm.CROSSIN_POINT; i < Algorithm.LENGTH; i++) this.sequence[i] = p1.sequence[i];
            }

            if (getDuplicity() > 0) fixMe();
        }

        /// <summary>
        /// Funkce si vypočítá nepoužitá města
        /// Následně prochází sekvenci a nahrazuje duplicity těmi nepoužitými
        /// </summary>
        private void fixMe()
        {
            // Stáhnu seznam měst
            string unusedCities = Algorithm.cities.getCityNames();
            List<char> unused = new List<char>(unusedCities);

            // Smažu použitá města
            for (int i = 0; i < this.sequence.Length; i++)
                for (int j = 0; j < unused.Count; j++)
                    if (unused[j] == this.sequence[i]) unused.RemoveAt(j);

            for (int i = 0; i < this.sequence.Length; i++)
                {
                    int counter = 0;
                    for (int j = 0; j < this.sequence.Length; j++)
                    {
                        if (this.sequence[i] == this.sequence[j]) counter++;
                        if (counter > 1)
                        {
                            this.sequence[j] = unused[unused.Count - 1];
                            unused.RemoveAt(unused.Count - 1);
                            counter = 0;
                        }
                    }
                }

        }

        /// <summary>
        /// Ohodnotí jedince a vrátí počet duplicit
        /// Stejný prvek = kladné body
        /// </summary>
        /// <returns>Int Duplicity</returns>
        public int getDuplicity()
        {
            char[] occurences = this.sequence.Distinct().ToArray();

            int score = 0;

            foreach(char c in occurences)
            {
                int occurence = -1;
                for (int i = 0; i < this.sequence.Length; i++)
                {
                    if (c == this.sequence[i]) occurence++;
                }
                score = score + occurence;
            }
            return score;
            
        }

        /// <summary>
        /// Vrátí sequenci genů jako string (použito pro tisk)
        /// </summary>
        /// <returns>string sequence</returns>
        public string getSequence()
        {
            string output = null;

            for (int i = 0; i< this.sequence.Length; i++) output = output + this.sequence[i];

            return output;
        }

    }
}
