using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;

namespace Saver.ExcelSaver
{
    public static class GenerateFile
    {
        public static void Go<T>(ObservableCollection<T> items)
        {
          
            SaveFileDialog sld = new SaveFileDialog();
            sld.Filter = "xlsx files (*.xlsx)|*.xlsx";
            if (sld.ShowDialog() == true)
            {
                ExcelExport.GenerateExcel(ConvertToDataTable(items), sld.FileName);
            }
        }

        static DataTable ConvertToDataTable<T>(ObservableCollection<T> models)// T : Generic Class
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties            
            // Adding Column to our datatable
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            // Adding Row
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = Props[i].GetValue(item, null);
                }
                // Finally add value to datatable  
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

    }
}
