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

namespace PL
{
    /// <summary>
    /// Interaction logic for Password.xaml/
    /// </summary>
    public partial class Password : Window
    {
        private string PasswordText;
        public Password()
        {

            InitializeComponent();
            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordText == "Fation")
                Close();
            else
            {
                PasswordText = "";
                lblNotCorrect.Visibility = Visibility.Visible;
            }
        }

        private void PasswordText_textBox(object sender, RoutedEventArgs e)
        {
            PasswordText = password.Password;
        }
    }
}
