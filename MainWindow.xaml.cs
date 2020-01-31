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
using System.Data.SqlClient;

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
        private void loadClick(object sender, RoutedEventArgs e)
        {

            Stream myStream;
            OpenFileDialog ofc = new OpenFileDialog();
            ofc.RestoreDirectory = true;
            ofc.InitialDirectory = @"Y:\OneDrive - Association Cesi Viacesi mail\CESI\projet4-CSHARP\projetDataReporting\txt";  //Chemin dossier à importer
            if (ofc.ShowDialog() == true)
            {

                if ((myStream = ofc.OpenFile()) != null)
                {
                    string str = ofc.FileName;

                    string filetxt = File.ReadAllText(str); //recupération du contenue du fichier TXT

                    try
                    {

                        try
                        {
                            string[] lines = File.ReadAllLines(str);
                            foreach (string line in lines)
                            {
                                listBox.Items.Add(line); //Conversion TEXT en LIST

                            }
                        }
                        catch { }
                    }
                    catch { }
                }
            }
        }

        //Fonction DELETE
        private void loadValue(object sender, EventArgs e)
        {
            listBox.Items.RemoveAt(listBox.SelectedIndex);
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=THIERRYLI6676;Initial Catalog=dataReport;User ID=sa;Password=azerty";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            //MessageBox.Show("Connection Open  !");
            //cnn.Close();

            SqlCommand command;
            SqlDataReader datareader;
            String sql,Output = "";
            sql = "Select numeroSerie, libelle from capteur";
            command = new SqlCommand(sql, cnn);
            datareader = command.ExecuteReader();
            while (datareader.Read())
            {
                Output = Output + datareader.GetValue(0) + " - " + datareader.GetString(1) + "\n";
            }
            MessageBox.Show(Output);
            datareader.Close();
            command.Dispose();
            cnn.Close();
        }
    }
}

