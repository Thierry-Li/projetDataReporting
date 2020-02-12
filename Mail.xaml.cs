using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace DataReporting
{
    /// <summary>
    /// Logique d'interaction pour Mail.xaml
    /// </summary>
    public partial class Mail : Window
    {
        public Mail()
        {
            InitializeComponent();
        }

        private void Load(object sender, EventArgs e)
        {
            //version1
            string zone = Model.Service.SuperGlobal.GlobalPath;
            if (zone != "")
            {
                //version1
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                MailAddress adresseMailEnvoyeur = new MailAddress("dta.hardwork@gmail.com");
                MailAddress Receveur = new MailAddress(TXTTo.Text);
                MailMessage ConfigMess = new MailMessage(adresseMailEnvoyeur, Receveur);
                Attachment PJ = new Attachment(zone);
                ConfigMess.Attachments.Add(PJ);
                ConfigMess.Body = "Voici le rapport PDF en ci joint :";
                ConfigMess.Subject = "Data Reporting";
                NetworkCredential user = new NetworkCredential("dta.hardwork@gmail.com", "Re7744624");
                client.Credentials = user;
                client.Send(ConfigMess);
                MainWindow pdf = new MainWindow();
                pdf.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Aucun envoi, veuillez d'abord creer un PDF");
                MainWindow pdf1 = new MainWindow();
                pdf1.Show();
                this.Close();
            }   
        }
    }
}
