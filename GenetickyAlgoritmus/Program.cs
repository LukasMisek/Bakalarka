using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Globalization;

namespace GenetickyAlgoritmus
{
    class Program
    {
        
        static void Main(string[] args)
        {
            // Oddelovac tisicu muze byt nastaven jinak na ruznych strojich. Timto nastavim "anglickou klavesnici"
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            InputOutput.start();

            for (int j = 1; j < 51; j++)
            {

                SuperPopulation sp = new SuperPopulation();
                sp.showMe();

                /*
                for (int i = 0; i < 1; i++)
                {
                    Console.WriteLine("pokousim se vylepsit po " + i);
                    sp.improve();
                    Console.WriteLine("vylepseno po " + i);
                    sp.showMe();
                    InputOutput.printResult(sp.getMe(), ("Test" + j + "_Record_Vnejsi0Vnitrni20_Gen" + i), true);
                }
                */

                Console.WriteLine("\n\nDokončeno\n\n");

                SuperInvidual result = sp.GetBest();

                InputOutput.printResult(result.getMe(), "Test" + j + "_Result_Vnejsi0Vnitrni20", true);

                result.showMeFull();

                sp = null;
            }

            /*

            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("pokousim se vylepsit po " + i);
                sp.improve();
                Console.WriteLine("vylepseno po " + i);
                sp.showMe();
                InputOutput.printResult(sp.getMe(), ("Test1_Record_Vnejsi20Vnitrni50_Gen" + i), true);
            }

            Console.WriteLine("\n\nDokončeno\n\n");

            SuperInvidual result = sp.GetBest();

            InputOutput.printResult(result.getMe(), "Test1_Result_Vnejsi20Vnitrni50", true);

            result.showMeFull();

            */
            /*

            SuperInvidual super = new SuperInvidual();



            super.showMeFull();
            Console.ReadLine();

            Console.WriteLine("prelozen: " + super.checkOverload());
            Console.ReadLine();

            SuperInvidual super2 = new SuperInvidual();

            super2.showMeFull();
            Console.ReadLine();

            Console.WriteLine("prelozen: " + super2.checkOverload());
            Console.ReadLine();

            SuperInvidual superNew = new SuperInvidual(super, super2);

            superNew.showMeFull();
            Console.ReadLine();
            */


            /*
            Console.WriteLine("prelozen: " + superNew.checkOverload());
            Console.ReadLine();

            Console.WriteLine("prelozeni ktereho ridice? ");
            int i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("prelozen: " + superNew.checkOverload(i));
            Console.ReadLine();

            Console.WriteLine("veze: " + superNew.getDriverPallets(i));
            Console.ReadLine();

            string[] cesta = new string[superNew.getDriverPallets(i)];
            cesta = superNew.getSequence(i);
            Functions.showArray(cesta);
            Console.ReadLine();
            */

            /*
            Console.WriteLine("poustim algoritmus");
            superNew.startAlgorithm();
            superNew.showMeRoutes();
            Console.ReadLine();

            int num = superNew.getFitness();
            Console.WriteLine("fitness: " + num);
            Console.ReadLine();
            */




            // Controller controller = new Controller();

            /*
            string[] s = { "ab", "cd", "dd", "aa", "ab" };

            Console.WriteLine("pred opravou");
            Functions.showArray(s);

            string[] a = Functions.removeDuplicityStringArray(s);

            Console.WriteLine("po oprave");
            Functions.showArray(a);

            Console.ReadLine();
            */
            //InputOutput.start();



            /*
            Algorithm algoritmus;
            Invidual result;
            
            // Pole jedinců, kteří tvoří objednávku (10 variant objednávek)
            Orders[] vysledek = new Orders[10];

            // Zde je seznam všech variant objednávky
            List<string> orderList = new List<string>();

            // Seznam názvů měst
            //string cities = "!#$%&()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{|}~";
            string cities = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            // Vytvořím 10 skupin objednávek - 10 variant, kudy mohou vést cesty
            for (int i = 0; i < vysledek.Length; i++) vysledek[i] = new Orders(cities);

            // Vytvořím jeden dlouhý seznam cest ze všech objednávek (Každá objednávka má několik cest)
            for (int i = 0; i < vysledek.Length; i++) orderList.AddRange(vysledek[i].getOrdersList());

            // Ukážu všechny Jedince (každou skupinu objednávek)
            for (int i = 0; i < vysledek.Length; i++)
            {
                Console.WriteLine("Jedinec cislo "+i+":");
                vysledek[i].showMatrix();
            }

            Console.ReadLine();

            

            int topVzdalenost = 12345675;
            int delka;
            int index = 0;
            List<int> vzdalenostiVysledek = new List<int>();
            List<string> jedinci = new List<string>();
            List<Invidual> topJedinci = new List<Invidual>();

            List<Invidual> currentJedinci = new List<Invidual>();

            int citac = 0;

            Console.WriteLine("\n\n\nVypocital jsem tyto nejkratsi cesty:");
            for (int i = 0; i < vysledek.Length; i++)
            {
                jedinci = vysledek[i].getOrdersList();
                delka = 0;
                Console.WriteLine("Skupina cislo:" + i);
                for (int j = 0; j < jedinci.Count; j++)
                {
                    if (jedinci[j].Length > 1)
                    {
                        citac++;
                        algoritmus = new Algorithm(jedinci[j], jedinci[j].Length);
                        result = algoritmus.start();
                        delka = delka + result.getDistance();
                        currentJedinci.Add(result);

                        Console.WriteLine(citac + " " + result.getSequence() + "\tVzdalenost: " + result.getDistance());
                    }

                }

                Console.WriteLine("Celkova delka:" + delka + "\n");
                vzdalenostiVysledek.Add(delka);

                if (delka < topVzdalenost)
                {
                    topVzdalenost = delka;
                    topJedinci.Clear();
                    topJedinci.AddRange(currentJedinci);
                    index = i;
                }
                jedinci.Clear();
                currentJedinci.Clear();
            }

            Console.WriteLine("\n\n\nMyslim ze tohle je top skupina: (" + index + ")");
            for (int i = 0; i < topJedinci.Count; i++)
            {
                Console.WriteLine(topJedinci[i].getSequence());
            }
            Console.WriteLine("Celkova vzdalenost: " + topVzdalenost);

            Console.WriteLine("\n\n\nVypocet vzdalenosti po jednom: " + topVzdalenost);

            int counter = 0;

            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].Length > 1)
                {
                    algoritmus = new Algorithm(orderList[i], orderList[i].Length);
                    result = algoritmus.start();
                    Console.WriteLine(counter + " Nejlepší jedinec: " + result.getSequence() + "\tVzdálenost:" + result.getDistance());
                    counter++;
                }
                else
                {
                    Console.WriteLine(counter + " Nejlepší jedinec: " + orderList[i]);
                    counter++;
                }

            }

            Console.ReadLine();
            */

        }


    }
}
