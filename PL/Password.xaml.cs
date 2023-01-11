﻿using System.Windows;
using System.Windows.Controls;

namespace PL
{
    /// <summary>
    /// Interaction logic for Password.xaml/
    /// </summary>
    public partial class Password : Window
    {
        
        public Password()
        {
            InitializeComponent();   
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.passwordText == "Fation")
                Close();
            else
                MainWindow.passwordText = "";
        }

        private void PasswordText_textBox(object sender, RoutedEventArgs e)
        {
            MainWindow.passwordText = ((PasswordBox)sender).Password;
        }
    }
}
