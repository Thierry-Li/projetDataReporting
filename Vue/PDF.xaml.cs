using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Projet4.Vue
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class PDF : Window
    {
        public PDF()
        {
            InitializeComponent();
            string str = Model.GetRep.Vone;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }


    }
}
