using Projet4.Model.DATA;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projet4
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            gridTest.ItemsSource = new DataReportingEntities().Capteur.ToList();
            
        }

        private void CBTNconnexion(object sender, RoutedEventArgs e)
        {
            
            Vue.Home home = new Vue.Home();
            home.Show();
            this.Close();
        }

        private void gridTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
