using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class SuperPopulation
    {
        private List<SuperInvidual> pCurrent;
        private List<SuperInvidual> pNext;

        private SuperInvidual best;

        /// <summary>
        /// Konstruktor
        /// Naplním populaci jedinci
        /// Na začátku jsou jedinci generováni náhodně
        /// </summary>
        public SuperPopulation()
        {
            this.pCurrent = new List<SuperInvidual>();
            this.pNext = new List<SuperInvidual>();
            this.best = null;

            for (int i = 0; i < Algorithm.POPULATION_SIZE; i++) this.pCurrent.Add(new SuperInvidual());

            this.updateBest();
        }

        /// <summary>
        /// Projdu celou populaci a aktializuji nejlepšího jedince.
        /// Tohoto jedince držím v proměnné Invidual best
        /// </summary>
        private void updateBest()
        {
            this.best = null;

            foreach (SuperInvidual iTmp in this.pCurrent)
            {
                if (this.best == null) this.best = iTmp;

                else
                {
                    if (iTmp.getFitness() < this.best.getFitness()) this.best = iTmp;
                }
            }
        }

        /// <summary>
        /// Getter - vrací nejlepšího jedince (Inviduála)
        /// </summary>
        /// <returns>this.best</returns>
        public SuperInvidual GetBest()
        {
            return this.best;
        }

        /// <summary>
        /// Pomocí Console.WriteLine zobrazím jedince v populaci
        /// </summary>
        public void showMe()
        {
            for (int i = 0; i < pCurrent.Count; i++)
            {
                Console.WriteLine("jedinec cislo: " + i);
                pCurrent[i].showMeRoutes();
                Console.WriteLine("Fitness super jedince: " + pCurrent[i].getFitness());
                Console.WriteLine("Pocet ridicu super jedince: " + pCurrent[i].getDriverCount());
            }
        }

        /// <summary>
        /// Výběrová funkce => Turnaj mezi 2 jedinci
        /// Náhodně vyberu 2 jedince z celé populace.
        /// Podle getDistance funkce vyberu lepšího jedince. (Kratší vzdálenost = lepší)
        /// </summary>
        /// <returns>Invidual (Jedinec)</returns>
        private SuperInvidual select()
        {
            var rnd = new Random();

            int i1 = rnd.Next(0, Algorithm.POPULATION_SIZE);
            int i2 = rnd.Next(0, Algorithm.POPULATION_SIZE);
            while (i1 == i2) i2 = rnd.Next(0, Algorithm.POPULATION_SIZE);

            SuperInvidual a = this.pCurrent[i1];
            SuperInvidual b = this.pCurrent[i2];

            if (a.getFitness() < b.getFitness()) return a;

            else return b;

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
            foreach (SuperInvidual iTmp in this.pCurrent)
            {
                SuperInvidual iNew;

                if (rnd.Next(1, 100) < Algorithm.P_CROSSOVER) iNew = new SuperInvidual(this.select(), this.select());

                else iNew = this.select();

                this.pNext.Add(iNew);

            }

            this.pCurrent.Clear();

            foreach (SuperInvidual iTmp in this.pNext) this.pCurrent.Add(iTmp);

            this.pCurrent[0] = this.best;

            this.updateBest();
        }
    }
}
