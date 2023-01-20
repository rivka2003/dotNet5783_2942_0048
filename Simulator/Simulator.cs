
namespace Simulator
{
    enum SimulationProgress { UpdateDone = 1, Done }
    public static class Simulator
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        private const int seconds = 1000;
        private static volatile bool disposed = false;
        private static int delay = 0;
        private static Random rand = new();
        public static event EventHandler? Report;
        public static void startsim()
        {
            Random ran = new Random();
            int delay;
            BO.Order ord;

            disposed = false;
            new Thread(() =>
            {
                while (!disposed)
                {
                    ord = bl!.Order.GetLastOrder();
                    if (ord != null)
                    {
                        delay = rand.Next(2, 10) * seconds;
                        Report!(Thread.CurrentThread, new TupleSimulatorArgs(Simulator.delay, ord));

                        Thread.Sleep(delay);
                        if (ord.Status is BO.OrderStatus.Confirmed)
                            bl.Order.UpdateShipDate(ord.ID);
                        else
                            bl.Order.UpdateDeliveryDate(ord.ID);
                    }
                    Thread.Sleep(seconds);
                }
            }).Start();
        }

        public static void stopsim()
        {
            disposed = true;
        }
    }
}
