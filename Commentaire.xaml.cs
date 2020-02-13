using System.Windows;

namespace DataReporting
{
    /// <summary>
    /// Logique d'interaction pour Commentaire.xaml
    /// </summary>
    public partial class Commentaire : Window
    {
        public Commentaire()
        {
            InitializeComponent();
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            Model.Service.SuperGlobal.GlobalComm = TXTcomm.Text;
            this.Close();
        }
    }
}
