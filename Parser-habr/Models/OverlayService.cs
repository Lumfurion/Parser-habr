using System;
using ToolkitMVVM;
namespace Parser_habr.Models
{
    public class OverlayService : ModelBase
    {
        private static OverlayService _Instance = new OverlayService();

        public static OverlayService GetInstance() => _Instance;

        private OverlayService() { }

        public Action<string> Show { get; set; }

        string text { get; set; } = "";
        public string Text
        {
            get { return text; }
            set 
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }
        public void Close()
        {
            Text = "";
        }

    }
}
