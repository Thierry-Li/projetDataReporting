using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;


namespace Projet4.Vue
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class PDF : Window
    {
        ObservableCollection<KeyValuePair<double, double>> Power = new ObservableCollection<KeyValuePair<double, double>>();
        public PDF()
        {
            InitializeComponent();
            
            string str = Model.GetRep.FilePassReleve;

            string filetxt = File.ReadAllText(str); //recupération du contenue du fichier TXT

            try
            {

                try
                {
                    string[] lines = File.ReadAllLines(str);
                    foreach (string line in lines)
                    {
                        LISTdata.Items.Add(line); //Conversion TEXT en LIST

                    }
                }
                catch { }
            }
            catch { }

            

        }
        





    //EXIT BUTTON-----------------------------------------------
    private void CBTNexit(object sender, RoutedEventArgs e)
        {
            Home back = new Home();
            back.Show();
            this.Close();
        }

        private void CBTNclick(object sender, EventArgs e)
        {
           
        }

    
    }
}
