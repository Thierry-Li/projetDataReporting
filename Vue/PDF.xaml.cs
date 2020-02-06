using Microsoft.Win32;
using System.IO;
using System;
using System.Windows;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;

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


            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            writer.Write(gridgeneral);
            doc.Close();
            package.Close();

            /*
            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
            PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, d.FileName, 0);
            */



            /*
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF file|*.pdf", ValidateNames = true };
            {
                if(sfd.ShowDialog() == true)
                {
                    
                    Document doc = new Document(PageSize.A4.Rotate());
                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();
                   
                 
                    }
                    catch(Exception error)
                    {
                        MessageBox.Show(error.Message, "Message", MessageBoxButton.OK);
                    }
                    finally
                    {
                        doc.Close();
                    }
                }
            }
            */

        }


    }
}
