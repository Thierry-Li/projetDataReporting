using DataReporting.Model.Business;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DataReporting
{
    /// <summary>
    /// Logique d'interaction pour PDF.xaml
    /// </summary>
    public partial class PDF : Window
    {

        public readonly List<BusinessLigneReleve> _listreleve;

        public PDF(List<BusinessLigneReleve> listreleve)
        {
            InitializeComponent();
            _listreleve = listreleve;


            string fSN = "";
            string ftotalRecords = "";
            string fstorageInterval = "";
            string ftempMin = "";
            string ftempMax = "";
            string ftempAvg = "";
            string fhygroMin = "";
            string fhygroMax = "";
            string fhygroAvg = "";
            string fstartDate = "";
            string fendDate = "";
            string fElapsedDate = ""; 

            fSN = Model.Service.SuperGlobal.GlobalSN;
            ftotalRecords = Model.Service.SuperGlobal.GlobalTotalRecords;
            fstorageInterval = Model.Service.SuperGlobal.GlobalStorageInterval;
            ftempMin = Model.Service.SuperGlobal.GlobalTempMin;
            ftempMax = Model.Service.SuperGlobal.GlobalTempMax;
            ftempAvg = Model.Service.SuperGlobal.GlobalTempAVG;
            fhygroMin = Model.Service.SuperGlobal.GlobalHygroMin;
            fhygroMax = Model.Service.SuperGlobal.GlobalHygroMax;
            fhygroAvg = Model.Service.SuperGlobal.GlobalHygroAVG;
            fstartDate = Model.Service.SuperGlobal.GlobalStartDate;
            fendDate = Model.Service.SuperGlobal.GlobalEndDate;
            fElapsedDate = Model.Service.SuperGlobal.GlobalElapsedDate;

            TXTSN.Text = fSN;
            TXTtotalRecords.Text = ftotalRecords;
            TXTstorageInterval.Text = fstorageInterval;
            TXTtempMin.Text = ftempMin;
            TXTtempMax.Text = ftempMax;
            TXTtempAvg.Text = ftempAvg;
            TXThygroMin.Text = fhygroMin;
            TXThygroMax.Text = fhygroMax;
            TXThygroAvg.Text = fhygroAvg;
            TXTstartDate.Text = fstartDate;
            TXTendDate.Text = fendDate;
            TXTelapsedDate.Text = fElapsedDate;

            


        }

        private void BTNretour(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            this.Close();
            mainwindow.Show();
        }

        private void BTNsave(object sender, RoutedEventArgs e)
        {
            //recup from global
            string SN = Model.Service.SuperGlobal.GlobalSN;
            string totalRecords = Model.Service.SuperGlobal.GlobalTotalRecords;
            string storageInterval = Model.Service.SuperGlobal.GlobalStorageInterval;
            string tempMin = Model.Service.SuperGlobal.GlobalTempMin;
            string tempMax = Model.Service.SuperGlobal.GlobalTempMax;
            string tempAvg = Model.Service.SuperGlobal.GlobalTempAVG;
            string hygroMin = Model.Service.SuperGlobal.GlobalHygroMin;
            string hygroMax = Model.Service.SuperGlobal.GlobalHygroMax;
            string hygroAvg = Model.Service.SuperGlobal.GlobalHygroAVG;
            string startDate = Model.Service.SuperGlobal.GlobalStartDate;
            string endDate = Model.Service.SuperGlobal.GlobalEndDate;
            string ElapsedDate = Model.Service.SuperGlobal.GlobalElapsedDate;


            //content --------------------------------
            
            //header
            string content = string.Empty;
            
            // CSS
            content += "<style>" + 
                "html{ font-size: 50px;}"+
                "li{ list-style: none;}" +
                "</style>";
            
            content += "<ul>";
            content += "<li>SN : " +SN+ "</li>";
            content += "<li>TotalRecords : "+totalRecords+"</li>";
            content += "<li>storageInterval : "+storageInterval+"</li>";
            content += "<li>température minimum : "+tempMin+"</li>";
            content += "<li>température maximum : "+tempMax+"</li>";
            content += "<li>température moyenne : "+tempAvg+"</li>";
            content += "<li>Hygrométrie minimum : "+hygroMin+"</li>";
            content += "<li>Hygrométrie maximum : "+hygroMax+"</li>";
            content += "<li>Hygrométrie moyenne : "+hygroAvg+"</li>";
            content += "<li>Date de départ : "+startDate+"</li>";
            content += "<li>Date de fin : "+endDate+"</li>";
            content += "<li>Temps écoulé : "+ElapsedDate+"</li>";
            content += "</ul>";
            
            content += "<li>----------------------------------------------------------</li>";
            //graph
            content += "<li>GRAPH HERE</li>";
            content += "<li>----------------------------------------------------------</li>";

            //content += "<li>----------------------------------------------------------</li>";
            //list

            Model.Service.ServiceDataReport serviceDataReport = new Model.Service.ServiceDataReport();
            
            content += "<ul>";
            int nbline = 1;
            foreach (var lines in _listreleve)
            {
                content += "<li>"+nbline+" . "+ lines.DateLigneReleve.ToString("yyyy/MM/dd") + " "+ lines.HeureLigneReleve + " " + lines.Temperature + " " + lines.Hygrometrie + "%</li>";
                nbline++;
            }
            
            content += "</ul>";


            // instantiate the html to pdf converter
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.MarginBottom = 10;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 10;

            // convert the url to pdf
            PdfDocument doc = converter.ConvertHtmlString(content);

            string adress = (string.Format("C:\\Users\\MSI Game\\TOTO\\{0}-{1}.pdf", DateTime.Now.ToString("yyyyMMddHHmmss"), SN));
            Model.Service.SuperGlobal.GlobalPath = adress;
            // save pdf document
            doc.Save(adress);

            // close pdf document
            doc.Close();
            MessageBox.Show("PDF crée");
        }

        private void BTNmail(object sender, RoutedEventArgs e)
        {
            Mail mail = new Mail();
            mail.Show();
            this.Close();
        }

        private void BTNcomm(object sender, RoutedEventArgs e)
        {
            
           
            
            
        }
    }
}
