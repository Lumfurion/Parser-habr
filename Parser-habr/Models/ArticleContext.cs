using Microsoft.Win32;
using Overlay;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;


namespace Parser_habr.Models
{
    public class ArticleContext : DbContext
    {
        public ArticleContext(string connectionString) : base(connectionString) { }
        public DbSet<Article> Articles { get; set; }
       
        public  static   void Save<T>(List<T> item) where T : class
        {
            OverlayService.GetInstance().Show = (str) =>//инициализация оверлея
            {
                OverlayService.GetInstance().Text = str;
            };

            Task.Run(() =>
            {
                SaveFileDialog sld = new SaveFileDialog();
                sld.Filter = "MDF files (*.MDF)|*.MDF";
                if (sld.ShowDialog() == true)
                {
                    OverlayService.GetInstance().Show($"Идет сохранение в базу данных...");
                    string filename = sld.FileName;
                    var builder = new SqlConnectionStringBuilder();
                    builder.DataSource = @"(localdb)\MSSQLLocalDB";
                    builder.AttachDBFilename = filename;
                    builder.InitialCatalog = Path.GetFileNameWithoutExtension(filename);
                    builder.IntegratedSecurity = true;
                    string connectionString = builder.ToString();
                    using (var db = new ArticleContext(connectionString))
                    {
                        db.Set<T>().AddRange(item);
                        db.SaveChanges();//Сохраняем все изменения, сделанные в базовой базе данных.
                        OverlayService.GetInstance().Close();
                    }

                }
            });
            
        }

    }
}
