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
        private string[] sequence = new string[Algorithm.LENGTH];

        /// <summary>
        /// Konstruktor bez argumentu -> Náhodna tvorba jedince (Používá se na začátku)
        /// </summary>
        public Invidual()
        {
            string[] stringCities = Algorithm.cities;
            List<string> unusedCities = new List<string>();

            foreach (string city in stringCities) unusedCities.Add(city);

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
        public double getDistance()
        {
            double distance = 0;
            for (int i = 0; i < this.sequence.Length - 1; i++)
            {
                distance = distance + Cities.getDistance(Convert.ToInt32(this.sequence[i]), Convert.ToInt32(this.sequence[i + 1]));
            }
            return distance * 111;
        }

        /// <summary>
        /// Vrátí vzdálenost jako Double
        /// </summary>
        /// <returns></returns>
        public double getDistanceString()
        {
            double distance = 0;

            for (int i = 0; i < this.sequence.Length - 1; i++)
                distance = distance + Cities.getDistance(this.sequence[i], this.sequence[i + 1]);

            return distance * 111;
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

            string tmp;
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
            int a = rnd.Next(0, 2);

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
            string[] unusedCities = Algorithm.cities;
            List<string> unused = new List<string>(unusedCities);

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
            string[] occurences = this.sequence.Distinct().ToArray();

            int score = 0;

            foreach (string c in occurences)
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

            for (int i = 0; i < this.sequence.Length; i++) output = output + "-" + this.sequence[i];

            return output;
        }

        public string[] getSequenceArray()
        {
            return this.sequence;
        }

        /// <summary>
        /// Zobrazí jedince
        /// </summary>
        /// <returns></returns>
        public string showSequence()
        {
            string output = this.sequence[0];

            for (int i = 1; i < this.sequence.Length; i++) output = output + "-" + this.sequence[i];

            return output;
        }

        /// <summary>
        /// Přeloží jedince z ID do normálních názvů
        /// </summary>
        public void translateSequence()
        {
            for (int i = 0; i < this.sequence.Length; i++) this.sequence[i] = Cities.getCityName(Convert.ToInt32(this.sequence[i]));
        }

        public void showMe()
        {
            string s = this.sequence[0];

            for (int i = 1; i < this.sequence.Length; i++) s = s + "-" + this.sequence[i];
            s = s + ":" + this.getDistance();
            Console.WriteLine(s);
        }

        public void showMeTranslated()
        {
            string s = Cities.getCityName(Convert.ToInt32(this.sequence[0]));

            for (int i = 1; i < this.sequence.Length; i++) s = s + "-" + Cities.getCityName(Convert.ToInt32(this.sequence[i]));
            s = s + ":" + this.getDistance();

            Console.WriteLine(s);
        }

        public void showMeFull()
        {
            string s = "Vzdálenost: " + getDistance() + "\t";
            s = s + Cities.getCityName(Convert.ToInt32(this.sequence[0]));

            for (int i = 1; i < this.sequence.Length; i++) s = s + "-" + Cities.getCityName(Convert.ToInt32(this.sequence[i]));
            Console.WriteLine(s);
        }

        public string getMeFull()
        {
            string s = "Fitness Invidual: " + getDistance() + "\t";
            s = s + Cities.getCityName(Convert.ToInt32(this.sequence[0]));

            for (int i = 1; i < this.sequence.Length; i++) s = s + "-" + Cities.getCityName(Convert.ToInt32(this.sequence[i]));
            return s;
        }

    }
}
