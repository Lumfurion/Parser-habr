using ToolkitMVVM;

namespace Overlay
{
    public class Settings : ModelBase
    {
        string text { get; set; }
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        int time { get; set; }
        public int Time
        {
            get { return time; }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }

    }
}
