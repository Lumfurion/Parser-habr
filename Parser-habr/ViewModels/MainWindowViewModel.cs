using Parser_habr.Core.Model;
using Parser_habr.Core.Command;
using ParserBase;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Saver.ExcelSaver;

namespace Parser_habr.ViewModels
{
    
    class MainWindowViewModel:ModelBase
    {
        readonly ParserWorker<HabrData[]> Parser;
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

        public ObservableCollection<HabrData> List { get; set; }

        public MainWindowViewModel()
        {
            List = new ObservableCollection<HabrData>();
            Parser = new ParserWorker<HabrData[]>(new HabrParser());
            //По заврешению работы парсера будет появляться уведомляющее окно.
            Parser.OnComplited += Parser_OnComplited;
            //Заполняем наш listBox заголовками
            Parser.OnNewData += Parser_OnNewData;
        }
        public void Parser_OnComplited(object o) { MessageBox.Show("Работа завершена!"); }
        public void Parser_OnNewData(object o, HabrData[] item) 
        {
            foreach (var i in item)
            {
                List.Add(new HabrData(i.Title, i.Text, i.Image));
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
                        List.Clear();
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
                    stopParse = new RelayCommand(() =>{Parser.Stop();});
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
                    saveExcel = new RelayCommand(() => { GenerateFile.Go(List); }, () => List != null);
                }
                return saveExcel;
            }
        }




    }
}
