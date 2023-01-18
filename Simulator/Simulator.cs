using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DocumentFormat.OpenXml.Bibliography;
using System.Threading;


namespace Simulator

{
    internal class Simulator
    {
        public static bool flag = true;
        //NEED TO LEARN HOW TO DEFINE AN EVENT
        static event report1(BlApi.IOrder? order, int time);
        static event report2(BlApi.IOrder? order, int time);
        static event report3(BlApi.IOrder? order, int time);
        public static void startsim()
        {
            Random ran = new Random();
            int delay;
            BlApi.IOrder? ord;
            new Thread(() =>
            {
                while (flag)
                {
                    ord = Bl.getlatestorder();
                    if (ord != null)
                    {
                      report1(ord, time);
                      delay = ran.Next(3, 10);
                      Thread.Sleep(1000 * delay);
                      bl.updateStatus();//NEED TO CREAT A METHOD OF FINDING LATEST ORDER IN THE LIST
                      report2(ord, time);
                    }
                    Thread.Sleep(1000 * delay);
                    report3(ord, time);
                }
            }).Start();

          
    }
            
            
        
        
    public static void stopsim()
    {
       flag = false;
    }
    
}
