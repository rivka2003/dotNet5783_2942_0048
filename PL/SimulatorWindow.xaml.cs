using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using BO;

namespace PL;

public class SimulatorWindowData : DependencyObject
{

    public BO.Order? CurrentOrderInLine
    {
        get { return (BO.Order?)GetValue(CurrentOrderInLineProperty); }
        set { SetValue(CurrentOrderInLineProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CurrentOrderInLine.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentOrderInLineProperty =
        DependencyProperty.Register("CurrentOrderInLine", typeof(BO.Order), typeof(SimulatorWindowData));

    public OrderStatus? NextStatus
    {
        get { return (OrderStatus)GetValue(NextStatusProperty); }
        set { SetValue(NextStatusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for NextStatus.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NextStatusProperty =
        DependencyProperty.Register("NextStatus", typeof(OrderStatus?), typeof(SimulatorWindowData));


    public string? StartTime
    {
        get { return (string)GetValue(startTimeProperty); }
        set { SetValue(startTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for startTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty startTimeProperty =
        DependencyProperty.Register("StartTime", typeof(string), typeof(SimulatorWindowData));

    public string? HandleTime
    {
        get { return (string)GetValue(handleTimeProperty); }
        set { SetValue(handleTimeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for handleTime.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty handleTimeProperty =
        DependencyProperty.Register("HandleTime", typeof(string), typeof(SimulatorWindowData));

}

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    // Reference to the business logic layer
    private readonly BlApi.IBl? bl = BlApi.Factory.Get();
    private bool IsRunTimer { get; set; }
    private Stopwatch? Watch { get; set; }

    public string Timer
    {
        get { return (string)GetValue(TimerProperty); }
        set { SetValue(TimerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Timer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TimerProperty =
        DependencyProperty.Register("Timer", typeof(string), typeof(SimulatorWindow));

    public int TimeProgress
    {
        get { return (int)GetValue(TimeProgressProperty); }
        set { SetValue(TimeProgressProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Timer.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TimeProgressProperty =
        DependencyProperty.Register("TimeProgress", typeof(int), typeof(SimulatorWindow));

    public SimulatorWindowData Data
    {
        get { return (SimulatorWindowData)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register("Data", typeof(SimulatorWindowData), typeof(SimulatorWindow));

    private BackgroundWorker _backgroundWorker;

    private event Action _onClosingWindow;

    /// <summary>
    /// Initializes the window and starts the simulation.
    /// </summary>
    /// <param name="_onClosingWindow">Event that is triggered when the window is closed.</param>
    public SimulatorWindow(Action _onClosingWindow)
    {
        InitializeComponent();

        this._onClosingWindow = _onClosingWindow;

        Watch = new Stopwatch();

        Watch.Restart();

        _backgroundWorker = new BackgroundWorker
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        _backgroundWorker.DoWork += _backgroundWorker_DoWork;
        _backgroundWorker.ProgressChanged += _backgroundWorker_ProgressChanged;
        _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        _backgroundWorker.RunWorkerAsync();
    }

    /// <summary>
    /// Handles the RunWorkerCompleted event of the _backgroundWorker control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="RunWorkerCompletedEventArgs"/> instance containing the event data.</param>
    private void _backgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Watch!.Stop();
        Simulator.Simulator.s_StopSimulation -= cancelAsync;
        Simulator.Simulator.s_UpdateSimulation -= reportProgress;
        _onClosingWindow?.Invoke();
        Close();
    }

    /// <summary>
    /// Handles the ProgressChanged event of the _backgroundWorker control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="ProgressChangedEventArgs"/> instance containing the event data.</param>
    private void _backgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {

        if (_backgroundWorker.IsBusy)
        {
            int action = e.ProgressPercentage;

            if (action == 0)
            {
                Timer = (e.UserState as string)!;
            }

            if (action == 1)
            {
                if (Data is null)
                    Data = new();

                (Data.CurrentOrderInLine, Data.NextStatus, Data.StartTime, Data.HandleTime)
               = (e.UserState as Tuple<BO.Order, BO.OrderStatus?, string, string>)!;
            }

            if (action == 2)
                TimeProgress = (int)e.UserState!;
        }
    }

    /// <summary>
    /// The _backgroundWorker_DoWork method is called when the background worker starts working.
    /// It subscribes to the "s_StopSimulation" and "s_UpdateSimulation" events from the Simulator class,
    /// starts the simulation and enters a loop to update the timer.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">The event data.</param>
    private void _backgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.s_StopSimulation += cancelAsync;

        Simulator.Simulator.s_UpdateSimulation += reportProgress;

        Simulator.Simulator.BeginSimulation();

        while (!_backgroundWorker.CancellationPending)
        {
            string timer = Watch!.Elapsed.ToString().Substring(0, 8);
            reportProgress(0, timer);

            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// The cancelAsync method is called when the "s_StopSimulation" event is raised from the Simulator class.
    /// It cancels the background worker.
    /// </summary>
    private void cancelAsync()
    {
        _backgroundWorker.CancelAsync();
    }

    /// <summary>
    /// reportProgress method is used to update the progress of the simulation, it checks if the background worker is busy then it reports the progress percentage and the user state.
    /// </summary>
    private void reportProgress(int progressPercentage, object? userState)
    {
        if (_backgroundWorker.IsBusy)
        {
            _backgroundWorker.ReportProgress(progressPercentage, userState);
        }
    }

    /// <summary>
    /// EndSimulationClick method is used to stop the simulation when the button is clicked, it calls the StopSimulation method from the Simulator class.
    /// </summary>
    private void EndSimulationClick(object sender, RoutedEventArgs e)
    {
        Simulator.Simulator.StopSimulation();
    }
}
