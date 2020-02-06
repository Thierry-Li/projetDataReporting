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
        //Fonction SHOW
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
                    Model.GetRep.Vone = str;


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
