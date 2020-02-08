using DataReporting.Model.Business;
using DataReporting.Model.Service;
using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

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
							//CheckIntegrity.CheckLinesIntegrity(line);
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
			listBoxReleve.Items.RemoveAt(listBoxReleve.SelectedIndex);
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

		}

		// Capteur
		private void BtnDeleteCapteur(object sender, RoutedEventArgs e)
		{
			var capteurARetirer = gridCapteur.SelectedItem as BusinessCapteur;
			string messageBoxText = "Voulez-vous supprimer le capteur selectionné?";
			string caption = "Supprimer ?";
			MessageBoxButton button = MessageBoxButton.YesNoCancel;
			MessageBoxImage icon = MessageBoxImage.Question;
			MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

			if (result == MessageBoxResult.Yes)
			{
				ServiceCapteur.DeleteCapteur(capteurARetirer);
				gridCapteur.ItemsSource = ServiceCapteur.GetCapteur();
			}
			
		}

		private void BtnAjoutCapteur(object sender, RoutedEventArgs e)
		{
			BusinessCapteur nouveauCapteur = new BusinessCapteur
			{
				NumeroSerie = int.Parse(fieldNumero.Text),
				Libelle = fieldLibelle.Text
			};
			ServiceCapteur.AddCapteur(nouveauCapteur);
			MessageBox.Show("Capteur ajouté");
			gridCapteur.ItemsSource = ServiceCapteur.GetCapteur();
		}

		private void BtnSelectCapteur(object sender, RoutedEventArgs e)
		{
			BusinessCapteur businessCapteur = gridCapteur.SelectedItem as BusinessCapteur;
			int capteurId = businessCapteur.IdCapteur;
			MessageBox.Show("Capteur selectionné : Affichage liste des relevés du capteur.");
			gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(capteurId);

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
			foreach (BusinessReleve businessReleve in gridReleve.SelectedItems) 
			{
				releves.AddRange(ServiceLigneReleve.GetLignesReleveById(businessReleve.IdReleve));
			}
			MessageBox.Show("Relevé(s) selectionné(s) : Affichage liste des Lignes.");
			gridLigneReleve.ItemsSource = releves;
		}


		private void BtnSupprimerReleve(object sender, RoutedEventArgs e)
		{
			var refresh = gridReleve.SelectedItem as BusinessReleve;
			var releveARetirer = gridReleve.SelectedItem as BusinessReleve;
			string messageBoxText = "Voulez-vous supprimer le revelé selectionné?";
			string caption = "Supprimer ?";
			MessageBoxButton button = MessageBoxButton.YesNoCancel;
			MessageBoxImage icon = MessageBoxImage.Question;
			MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

			if (result == MessageBoxResult.Yes)
			{
				ServiceReleve.DeleteReleve(releveARetirer);
			}
			gridLigneReleve.ItemsSource = null;
			gridReleve.ItemsSource = ServiceReleve.GetReleveByCapteurId(releveARetirer.CapteurID);
		}

		// Test
		private void BtnSupprimerLigneDeReleve(object sender, RoutedEventArgs e)
		{
			var releveARetirer = gridLigneReleve.SelectedItem as BusinessReleve;
			ServiceLigneReleve.DeleteLigneReleve(releveARetirer);
			gridLigneReleve.ItemsSource = ServiceReleve.GetReleve();
		}

	}
}