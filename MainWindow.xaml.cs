using Microsoft.Win32;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;


namespace DataReporting
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Import Dossier TXT
       

        private void GOTO(object sender, EventArgs e)
        {
            Vue.Window2 v2 = new Vue.Window2();
            v2.Show();



        }

        private void Connexion(object sender, EventArgs e)
        {
            Model.Program connexion = new Model.Program();
            connexion.Start();

        }

    }
}

