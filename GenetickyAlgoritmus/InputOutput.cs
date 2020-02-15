using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace GenetickyAlgoritmus
{
    /// <summary>
    /// Třída slouží k nahrání vstupních dat
    /// </summary>
    class InputOutput
    {
        /// <summary>
        /// Volám k nahrání defaultních hodnot
        /// Vytvořím dočasnou tabulku
        /// Načtu do dočasné tabulky všechny data
        /// Vytvořím jednotlivé soubory pro každý Kraj
        /// </summary> 
        public static void start()
        {
            createTable();
            loadData();
            createFiles();
        }

        /// <summary>
        /// Smaže tabulku
        /// </summary>
        public static void deleteTable()
        {
            Controller.allCitiesTable.Clear();
        }

        /// <summary>
        /// Vytvoří dataTable, ve kterých budu držet data
        /// </summary>
        public static void createTable()
        {
            createAllcitiesTable();
            createActiveOrdersTable();
        }

        /// <summary>
        /// Načte data ze souboru a naplní dataTable
        /// </summary>
        public static void loadData()
        {
            loadDataAllCities();
            loadDataActiveOrders();
        }

        /// <summary>
        /// Vytvori tabulku, ve které budu držet data
        /// </summary>
        public static void createAllcitiesTable()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Id";
            Controller.allCitiesTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Obec";
            Controller.allCitiesTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Okres";
            Controller.allCitiesTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Kraj";
            Controller.allCitiesTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PSČ";
            Controller.allCitiesTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "X";
            Controller.allCitiesTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Y";
            Controller.allCitiesTable.Columns.Add(column);
        }

        /// <summary>
        /// Vytvori tabulku, ve které budu držet data
        /// </summary>
        public static void createActiveOrdersTable()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "CustomerId";
            Controller.activeOrdersTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Obec";
            Controller.activeOrdersTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Okres";
            Controller.activeOrdersTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Kraj";
            Controller.activeOrdersTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "Count";
            Controller.activeOrdersTable.Columns.Add(column);
        }

        /// <summary>
        /// Nactu surová data ze souboru a uložím je do tabulky AllCitiesTable
        /// </summary>
        public static void loadDataAllCities()
        {
            string[] lines = File.ReadAllLines(@Controller.pathAllCitiesTable);
            lines = lines.Skip(1).ToArray();
            int i = 0;
            DataRow row;
            foreach (string line in lines)
            {
                row = Controller.allCitiesTable.NewRow();
                string[] columns = line.Trim().Split(',');
                row[0] = i;
                row[1] = columns[0];
                row[2] = columns[2];
                row[3] = columns[4];
                row[4] = columns[6];
                row[5] = Convert.ToDouble(columns[7].ToString());
                row[6] = Convert.ToDouble(columns[8].ToString());
                Controller.allCitiesTable.Rows.Add(row);
                i++;                
            }
        }

        /// <summary>
        /// Nactu surová data ze souboru a uložím je do tabulky ActiveOrdersTable
        /// </summary>
        public static void loadDataActiveOrders()
        {
            string[] lines = File.ReadAllLines(@Controller.pathActiveOrdersTable);
            lines = lines.Skip(1).ToArray();
            int i = 0;
            DataRow row;
            foreach (string line in lines)
            {
                row = Controller.activeOrdersTable.NewRow();
                string[] columns = line.Trim().Split('\t');
                row[0] = columns[0];
                row[1] = columns[1];
                row[2] = columns[2];
                row[3] = columns[3];
                row[4] = columns[4];
                Controller.activeOrdersTable.Rows.Add(row);
                i++;
            }
            
        }

        /// <summary>
        /// Vytvoří soubory pro každý kraj
        /// Metoda si stáhne seznam unikátních hodnot ve sloupci Kraj - index 2
        /// Soubory uloží do kořenového adresáře (4 úrovně nad Debug adresářem)
        /// </summary>
        public static void createFiles()
        {
            List<string> values = new List<string>();
            values = getUniqueColumnsValuesAllcities(3);

            foreach (string s in values)
            {
                string address = Controller.pathDistinctAreasTable + s + ".txt";
                string write = "Id\tObec\tOkres\tKraj\tPSČ\tX\tY\n";
                System.IO.TextWriter writeFile = new StreamWriter(address);

                for (int i = 0; i < Controller.allCitiesTable.Rows.Count; i++)
                {
                    if (Controller.allCitiesTable.Rows[i][3].ToString() == s)
                    {
                        write = write + Controller.allCitiesTable.Rows[i][0].ToString() + "\t";
                        write = write + Controller.allCitiesTable.Rows[i][1].ToString() + "\t";
                        write = write + Controller.allCitiesTable.Rows[i][2].ToString() + "\t";
                        write = write + Controller.allCitiesTable.Rows[i][3].ToString() + "\t";
                        write = write + Controller.allCitiesTable.Rows[i][4].ToString() + "\t";
                        write = write + Controller.allCitiesTable.Rows[i][6].ToString() + "\t";
                        write = write + Controller.allCitiesTable.Rows[i][5].ToString() + "\n";
                    }
                }
                writeFile.Write(write);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
        }

        public static void printList(List<string> list)
        {
            string address = Controller.pathTest;
            string s_out = "";

            foreach (string s in list) s_out = s_out + s + "\t";

            System.IO.TextWriter writeFile = new StreamWriter(address);

            writeFile.Write(s_out);
            writeFile.Flush();
            writeFile.Close();
            writeFile = null;

        }

        public static void printList(List<int> list)
        {
            string address = Controller.pathTest;
            string s_out = "";

            foreach (int s in list) s_out = s_out + s +"\n";

            System.IO.TextWriter writeFile = new StreamWriter(address);

            writeFile.Write(s_out);
            writeFile.Flush();
            writeFile.Close();
            writeFile = null;

        }

        public static void printString(string s)
        {
            string address = Controller.pathTest;

            System.IO.TextWriter writeFile = new StreamWriter(address);

            writeFile.Write(s);
            writeFile.Flush();
            writeFile.Close();
            writeFile = null;

        }

        /// <summary>
        /// Vrátím unikátní hodnoty ve sloupci jako List. 
        /// Metoda přebírá číslo, které označuje index sloupce
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns>List<string> values</returns>
        public static List<string> getUniqueColumnsValuesAllcities(int columnIndex)
        {
            List<string> values = new List<string>();

            for (int i = 0; i < Controller.allCitiesTable.Rows.Count; i++)
                if (!values.Contains(Controller.allCitiesTable.Rows[i][columnIndex].ToString()))
                    values.Add(Controller.allCitiesTable.Rows[i][columnIndex].ToString());

            return values;

        }

        /// <summary>
        /// Vrátím unikátní hodnoty ve sloupci jako List. 
        /// Metoda přebírá číslo, které označuje index sloupce
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns>List<string> values</returns>
        public static List<string> getUniqueColumnsValuesActiveOrders(int columnIndex)
        {
            List<string> values = new List<string>();

            for (int i = 0; i < Controller.activeOrdersTable.Rows.Count; i++)
                if (!values.Contains(Controller.activeOrdersTable.Rows[i][columnIndex].ToString()))
                {
                    values.Add(Controller.activeOrdersTable.Rows[i][columnIndex].ToString());
                }                    

            return values;

        }


    }

}
