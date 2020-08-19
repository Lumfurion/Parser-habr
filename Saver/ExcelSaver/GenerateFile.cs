using Microsoft.Win32;
using System.Collections.ObjectModel;

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
                ExcelExport.GenerateExcel(ExcelExport.ConvertToDataTable(items), sld.FileName);
            }
           
        }
    }
}
