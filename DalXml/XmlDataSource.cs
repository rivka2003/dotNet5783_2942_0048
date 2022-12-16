using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class XmlDataSource
    {
        static XmlDataSource()
        {
            s_Initialize();
        }

        private static void s_Initialize()
        {
            InitProduct();
            InitOrder();
            InitOrderItem();
        }
    }
}
