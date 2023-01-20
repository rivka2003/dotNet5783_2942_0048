namespace Simulator
{
    enum SimulationProgress { UpdateDone = 1, Done }
    public static class Simulator
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        private const int seconds = 1000;
        private static volatile bool run = false;
        private static int delay = 0;
        private static Random rand = new();
        public static event EventHandler? Report;
        public static void startsim()
        {
            BO.Order ord = new();
            run = true;

            new Thread(() =>
            {
                while (run)
                {
                    ord = bl!.Order.GettingLatestOrder();
                    if (ord != null)
                    {
                        delay = rand.Next(2, 10) * seconds;
                        Report!(Thread.CurrentThread, new TupleSimulatorArgs(delay, ord));

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
            run = false;
        }
    }
}
