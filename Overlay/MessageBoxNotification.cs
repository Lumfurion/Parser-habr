
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;


namespace Overlay
{
    public class MessageBoxNotification
    {
        private static Settings settings { get; set; } = new Settings();
        public static Settings Settings
        {
            get { return settings; }
        }

        public MessageBoxNotification() { }



        private static void Set(string text, int time)
        {
            settings.Text = text;
            settings.Time = time;
        }



        public static string Show(string text, int time)
        {
            Set(text, time);
            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                ShowBlurEffectAllWindow();
                MainWindow alarm = new MainWindow();
                alarm.Show();
                var seconds = time * 1000;
                await Task.Delay(seconds);
                StopBlurEffectAllWindow();
                alarm.Close();
            });
            return "1";
        }

        public static string ShowDialog(string text, int time)
        {
            Set(text, time);
            MainWindow alarm = new MainWindow();
            _ = Close(time, alarm);
            alarm.ShowDialog();
            return "1";
        }

        public static async Task Close(int millisecondstoseconds, MainWindow window)
        {
            var seconds = millisecondstoseconds * 1000;
            await Task.Delay(seconds);
            window.Close();
        }


        /// <summary>
        /// Эффект растрового изображения, размывающий целевую текстуру.
        /// Данном случае это будет размывать окно.
        /// </summary>
        private static BlurEffect Blur = new BlurEffect();
        static void ShowBlurEffectAllWindow()
        {
            Blur.Radius = 20;
            foreach (Window window in Application.Current.Windows)
            {
                window.Effect = Blur;
            }
        }

        static void StopBlurEffectAllWindow()
        {
            Blur.Radius = 0;
        }

    }
}
