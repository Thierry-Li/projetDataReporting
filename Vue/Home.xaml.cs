using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace Projet4.Vue
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();


        }

        //LIST TXT---------------------------------------------------------

        //Fonction DELETE
        private void CBTNdelete(object sender, EventArgs e)
        {
            listBox.Items.RemoveAt(listBox.SelectedIndex);
        }

        //BACK BUTTON-----------------------------------------------
        private void CBTNback(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Close();
        }

        private void CBTNpdf(object sender, RoutedEventArgs e)
        {
            PDF pdf = new PDF();
            pdf.Show();
            this.Close();
        }
    }
}

