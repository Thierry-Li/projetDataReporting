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
            ofc.InitialDirectory = @"D:\TOTO";  //Chemin dossier à importer
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

    }
}

