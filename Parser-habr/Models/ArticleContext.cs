using Microsoft.Win32;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;

namespace Parser_habr.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(string connectionString) : base(connectionString) { }
        public DbSet<Article> Articles { get; set; }
       
        public  static   void Save<T>(List<T> item) where T : class
        {
           
            Task.Run(() =>
            {
                SaveFileDialog sld = new SaveFileDialog();
                sld.Filter = "MDF files (*.MDF)|*.MDF";
                if (sld.ShowDialog() == true)
                {
                    MessageBox.Show("Подождите идет сохранение в базу данных", "Подождите идет", MessageBoxButton.OK, MessageBoxImage.Warning);
                    string filename = sld.FileName;
                    var builder = new SqlConnectionStringBuilder();
                    builder.DataSource = @"(localdb)\MSSQLLocalDB";
                    builder.AttachDBFilename = filename;
                    builder.IntegratedSecurity = true;
                    string connectionString = builder.ToString();
                    using (var db = new ArticleContext(connectionString))
                    {
                        db.Set<T>().AddRange(item);
                        db.SaveChanges();//Сохраняем все изменения, сделанные в базовой базе данных.
                        MessageBox.Show("Данные сохранение в базу данных");
                    }

                }
            });
            
        }

    }
}
