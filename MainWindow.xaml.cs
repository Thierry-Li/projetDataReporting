using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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
		
		public MainWindow()
		{
			InitializeComponent();

			ComboBoxSelectCapteur.ItemsSource = ServiceCapteur.GetCapteur().ToString();

			ObservableCollection<BusinessCapteur> capteurs =  new ObservableCollection<BusinessCapteur>(ServiceCapteur.GetCapteur() as List<BusinessCapteur>);
			gridCapteur.ItemsSource = capteurs;
			//gridReleve.ItemsSource = ServiceReleve.GetReleve();
			//gridLigneReleve.ItemsSource = ServiceLigneReleve.GetLignesReleve();
		}
		

		// Import fichier TXT

		private void ChargerFichier_Click(object sender, RoutedEventArgs e)
		{
			Stream myStream;
			OpenFileDialog ofc = new OpenFileDialog
			{
				RestoreDirectory = true,
				InitialDirectory = @"Y:\OneDrive - Association Cesi Viacesi mail\CESI\projet4-CSHARP\projetDataReporting\txt"
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
						foreach (string line in lines)
						{
							listBoxReleve.Items.Add(line);
						}
					}
					catch (Exception)
					{
						throw;
					}
				}
			}

		}
		/*
		private void ChargerFichier_Click(object sender, RoutedEventArgs e)
		{
			Stream myStream;
			OpenFileDialog ofc = new OpenFileDialog
			{
				RestoreDirectory = true,
				InitialDirectory = @"Y:\OneDrive - Association Cesi Viacesi mail\CESI\projet4-CSHARP\projetDataReporting\txt" 
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
						foreach (string line in lines)
						{
							listBoxReleve.Items.Add(line);
							//string[] categorieDeReleve = line.Split(' ');
							//foreach (string champdeReleve in categorieDeReleve)
							//{
							//	char indexReleve = champdeReleve[0];
							//	//string dateTime = Convert.ToDateTime(champdeReleve[1]);
							//	//TimeSpan timeSpan = TimeSpan.TryParse(champdeReleve[2]);
							//	BusinessReleve releveAEnregistrer = new BusinessReleve();
							//	//ReleveAEnregistrer.DateReleve = DateTime.Parse(char.ToString(champdeReleve[1]));
							//
							//	releveAEnregistrer.DateReleve = Convert.ToDateTime(champdeReleve[1]);
							//	//MessageBox.Show(ReleveAEnregistrer.DateReleve.ToString());
							//};
							
						}
					}
					catch
					{
						
					}
				}
			}
		}
		*/

		private void BtnSupprimerLigne(object sender, EventArgs e)
		{
			try
			{
				if(listBoxReleve.SelectedItem != null)
				{
					MessageBoxResult messageBoxResult = MessageBox.Show("Voulez-vous retirer la ligne selectionnée et ne pas l'enregistrer dans la BDD?", "Supprimer la ligne ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
					if (messageBoxResult == MessageBoxResult.Yes)
					{
						listBoxReleve.Items.Remove(listBoxReleve.SelectedItem);
					}
				} else
				{
					MessageBox.Show("Aucune ligne n'est sélectionnée.");
				}
			}
			catch (Exception)
			{
				throw;
				
			};

		}

		private void SaveReleveToBDD(object sender, RoutedEventArgs e)
		{
			var cellInfo = gridCapteur.SelectedCells[0];
			BusinessCapteur businessCapteur = (BusinessCapteur)cellInfo.Item;
			int capteurId = businessCapteur.IdCapteur;
			BusinessReleve businessReleve = new BusinessReleve
			{
				CapteurID = capteurId
			};

			int idReleve = ServiceReleve.AddReleve(businessReleve);

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
					ReleveID = idReleve
				};
				lignesReleve.Add(businessLigneReleve);
			}
			ServiceLigneReleve.AddLignesReleve(lignesReleve);
			MessageBox.Show("yeah");
			listBoxReleve.Items.Clear();
			gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(capteurId);
		}
		

		// Capteur
		private void BtnDeleteCapteur(object sender, RoutedEventArgs e)
		{
			BusinessCapteur capteurARetirer = gridCapteur.SelectedItem as BusinessCapteur;
			//string messageBoxText = "Voulez-vous supprimer le capteur selectionné?";
			//string caption = "Supprimer ?";
			//MessageBoxButton button = MessageBoxButton.YesNoCancel;
			//MessageBoxImage icon = MessageBoxImage.Question;
			//MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, caption, button, icon);
			if (capteurARetirer != null)
			{
				MessageBoxResult messageBoxResult = MessageBox.Show("Voulez-vous supprimer le capteur selectionné?", "Supprimer ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
				if (messageBoxResult == MessageBoxResult.Yes)
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

		private void BtnAjoutCapteur(object sender, RoutedEventArgs e)
		{
			BusinessCapteur nouveauCapteur = new BusinessCapteur
			{
				NumeroSerie = int.Parse(fieldNumero.Text),
				Libelle = fieldLibelle.Text
			};

			if (nouveauCapteur != null)
			{
				
				ServiceCapteur.AddCapteur(nouveauCapteur);
				MessageBox.Show("Capteur ajouté");
				gridCapteur.ItemsSource = ServiceCapteur.GetCapteur();
			}
			else if (nouveauCapteur == null)
			{
				MessageBox.Show("Veuillez ajouter un numéro de capteur et un nom de capteur.");
			}
			
		}

		private void BtnSelectCapteur(object sender, RoutedEventArgs e)
		{
			
			BusinessCapteur businessCapteur = gridCapteur.SelectedItem as BusinessCapteur;
			try
			{
				if (businessCapteur != null)
				{
					int capteurId = businessCapteur.IdCapteur;
					//MessageBox.Show("Capteur selectionné : Affichage liste des relevés du capteur.");
					gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(capteurId);
				}
				else
				{
					MessageBox.Show("Veuillez selectionner un capteur");
				}
			} catch (Exception)
			{
				throw;
				//Debug.WriteLine("Exception Message: " + e.Message);
			};
		}

		//private void BtnSupprLigneCapteur(object sender, RoutedEventArgs e)
		//{
		//
		//	//var cellCapteur = gridCapteur.SelectedItems;
		//	
		//	gridCapteur.SelectedItems.Clear();
		//	/*BusinessCapteur businessCapteur = (BusinessCapteur)cellCapteur.Item;
		//	businessCapteur*/
		//}

		// Releve
		private void BtnSelectReleves(object sender, RoutedEventArgs e)
		{
			List<BusinessLigneReleve> releves = new List<BusinessLigneReleve>();
			if ( releves != null)
			{
				foreach (BusinessReleve businessReleve in gridReleve.SelectedItems)
				{
					releves.AddRange(ServiceLigneReleve.GetLignesReleveById(businessReleve.IdReleve));
				}
				//MessageBox.Show("Relevé(s) selectionné(s) : Affichage liste des Lignes.");
				gridLigneReleve.ItemsSource = releves;
			} else
			{
				MessageBox.Show("Veuillez selectionner une date de relevé.");
			}
			
		}


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


		// Ligne de Releve
		private void BtnSupprimerLigneDeReleve(object sender, RoutedEventArgs e)
		{
			BusinessLigneReleve ligneReleveARetirer = gridLigneReleve.SelectedItem as BusinessLigneReleve;
			MessageBoxResult messageBoxResult = MessageBox.Show("Voulez-vous supprimer la ligne de relevé selectionnée?", "Supprimer ?", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
			if (ligneReleveARetirer != null)
			{
				if (messageBoxResult == MessageBoxResult.Yes)
				{
					ServiceLigneReleve.DeleteLigneReleve(ligneReleveARetirer);

				}
				gridLigneReleve.ItemsSource = ServiceLigneReleve.GetLignesReleveById(ligneReleveARetirer.ReleveID);
			}
		}


		// Rapport Synthèse
		private void BtnGenererRapport(object sender, RoutedEventArgs e)
		{
			//IEnumerable ligneReleveContent;
			//ligneReleveContent = gridLigneReleve.ItemsSource;
			List<BusinessLigneReleve> ligneReleveContent = new List<BusinessLigneReleve>();
			ligneReleveContent = gridLigneReleve.ItemsSource as List<BusinessLigneReleve>;
		
			List<DateTime> infosDates = new List<DateTime>();


			BusinessDataReport businessDataReport = new BusinessDataReport();
			foreach (BusinessLigneReleve item in ligneReleveContent)
			{
				infosDates.Add(item.DateLigneReleve.Add(item.HeureLigneReleve));
			
			}

			BusinessTemperature temperature = new BusinessTemperature();
			temperature.TempMin = ligneReleveContent.Min( ligne => ligne.Temperature);
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

			SNField.Text = businessDataReport.NumeroSerieCapteur.ToString();
			totalRecordsField.Text = ligneReleveContent.Count.ToString();
			storageIntervalField.Text = businessDateAndTime.StorageInterval.ToString();

			tempMinField.Text = temperature.TempMin.ToString("0.00");
			tempMaxField.Text = temperature.TempMax.ToString("0.00");
			tempAvgField.Text = temperature.TempAvg.ToString("0.00");

			hygroMinField.Text = hygrometrie.HygrometrieMin.ToString();
			hygroMaxField.Text = hygrometrie.HygrometrieMax.ToString();
			//hygroAvgField.Text = hygrometrie.HygrometrieAvg.ToString("P", CultureInfo.CreateSpecificCulture("hr-HR"));
			hygroAvgField.Text = hygrometrie.HygrometrieAvg.ToString("0.00"); 
			

			startDateField.Text = businessDateAndTime.StartTime.ToString();
			endDateField.Text = businessDateAndTime.EndTime.ToString();
			ElapsedDateField.Text = businessDateAndTime.ElapsedTime.ToString(@"ddd\hh\:mm\:ss");
			//TODO ajouter les champs
		}

	}
}/*
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
	ReleveID = idReleve
};
lignesReleve.Add(businessLigneReleve);
			}
			ServiceLigneReleve.AddLignesReleve(lignesReleve);*/