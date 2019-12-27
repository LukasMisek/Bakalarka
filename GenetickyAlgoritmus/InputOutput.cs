using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace GenetickyAlgoritmus
{
    class InputOutput
    {
       private System.Data.DataSet dataSet = new DataSet("allData");
       private System.Data.DataTable tmpTable = new DataTable("tmpTable");

        public InputOutput()
        {
            createTable();
            loadData();
            //showList();
            createFiles();
        }

        private void createTable()
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Obec";
            tmpTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Okres";
            tmpTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Kraj";
            tmpTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "PSČ";
            tmpTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Latitude";
            tmpTable.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = "Longtitude";
            tmpTable.Columns.Add(column);
        }

        private void loadData()
        {
            string address = "..\\..\\..\\..\\" + "Coordinates.txt";

            string[] lines = File.ReadAllLines(@address);
            lines = lines.Skip(1).ToArray();

            DataRow row;
            
            foreach (string line in lines)
            {
                row = tmpTable.NewRow();
                string[] columns = line.Trim().Split(',');
                row[0] = columns[0];
                row[1] = columns[2];
                row[2] = columns[4];
                row[3] = columns[6];
                row[4] = Convert.ToDouble(columns[7].Substring(0, 6)) * 1000;
                row[5] = Convert.ToDouble(columns[8].Substring(0, 6)) * 1000;
                tmpTable.Rows.Add(row);
                
            }

            Console.WriteLine("loading complete");
        }

        private void showList()
        {
            List<string> values = new List<string>();
            values = getUniqueColumnsValues(2);

            foreach (string s in values)
            {
                Console.WriteLine(s);
            }
        }

        private void showData()
        {
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                Console.WriteLine(
                    "|{0,-25}|{1,-25}|{2,-25}|{3,-25}|{4,-25}|{5,-25}",
                    tmpTable.Rows[i][0].ToString(),
                    tmpTable.Rows[i][1].ToString(),
                    tmpTable.Rows[i][2].ToString(),
                    tmpTable.Rows[i][3].ToString(),
                    tmpTable.Rows[i][4].ToString().Substring(0, 6),
                    tmpTable.Rows[i][5].ToString().Substring(0, 6));
            }
        }

        private List<string> getUniqueColumnsValues(int columnIndex)
        {
            List<string> values = new List<string>();

            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                if (!values.Contains(tmpTable.Rows[i][columnIndex].ToString()))
                {
                    values.Add(tmpTable.Rows[i][columnIndex].ToString());
                }
            }

            return values;

        }

        private void createFiles()
        {
            List<string> values = new List<string>();
            values = getUniqueColumnsValues(2);

            foreach (string s in values)
            {
                string address = "..\\..\\..\\..\\" + s + ".txt";
                string write = "Obec\tOkres\tKraj\tPSČ\tX\tY\n";
                System.IO.TextWriter writeFile = new StreamWriter(address);

                for (int i = 0; i < tmpTable.Rows.Count; i++)
                {
                    if (tmpTable.Rows[i][2].ToString() == s)
                    {
                        write = write + tmpTable.Rows[i][0].ToString() + "\t";
                        write = write + tmpTable.Rows[i][1].ToString() + "\t";
                        write = write + tmpTable.Rows[i][2].ToString() + "\t";
                        write = write + tmpTable.Rows[i][3].ToString() + "\t";
                        write = write + tmpTable.Rows[i][5].ToString() + "\t";
                        write = write + tmpTable.Rows[i][4].ToString() + "\n";
                    }
                }
                writeFile.Write(write);
                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
        }
        
    }
    
}
