using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

using DataReporting.Model.Business;
using DataReporting.Model.Service;

using Microsoft.Win32;

namespace DataReporting
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //List<BusinessLigneReleve> modelReleve;

        public MainWindow()
        {
            InitializeComponent();
            gridCapteur.ItemsSource = ServiceCapteur.GetCapteur();
        }

        /// <summary>
        /// import txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChargerFichier_Click(object sender, RoutedEventArgs e)
        {
            Stream myStream;
            OpenFileDialog ofc = new OpenFileDialog
            {
                RestoreDirectory = true,
                InitialDirectory = @"D:\TOTO"
            };
            if (ofc.ShowDialog() == true)
            {
                if ((myStream = ofc.OpenFile()) != null)
                {
                    string str = ofc.FileName;
                    string filetxt = File.ReadAllText(str); //recupération du contenue du fichier TXT

                    try
                    {
                        string[] lines = File.ReadAllLines(str);
                        listBoxReleve.ItemsSource = lines.ToList();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimerLigne(object sender, EventArgs e)
        {
            try
            {
                if (listBoxReleve.SelectedItem != null)
                {
                    if (MessageBox.Show("Voulez-vous retirer la ligne selectionnée et ne pas l'enregistrer dans la BDD?", "Supprimer la ligne ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        listBoxReleve.Items.Remove(listBoxReleve.SelectedItem);
                    }
                }
                else
                {
                    MessageBox.Show("Aucune ligne n'est sélectionnée.");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveReleveToBDD(object sender, RoutedEventArgs e)
        {
            if (gridCapteur.SelectedItem == null)
            {
                MessageBox.Show("Veuillez choisir un capteur");
                return;
            }

            BusinessReleve businessReleve = new BusinessReleve
            {
                CapteurID = ((BusinessCapteur)gridCapteur.SelectedItem).IdCapteur,
            };

            ServiceReleve.AddReleve(businessReleve);

            List<BusinessLigneReleve> lignesReleve = new List<BusinessLigneReleve>();
            foreach (var item in listBoxReleve.Items)
            {
                string[] list = Regex.Split(item.ToString(), @"\s+");

                BusinessLigneReleve businessLigneReleve = new BusinessLigneReleve
                {
                    DateLigneReleve = DateTime.Parse(list[1]),
                    HeureLigneReleve = TimeSpan.Parse(list[2]),
                    Temperature = double.Parse(list[3].Replace(".", ",")),
                    Hygrometrie = double.Parse(list[4].Remove(list[4].Length - 1).Replace(".", ",")),
                    ReleveID = businessReleve.IdReleve
                };
                lignesReleve.Add(businessLigneReleve);
            }
            ServiceLigneReleve.AddLignesReleve(lignesReleve);
            MessageBox.Show("Fichier TXT associé au capteur");
            listBoxReleve.Items.Clear();
            gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(businessReleve.IdReleve);
        }


        /// <summary>
        /// capteur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteCapteur(object sender, RoutedEventArgs e)
        {
            BusinessCapteur capteurARetirer = gridCapteur.SelectedItem as BusinessCapteur;
            if (capteurARetirer != null)
            {
                if (MessageBox.Show("Voulez-vous supprimer le capteur selectionné?", "Supprimer ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    ServiceCapteur.DeleteCapteur(capteurARetirer);
                    gridCapteur.ItemsSource = ServiceCapteur.GetCapteur();
                }
            }
            else
            {
                MessageBox.Show("Aucun capteur n'est sélectionné.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAjoutCapteur(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(fieldNumero.Text) && !string.IsNullOrEmpty(fieldLibelle.Text))
            {
                ServiceCapteur.AddCapteur(new BusinessCapteur
                {
                    NumeroSerie = int.Parse(fieldNumero.Text),
                    Libelle = fieldLibelle.Text
                });
                MessageBox.Show("Capteur ajouté");
                gridCapteur.ItemsSource = ServiceCapteur.GetCapteur();
            }
            else
            {
                MessageBox.Show("Veuillez ajouter un numéro de capteur et un nom de capteur.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectCapteur(object sender, RoutedEventArgs e)
        {
            BusinessCapteur businessCapteur = gridCapteur.SelectedItem as BusinessCapteur;
            try
            {
                if (businessCapteur != null)
                {
                    gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(businessCapteur.IdCapteur);
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner un capteur");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectReleves(object sender, RoutedEventArgs e)
        {
            List<BusinessLigneReleve> releves = new List<BusinessLigneReleve>();
            foreach (BusinessReleve businessReleve in gridReleve.SelectedItems)
            {
                releves.AddRange(ServiceLigneReleve.GetLignesReleveById(businessReleve.IdReleve));
            }
            gridLigneReleve.ItemsSource = releves;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSupprimerReleve(object sender, RoutedEventArgs e)
        {
            BusinessReleve releveARetirer = gridReleve.SelectedItem as BusinessReleve;
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez-vous supprimer le revelé selectionné?", "Supprimer ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (releveARetirer != null)
            {
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    ServiceReleve.DeleteReleve(releveARetirer);
                }
                gridLigneReleve.ItemsSource = null;
                gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(releveARetirer.CapteurID);
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenererRapport(object sender, RoutedEventArgs e)
        {
            List<BusinessLigneReleve> ligneReleveContent = new List<BusinessLigneReleve>();
            ligneReleveContent = gridLigneReleve.ItemsSource as List<BusinessLigneReleve>;

         

            //IEnumerable ligneReleveContent;
            //ligneReleveContent = gridLigneReleve.ItemsSource;


            List<DateTime> infosDates = new List<DateTime>();


            //var heureMin = (from r in ligneReleveContent group r by r.HeureLigneReleve into kj select kj.Min().).FirstOrDefault();

            BusinessDataReport businessDataReport = new BusinessDataReport();
            foreach (BusinessLigneReleve item in ligneReleveContent)
            {
                infosDates.Add(item.DateLigneReleve.Add(item.HeureLigneReleve));

            }

            BusinessTemperature temperature = new BusinessTemperature();
            temperature.TempMin = ligneReleveContent.Min(ligne => ligne.Temperature);
            temperature.TempMax = ligneReleveContent.Max(ligne => ligne.Temperature);
            temperature.TempAvg = ligneReleveContent.Average(ligne => ligne.Temperature);

            businessDataReport.Temperature = temperature;

            BusinessHygrometrie hygrometrie = new BusinessHygrometrie();

            hygrometrie.HygrometrieMin = ligneReleveContent.Min(ligne => ligne.Hygrometrie);
            hygrometrie.HygrometrieMax = ligneReleveContent.Max(ligne => ligne.Hygrometrie);
            hygrometrie.HygrometrieAvg = ligneReleveContent.Average(ligne => ligne.Hygrometrie);

            businessDataReport.Hygrometrie = hygrometrie;

            BusinessDateAndTime businessDateAndTime = new BusinessDateAndTime();
            businessDateAndTime.StartTime = infosDates.Min();
            businessDateAndTime.EndTime = infosDates.Max();
            businessDateAndTime.ElapsedTime = businessDateAndTime.EndTime - businessDateAndTime.StartTime;
            //businessDateAndTime.StorageInterval = infosDates[1] - infosDates[0];
            businessDateAndTime.StorageInterval = infosDates[1].Subtract(infosDates[0]);

            //TODO ajoiuter champ note et numero 
            //businessDataReport.Notes();

            businessDataReport.TotalRecords = ligneReleveContent.Count;

            BusinessCapteur businessCapteur = ServiceCapteur.GetCapteurByReleveId(ligneReleveContent[0].ReleveID);
            businessDataReport.NumeroSerieCapteur = businessCapteur.NumeroSerie;

            //other
            string SNField = businessDataReport.NumeroSerieCapteur.ToString();
            string totalRecordsField = ligneReleveContent.Count.ToString();
            string storageIntervalField = businessDateAndTime.StorageInterval.ToString();
            //temp
            string tempMinField = temperature.TempMin.ToString("0.00");
            string tempMaxField = temperature.TempMax.ToString("0.00");
            string tempAvgField = temperature.TempAvg.ToString("0.00");
            //hygrometrie
            string hygroMinField = hygrometrie.HygrometrieMin.ToString();
            string hygroMaxField = hygrometrie.HygrometrieMax.ToString();
            string hygroAvgField = hygrometrie.HygrometrieAvg.ToString("0.00");
            //date
            string startDateField = businessDateAndTime.StartTime.ToString();
            string endDateField = businessDateAndTime.EndTime.ToString();
            string ElapsedDateField = businessDateAndTime.ElapsedTime.ToString(@"ddd\hh\:mm\:ss");
            //TODO ajouter les champs

            //toGlobal
            SuperGlobal.GlobalSN = SNField;
            SuperGlobal.GlobalTotalRecords = totalRecordsField;
            SuperGlobal.GlobalStorageInterval = storageIntervalField;
            SuperGlobal.GlobalTempMin = tempMinField;
            SuperGlobal.GlobalTempMax = tempMaxField;
            SuperGlobal.GlobalTempAVG = tempAvgField;
            SuperGlobal.GlobalHygroMin = hygroMinField;
            SuperGlobal.GlobalHygroMax = hygroMaxField;
            SuperGlobal.GlobalHygroAVG = hygroAvgField;
            SuperGlobal.GlobalStartDate = startDateField;
            SuperGlobal.GlobalEndDate = endDateField;
            SuperGlobal.GlobalElapsedDate = ElapsedDateField;

            PDF pdf = new PDF(ligneReleveContent);
            pdf.Show();
            this.Close();
        }

        private void BtnGenererCSV(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Le document CSV à bien été crée");
            var csv = new StringBuilder();

            var filteredTable = gridLigneReleve.ItemsSource as List<BusinessLigneReleve>;

            var header = string.Format("{0};{1};{2};{3};{4}{5}",
                   "Date",
                    "Heure",
                    "Humidité",
                    "id ligne relevé",
                    "température",
                    Environment.NewLine
                );
            csv.Append(header);

            foreach (var item in filteredTable)
            {
                var newline = string.Format("{0};{1};{2};{3};{4}{5}",
                    item.DateLigneReleve.ToString("yyyy/MM/dd"),
                    item.HeureLigneReleve,
                    item.Hygrometrie,
                    item.IdLigneReleve,
                    item.Temperature,
                    Environment.NewLine
                );
                csv.Append(newline);
            }

            File.WriteAllText(string.Format("C:\\Users\\MSI Game\\TOTO\\{0}.csv", DateTime.Now.ToString("yyyyMMddHms")), csv.ToString(), Encoding.UTF8);


        }
    }
}
