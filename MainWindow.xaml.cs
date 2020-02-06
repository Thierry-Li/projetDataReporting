using Microsoft.Win32;
using Projet4.Model.DATA;
using System;
using System.IO;
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
            gridTest.ItemsSource = new DataReportingEntities().Capteur.ToList();//Get Capteur ID from BDD
            
        }

        private void CBTNselect(object sender, EventArgs e)
        {

            Stream myStream;
            OpenFileDialog ofc = new OpenFileDialog();
            ofc.RestoreDirectory = true;
            ofc.InitialDirectory = @"D:\TOTO";  //Chemin dossier à importer
            if (ofc.ShowDialog() == true)
            {

                if ((myStream = ofc.OpenFile()) != null)
                {
                    string str = ofc.FileName;

                    //envois du repertoire:
                    Model.GetRep.FilePassReleve = str;//envois TXTpath vers Global


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

        private void CBTNvalider(object sender, RoutedEventArgs e)
        {
            
            

            object item = gridTest.SelectedItem;
            string ID = (gridTest.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;//recup de l'ID capteur
            string Sreleve = Model.GetRep.FilePassReleve;//recup du chemin relevé
            Model.GetRep.GlobalIDCapteur = ID;//envois ID vers Global

            MessageBox.Show("Capteur : " + ID + "\nReleve : " + Sreleve);

            Vue.Home home = new Vue.Home();
            home.Show();
            this.Close();
        }
    }
}
