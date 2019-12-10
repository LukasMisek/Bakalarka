using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    /// <summary>
    /// Třída populace obsahuje konstruktor populace, výběr jedinců uvnitř populace (nejlepší a silnější) a zobrazení pomocí Console.WriteLine
    /// Populace = List jedinců (Třída Invidual)
    /// </summary>
    public class Population
    {
        /// <summary>
        /// Proměnné pro uložení populací
        /// </summary>
        private List<Invidual> pCurrent;
        private List<Invidual> pNext;

        /// <summary>
        /// Inviduál = Jedinec
        /// </summary>
        private Invidual best;

        /// <summary>
        /// Konstruktor
        /// Naplním populaci jedinci
        /// Na začátku jsou jedinci generováni náhodně
        /// </summary>
        public Population()
        {
            this.pCurrent = new List<Invidual>();
            this.pNext = new List<Invidual>();
            this.best = null;

            for (int i = 0; i < Algorithm.POPULATION_SIZE; i++) this.pCurrent.Add(new Invidual());

            this.updateBest();
        }

        /// <summary>
        /// Zlepšující funkce
        /// S 80% šancí (nebo Algorithm.P_CROSSOVER) aplikuji operátor křížení
        /// S 20% šancí (nebo Algorithm.P_MUTATION) apliukuji operátor mutace
        /// Aktualizuji populaci po křížení a mutaci
        /// </summary>
        public void improve()
        {
            var rnd = new Random();

            this.pNext.Clear();
            foreach (Invidual iTmp in this.pCurrent)
            {
                Invidual iNew;
               
                
                if (rnd.Next(1, 100) < Algorithm.P_CROSSOVER) iNew = new Invidual(this.select(), this.select());
  
                else iNew = this.select();
                

                if (rnd.Next(1, 100) < Algorithm.P_MUTATION) iNew.mutate();

                this.pNext.Add(iNew);


            }

            this.pCurrent.Clear();

            foreach(Invidual iTmp in this.pNext) this.pCurrent.Add(iTmp);

            this.updateBest();
        }

        /// <summary>
        /// Výběrová funkce => Turnaj mezi 2 jedinci
        /// Náhodně vyberu 2 jedince z celé populace.
        /// Podle getDistance funkce vyberu lepšího jedince. (Kratší vzdálenost = lepší)
        /// </summary>
        /// <returns>Invidual (Jedinec)</returns>
        private Invidual select()
        {
            var rnd = new Random();

            int i1 = rnd.Next(0, Algorithm.POPULATION_SIZE);
            int i2 = rnd.Next(0, Algorithm.POPULATION_SIZE);
            while (i1 == i2) i2 = rnd.Next(0, Algorithm.POPULATION_SIZE);

            Invidual a = this.pCurrent[i1];
            Invidual b = this.pCurrent[i2];

            if (a.getDistance() < b.getDistance()) return a;

            else return b;

        }

        /// <summary>
        /// Projdu celou populaci a aktializuji nejlepšího jedince.
        /// Tohoto jedince držím v proměnné Invidual best
        /// </summary>
        private void updateBest()
        {
            this.best = null;

            foreach (Invidual iTmp in this.pCurrent)
            {
                if (this.best == null) this.best = iTmp;

                else
                {
                    if (iTmp.getDistance() < this.best.getDistance()) this.best = iTmp;
                }
            }
        }
        
        /// <summary>
        /// Getter - vrací nejlepšího jedince (Inviduála)
        /// </summary>
        /// <returns>this.best</returns>
        public Invidual GetBest()
        {
            return this.best;
        }

        public string getBestString()
        {
            return "(" + best.getDistance() + ")" + "\t" + best.getSequence();
        }

        /// <summary>
        /// Pomocí Console.WriteLine zobrazím jedince v populaci
        /// </summary>
        public void showMe()
        {
            /*
            for (int i = 0; i < pCurrent.Count; i = i++) Console.WriteLine("Invidual " + i + ": " + pCurrent[i].getSequence());
            */

            // Zobrazím jedince v populaci (1 řádek 1 jedinec)
            for (int i = 0; i < pCurrent.Count-2; i = i + 3)
            {
                Console.WriteLine(
                    "Invidual" + (i) + "(" + pCurrent[i].getDistance() + ")" + "\t" + pCurrent[i].getSequence() + "\t" +
                    "Invidual" + (i+1) + "(" + pCurrent[i+1].getDistance() + ")" + "\t" + pCurrent[i+1].getSequence() + "\t" +
                    "Invidual" + (i+2) + "(" + pCurrent[i+2].getDistance() + ")" + "\t" + pCurrent[i+2].getSequence());
        }
            
            /*
            // Zobrazím jedince v poopulaci (1 řádek 4 jedinci)
            for (int i = 0; i < pCurrent.Count; i = i + 4)
            {
                Console.WriteLine(
                    "Invidual" + i + "(" + pCurrent[i].getDuplicity() + ")" + "\t" + pCurrent[i].getSequence() +
                    "\t Invidual" + (i + 1) + "(" + pCurrent[i + 1].getDuplicity() + ")" + "\t" + pCurrent[i + 1].getSequence() +
                    "\t Invidual" + (i + 2) + "(" + pCurrent[i + 2].getDuplicity() + ")" + "\t" + pCurrent[i + 2].getSequence() +
                    "\t Invidual" + (i + 3) + "(" + pCurrent[i + 3].getDuplicity() + ")" + "\t" + pCurrent[i + 3].getSequence());

            }*/
        }

    }
}
