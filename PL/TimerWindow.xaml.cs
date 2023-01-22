using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.ComponentModel;

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

        public string TimerText
        {
            get { return (string)GetValue(timerTextProperty); }
            set { SetValue(timerTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimerText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty timerTextProperty =
            DependencyProperty.Register("TimerText", typeof(string), typeof(TimerWindow));

        public Tuple<int, BO.Order> Data
        {
            get { return (Tuple<int, BO.Order>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Tuple<int, BO.Order>), typeof(TimerWindow));



        public TimerWindow()
        {
            InitializeComponent();

            Closing += (s, e) => e.Cancel = true;
            stopWatch = new Stopwatch();
            stopWatch.Start();
            isTimerRun = true;

            timerWorker = new BackgroundWorker();
            timerWorker.DoWork += Worker_DoWork;
            timerWorker.ProgressChanged += ;
            timerWorker.WorkerReportsProgress = true;
            timerWorker.RunWorkerAsync();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            CancelEventArgs args = e as CancelEventArgs;
            args!.Cancel = true;
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

        private void setTextInvok(string text)
        {
            if (!CheckAccess())
            {
                Action<string> d = setTextInvok;
                Dispatcher.BeginInvoke(d, new object[] { text });
            }
            else
                timerTextBlock.Text = text;
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    string timer = stopWatch.Elapsed.ToString();
                    TimerText = timer.Substring(0, 8);
                    break;
                case 1:
                    Dispatcher.Invoke(() =>
                    {

                    });
                default:
                    break;
            }
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Report += ChangingOrder;
            Simulator.startsim();
            while(isTimerRun)
            {
                timerWorker.ReportProgress(0);
                Thread.Sleep(1000);
            }
        }

        private void ChangingOrder(object sender, EventArgs e)
        {
            int delay = (e as TupleSimulatorArgs).delay;
            BO.Order order = (e as TupleSimulatorArgs).ord;
            Tuple<int, BO.Order> localTuple = new(delay, order);
            ProgressChangedEventArgs progress = new(1, localTuple);
            Worker_ProgressChanged(sender, progress);
        }

        private void runTimer()
        {
            while (isTimerRun)
            {
                TimerText = stopWatch.Elapsed.ToString();
                TimerText = TimerText.Substring(0, 8);

                setTextInvok(TimerText);
                Thread.Sleep(1000);
            }
        }
    }
}