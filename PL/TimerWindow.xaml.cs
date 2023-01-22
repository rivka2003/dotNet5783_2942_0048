using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace PL
{
    /// <summary>
    /// Interaction logic for TimerWindow.xaml
    /// </summary>
    public partial class TimerWindow : Window
    {
        private Stopwatch stopWatch;
        private BackgroundWorker timerWorker;
        private bool isTimerRun;

        public string timerText
        {
            get { return (string)GetValue(timerTextProperty); }
            set { SetValue(timerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for timerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timerTextProperty =
            DependencyProperty.Register("timerText", typeof(string), typeof(TimerWindow));

        public TimerWindow()
        {
            InitializeComponent();

            Closing += (s, e) => e.Cancel = true;
            stopWatch = new Stopwatch();
            stopWatch.Restart();
            isTimerRun = true;

            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += runTimer;
            timerWorker.Start();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
                stopWatch.Stop();
                isTimerRun = false;
            }
            Close();
        }

        void setTextInvok(string text)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTextInvok;
                Dispatcher.BeginInvoke(d, new object[] { text });
            }
            else
                timerTextBlock.Text = text;
        }

        private void runTimer()
        {
            Simulator.Report += orderChange;
            while (isTimerRun)
            {
                timerText = stopWatch.Elapsed.ToString();
                timerText = timerText.Substring(0, 8);

                setTextInvok(timerText);
                Thread.Sleep(1000);
            }
        }
    }
}