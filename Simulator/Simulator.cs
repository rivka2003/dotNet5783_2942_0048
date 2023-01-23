using BO;

namespace Simulator;

/// <summary>
/// Simulator class simulates the progress of an order.
/// It contains events to update the simulation and stop the simulation.
/// It also has a private variable to hold the business logic API, a constant sleep time,
/// a flag to stop the simulation, and a random number generator.
/// </summary>
public static class Simulator
{
    /// <summary>
    /// Private variable to hold the business logic API.
    /// </summary>
    private static readonly BlApi.IBl? _bl = BlApi.Factory.Get();
    
    /// <summary>
    /// Constant variable to hold the sleep time for the simulation.
    /// </summary>
    private const int c_sleepTime = 1000;

    /// <summary>
    /// Flag variable to stop the simulation.
    /// </summary>
    private static volatile bool s_flagStopSimulation;

    /// <summary>
    /// Random number generator.
    /// </summary>
    private static readonly Random _random = new();

    /// <summary>
    /// Event that is triggered when the simulation is stopped.
    /// </summary>
    private static event Action? s_stopSimulation;

    /// <summary>
    /// Public property that allows access to the stop simulation event.
    /// </summary>
    public static event Action? s_StopSimulation
    {
        add => s_stopSimulation += value;
        remove => s_stopSimulation -= value;
    }

    /// <summary>
    /// Event that is triggered when the simulation is updated.
    /// </summary>
    private static event Action<int, object?>? s_updateSimulation;

    /// <summary>
    /// Public property that allows access to the update simulation event.
    /// </summary>
    public static event Action<int, object?>? s_UpdateSimulation
    {
        add => s_updateSimulation += value;
        remove => s_updateSimulation -= value;
    }

    /// <summary>
    /// Begins the simulation by starting a new thread.
    /// </summary>
    public static void BeginSimulation() => new Thread(simulationProgress).Start();

    /// <summary>
    /// The main loop of the simulation that simulates the progress of an order.
    /// </summary>
    private static void simulationProgress()
    {
        Delegate[] delegates = s_updateSimulation!.GetInvocationList();
        Order? order;
        s_flagStopSimulation = false;
        while (!s_flagStopSimulation)
        {
            order = _bl!.Order.GettingLatestOrder()!;

            if (order is null)
                Thread.Sleep(c_sleepTime);

            else
            {
                int timeOfTreatment = _random.Next(3, 10);
                int nextStatus = ((int)order.Status! + 1);
                BO.OrderStatus? oRDER_STATUS = (OrderStatus)nextStatus;


                Tuple<Order, BO.OrderStatus?, string, string> items =
                    Tuple.Create(order, oRDER_STATUS, DateTime.Now.ToString(), DateTime.Now.AddSeconds(timeOfTreatment).ToString());

                beginInvoke(delegates, 1, items);

                int sleepTime = timeOfTreatment * 1000;

                new Thread(() => updateTime(timeOfTreatment, delegates)).Start();// for the bar update

                Thread.Sleep(sleepTime);

                order = order.Status == OrderStatus.Confirmed ? _bl.Order.UpdateShipDate(order.ID)
                        : _bl.Order.UpdateDeliveryDate(order.ID);

                items =
                     Tuple.Create(order, (order.Status == OrderStatus.Shipped ? (OrderStatus?)OrderStatus.Delivered : null), "", "");

                beginInvoke(delegates, 1, items);
            }

            Thread.Sleep(c_sleepTime);
        }
    }

    /// <summary>
    /// Updates the time of the simulation and triggers the update simulation event.
    /// </summary>
    /// <param name="sleepTime">The time of the simulation</param>
    /// <param name="delegates">Delegates to invoke the update simulation event</param>
    private static void updateTime(int sleepTime, Delegate[] delegates)
    {
        int jump = Convert.ToInt32(100 / sleepTime);

        for (int i = jump; i <= 100; i += jump)
        {
            if (!s_flagStopSimulation)
            {
                beginInvoke(delegates, 2, i);
                Thread.Sleep(1000);
            }
            else
                break;
        }
    }

    /// <summary>
    /// Stops the simulation by setting the stop flag to true and triggering the stop simulation event.
    /// </summary>
    public static void StopSimulation()
    {
        s_flagStopSimulation = true;
        s_stopSimulation?.Invoke();
    }

    /// <summary>
    /// Invokes the update simulation event.
    /// </summary>
    /// <param name="delegates">Delegates to invoke the update simulation event</param>
    /// <param name="objects">Objects to pass to the event</param>
    private static void beginInvoke(Delegate[]? delegates, params object[]? objects)
    {
        foreach (var @delegate in delegates!)
            @delegate?.DynamicInvoke(objects);
    }
}