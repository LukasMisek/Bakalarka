using System;
using System.Collections.Generic;
using System.Text;

namespace GenetickyAlgoritmus
{
    /// <summary>
    /// Třída populace obsahuje konstruktor populace, výběr jedinců uvnitř populace (nejlepší a silnější) a zobrazení pomocí Console.WriteLine
    /// Populace = List jedinců (Třída Invidual)
    /// </summary>
    public class PopulationOrder
    {

        /// <summary>
        /// Proměnné pro uložení populací
        /// </summary>
        private List<InvidualOrder> pCurrent;
        private List<InvidualOrder> pNext;

        private InvidualOrder best;

        /// <summary>
        /// Inviduál = Jedinec
        /// </summary>
       // private InvidualOrder best;

        /// <summary>
        /// Konstruktor
        /// Naplním populaci jedinci
        /// Na začátku jsou jedinci generováni náhodně
        /// </summary>
        public PopulationOrder()
        {
            this.pCurrent = new List<InvidualOrder>();
            this.pNext = new List<InvidualOrder>();
            this.best = null;

            for (int i = 0; i < Algorithm.POPULATION_SIZE; i++) this.pCurrent.Add(new InvidualOrder());

            runAlgorithm();
            this.updateBest();
        }

        public void runAlgorithm()
        {
            foreach (InvidualOrder invidualOrder in pCurrent)
            {
                invidualOrder.calculateRoutes();
            }
        }

        public void runAlgorithm(int i)
        {
            Console.WriteLine("poustim algoritmus pro č.{0}\n", i);
            pCurrent[i].calculateRoutes();
        }

        public void showResult()
        {
            for (int i = 0; i < pCurrent.Count; i++)
            {
                Console.WriteLine("\n\nVýsledek nadjedinec č.{0}\n", i);
                pCurrent[i].showSortedOrders();
            }
        }

        public void showResultTranslated()
        {
            for (int i = 0; i < pCurrent.Count; i++)
            {
                Console.WriteLine("\n\nVýsledek nadjedinec č.{0}\n", i);
                pCurrent[i].showSortedOrdersTranslated();
            }
        }

        public void showResultFull()
        {
            for (int i = 0; i < pCurrent.Count; i++)
            {
                Console.WriteLine("\n\nVýsledek nadjedinec č.{0}\n", i);
                pCurrent[i].showSortedOrdersFull();
            }
        }

        public void showResult(int i)
        {
            Console.WriteLine("\n\nVýsledek nadjedinec č.{0}\n", i);
            pCurrent[i].showSortedOrders();
        }

        public void showResultTranslated(int i)
        {
            Console.WriteLine("\n\nVýsledek nadjedinec č.{0}\n", i);
            pCurrent[i].showSortedOrdersTranslated();
        }

        public void showResultFull(int i)
        {
            Console.WriteLine("\n\nVýsledek nadjedinec č.{0}\n", i);
            pCurrent[i].showSortedOrdersFull();
        }

        public void translateResult(int i)
        {
            Console.WriteLine("\n\nPřekládám nadjedince č.{0}\n", i);
            pCurrent[i].translateRoutes();
        }

        public void translateResult()
        {
            for (int i = 0; i < pCurrent.Count; i++)
            {
                Console.WriteLine("\n\nPřekládám nadjedince č.{0}\n", i);
                pCurrent[i].translateRoutes();
            }
        }

        public double getFitness()
        {
            double fitness = 0;
            for (int i = 0; i < pCurrent.Count; i++)
            {
                fitness = fitness + pCurrent[i].getFitness();
            }
            return fitness;
        }

        public double getFitness(int i)
        {
            return pCurrent[i].getFitness();
        }

        public void updateBest()
        {
            this.best = null;

            foreach (InvidualOrder iTmp in this.pCurrent)
            {
                if (this.best == null) this.best = iTmp;

                else
                {
                    if (iTmp.getFitness() < this.best.getFitness()) this.best = iTmp;
                }
            }
        }

        public InvidualOrder GetBest()
        {
            return this.best;
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
            foreach (InvidualOrder iTmp in this.pCurrent)
            {
                InvidualOrder iNew;

                if (rnd.Next(1, 100) < Algorithm.P_CROSSOVER) iNew = new InvidualOrder(this.select(), this.select());

                else iNew = this.select();
                    
                /*
                Console.WriteLine("tento jedinec byl vybran:");
                iNew.showMeId();
                Console.WriteLine("toto je jeho fitness:" + iNew.getFitness());
                Console.ReadLine();
                */

                //if (rnd.Next(1, 100) < Algorithm.P_MUTATION) iNew.mutate();

                this.pNext.Add(iNew);
                

            }
            this.pCurrent.Clear();

            foreach (InvidualOrder iTmp in this.pNext) this.pCurrent.Add(iTmp);

            this.runAlgorithm();
            this.updateBest();
        }

        /// <summary>
        /// Výběrová funkce => Turnaj mezi 2 jedinci
        /// Náhodně vyberu 2 jedince z celé populace.
        /// Podle getDistance funkce vyberu lepšího jedince. (Kratší vzdálenost = lepší)
        /// </summary>
        /// <returns>Invidual (Jedinec)</returns>
        private InvidualOrder select()
        {
            var rnd = new Random();

            int i1 = rnd.Next(0, Algorithm.POPULATION_SIZE);
            int i2 = rnd.Next(0, Algorithm.POPULATION_SIZE);
            while (i1 == i2) i2 = rnd.Next(0, Algorithm.POPULATION_SIZE);

            InvidualOrder a = this.pCurrent[i1];
            InvidualOrder b = this.pCurrent[i2];

            if (a.getFitness() < b.getFitness()) return a;

            else return b;

        }

        /// <summary>
        /// Projdu celou populaci a aktializuji nejlepšího jedince.
        /// Tohoto jedince držím v proměnné Invidual best
        /// </summary>
        /// 
        /*private void updateBest()
        {
            this.best = null;

            foreach (InvidualOrder iTmp in this.pCurrent)
            {
                if (this.best == null) this.best = iTmp;

                else
                {
                    if (iTmp.getDistance() < this.best.getDistance()) this.best = iTmp;
                }
            }
        }*/

        /// <summary>
        /// Getter - vrací nejlepšího jedince (Inviduála)
        /// </summary>
        /// <returns>this.best</returns>
        /*public InvidualOrder GetBest()
        {
            return this.best;
        }*/

        /// <summary>
        /// Vrátí nejlepšího jedince a jeho vzdálenostjako string
        /// </summary>
        /// <returns></returns>
     /*   public string getBestString()
        {
            return "(" + this.best.getDistance() + ")" + "\t" + this.best.getSequence();
        }*/

        /// <summary>
        /// Pomocí Console.WriteLine zobrazím jedince v populaci
        /// </summary>
     /*   public void showMe()
        {
            for (int i = 0; i < pCurrent.Count - 1; i = i + 2)
            {
                Console.WriteLine(
                    "Invidual" + (i) + "(" + pCurrent[i].getDistance() + ")" + "\t" + pCurrent[i].showSequence() + "\t" +
                    "Invidual" + (i + 2) + "(" + pCurrent[i + 1].getDistance() + ")" + "\t" + pCurrent[i + 1].showSequence());
            }
        }*/

    }
}
