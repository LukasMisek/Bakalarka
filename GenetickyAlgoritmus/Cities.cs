using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GenetickyAlgoritmus
{
    public class Cities
    {
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

            double x1 = Convert.ToDouble(Controller.allCitiesTable.Rows[index1]["X"].ToString());
            double x2 = Convert.ToDouble(Controller.allCitiesTable.Rows[index2]["X"].ToString());
            double y1 = Convert.ToDouble(Controller.allCitiesTable.Rows[index1]["Y"].ToString());
            double y2 = Convert.ToDouble(Controller.allCitiesTable.Rows[index2]["Y"].ToString());

            return Math.Round(Math.Pow(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2), 0.5), 4);

        }

        /// <summary>
        /// Vrátí vzdálenost mezi 2 městy. Vstupem jsou index měst
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns></returns>
        public static double getDistance(int city1, int city2)
        {
            double x1 = Convert.ToDouble(Controller.allCitiesTable.Rows[city1]["X"].ToString());
            double x2 = Convert.ToDouble(Controller.allCitiesTable.Rows[city2]["X"].ToString());
            double y1 = Convert.ToDouble(Controller.allCitiesTable.Rows[city1]["Y"].ToString());
            double y2 = Convert.ToDouble(Controller.allCitiesTable.Rows[city2]["Y"].ToString());

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
                {
                    coordinate = i;
                    break;
                }

            return coordinate;
        }     
        
        /// <summary>
        /// Vrátí ID města, když dodám název města
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static string getId(string city)
        {
            int index = getIndex(city);
            return Controller.allCitiesTable.Rows[index]["Id"].ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city1"></param>
        /// <param name="city2"></param>
        /// <returns></returns>
        public static double getDistanceId(string city1, string city2)
        {
            int index1 = getIndexId(city1);
            int index2 = getIndexId(city2);

            int x1 = Convert.ToInt32(Controller.allCitiesTable.Rows[index1]["X"].ToString());
            int x2 = Convert.ToInt32(Controller.allCitiesTable.Rows[index2]["X"].ToString());
            int y1 = Convert.ToInt32(Controller.allCitiesTable.Rows[index1]["Y"].ToString());
            int y2 = Convert.ToInt32(Controller.allCitiesTable.Rows[index2]["Y"].ToString());

            return Math.Round(Math.Pow(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2), 0.5), 4);

        }

        /// <summary>
        /// Vrátí index města podle názvu
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        public static int getIndexId(string city)
        {
            int coordinate = 0;
            for (int i = 0; i < Controller.allCitiesTable.Rows.Count; i++)
                if (Controller.allCitiesTable.Rows[i]["Id"].ToString() == city)
                    coordinate = i;

            return coordinate;
        }

        /// <summary>
        /// Vrátí název města podle indexu
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string getCityName(int i)
        {
            return Controller.allCitiesTable.Rows[i]["Obec"].ToString();
        }

        public static int getCityCount(int index)
        {
            string city = "";
            for (int i = 0; i < Controller.allCitiesTable.Rows.Count; i++)
            {
                if(Convert.ToInt32(Controller.allCitiesTable.Rows[i]["Id"].ToString()) == index)
                {
                    city = Controller.allCitiesTable.Rows[i]["Obec"].ToString();
                    break;
                }
            }

            int count = 0;
            for (int i = 0; i < Controller.activeOrdersTable.Rows.Count; i++)
            {
                if (Controller.activeOrdersTable.Rows[i]["Obec"].ToString() == city)
                {
                    count = Convert.ToInt32(Controller.activeOrdersTable.Rows[i]["Count"].ToString());
                    break;
                }
            }
            return count;
        }

    }
}
