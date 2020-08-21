using Parser_habr.Core.Model;
using Parser_habr.Core.Command;
using ParserBase;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Saver.ExcelSaver;
using Saver.XmlSaver;
using System.Linq;

namespace Parser_habr.ViewModels
{
    
    class MainWindowViewModel:ModelBase
    {
        readonly ParserWorker<Article[]> Parser;
        public ObservableCollection<Article> Articles { get; set; }
        int startPage { get; set; } = 1;
        public int StartPage
        {
            get { return startPage; }
            set 
            {  
                startPage = value;
                OnPropertyChanged("StartPage");
            }
        }
       
        int endPage { get; set; } = 1;
        public int EndPage
        {
            get { return endPage; }
            set
            {
                endPage = value;
                OnPropertyChanged("EndPage");
            }
        }
        public MainWindowViewModel()
        {
            Articles = new ObservableCollection<Article>();
            Parser = new ParserWorker<Article[]>(new HabrParser());
            //По заврешению работы парсера будет появляться уведомляющее окно.
            Parser.OnComplited += Parser_OnComplited;
            //Заполняем наш listBox заголовками
            Parser.OnNewData += Parser_OnNewData;
        }
        public void Parser_OnComplited(object o) { MessageBox.Show("Работа завершена!"); }
        public void Parser_OnNewData(object o, Article[] item) 
        {
            foreach (var i in item)
            {
                Articles.Add(new Article(i.Title, i.Text, i.Image));
            }
        }

        private ICommand goParse;
        public ICommand GoParse
        {
            get
            {
                if (goParse == null)
                {
                    goParse = new RelayCommand(()=> 
                    {
                        Articles.Clear();
                        Parser.Settings = new HabrSettings(StartPage,EndPage);
                        Parser.Start();
                    });
                }
                return goParse;
            }
        }

        private ICommand stopParse;
        public ICommand StopParse
        {
            get
            {
                if (stopParse == null)
                {
                    stopParse = new RelayCommand(() => { Parser.Stop(); }, () => IsEmpty());
                }
                return stopParse;
            }
        }


        private ICommand saveExcel;
        public ICommand SaveExcel
        {
            get
            {
                if (saveExcel == null)
                {
                    saveExcel = new RelayCommand(() => { GenerateFile.Go(Articles); }, () => IsEmpty());
                }
                return saveExcel;
            }
        }

        private ICommand saveXml;
        public ICommand SaveXml
        {
            get
            {
                if (saveXml == null)
                {
                    saveXml = new RelayCommand(() => 
                    {
                        var hub = Articles.ToList();
                        Genarate.Go(Parser_habr.Articles.Craete(hub)); 
                    
                    }, () => IsEmpty());
                }
                return saveXml;
            }
        }

        private ICommand saveDataBase;
        public ICommand SaveDataBase
        {
            get
            {
                if (saveDataBase == null)
                {
                    saveDataBase = new RelayCommand(() =>
                    {
                        var art = Articles.ToList();
                        ArticleContext.Save(art);

                    }, () => IsEmpty());
                }
                return saveDataBase;
            }
        }


        private bool IsEmpty()=> (Articles.Count > 0) ? true : false;

    }
}
