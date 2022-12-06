using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for adding.xaml.
    /// </summary>
    public partial class adding : Window
    {
        private BO.Category category = new BO.Category();
        
        public adding(IBl bl)
        {
            InitializeComponent();
          
        }

        private void cbCATEGORY_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
