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


			ObservableCollection<BusinessCapteur> capteurs =  new ObservableCollection<BusinessCapteur>(ServiceCapteur.GetCapteur() as List<BusinessCapteur>);
			gridTest.ItemsSource = capteurs;

			//gridFromBdd.ItemsSource = ServiceLigneReleve.GetLignesReleve();
			gridFromBdd.ItemsSource = ServiceReleve.GetReleve();
			List<BusinessReleve> Releves= ServiceReleve.GetReleve();


			


		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{

			var cellCapteur = gridTest.SelectedItem;
			gridTest.SelectedCells.Clear();
			//ItemsControl.ItemsSource
			gridTest.Items.Remove(cellCapteur);
			/*BusinessCapteur businessCapteur = (BusinessCapteur)cellCapteur.Item;
			businessCapteur*/
		}

		//Import Dossier TXT

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


		//Fonction DELETE
		private void LoadValue(object sender, EventArgs e)
		{
			listBoxReleve.Items.RemoveAt(listBoxReleve.SelectedIndex);
		}

		private void DeleteTest_Click(object sender, RoutedEventArgs e)
		{
			var capteurARetirer = gridTest.SelectedItem as BusinessCapteur;
			ServiceCapteur.DeleteCapteur(capteurARetirer);
			gridTest.ItemsSource = ServiceCapteur.GetCapteur();
		}

		private void AddTest_click(object sender, RoutedEventArgs e)
		{
			BusinessCapteur nouveauCapteur = new BusinessCapteur
			{
				NumeroSerie = int.Parse(fieldNumero.Text),
				Libelle = fieldLibelle.Text
			};
			ServiceCapteur.AddCapteur(nouveauCapteur);
			MessageBox.Show("Capteur ajouté");
			gridTest.ItemsSource = ServiceCapteur.GetCapteur();
		}

		private void SaveReleveToBDD(object sender, RoutedEventArgs e)
		{
			var cellInfo = gridTest.SelectedCells[0];
			BusinessCapteur businessCapteur = (BusinessCapteur)cellInfo.Item;
			int capteurId = businessCapteur.IdCapteur;
			BusinessReleve businessReleve = new BusinessReleve();
			businessReleve.CapteurID = capteurId;

			int idReleve = ServiceReleve.AddReleve(businessReleve);
		
			List<BusinessLigneReleve> lignesReleve = new List<BusinessLigneReleve>();
			foreach (var item in listBoxReleve.Items)
			{
				string[] list = Regex.Split(item.ToString(), @"\s+");

				BusinessLigneReleve businessLigneReleve = new BusinessLigneReleve();
				businessLigneReleve.DateLigneReleve = DateTime.Parse(list[1]);
				businessLigneReleve.HeureLigneReleve = TimeSpan.Parse(list[2]);
				businessLigneReleve.Temperature = double.Parse(list[3].Replace(".", ","));
				businessLigneReleve.Hygrometrie = double.Parse(list[4].Remove(list[4].Length - 1).Replace("." , ","));
				businessLigneReleve.ReleveID = idReleve;
				lignesReleve.Add(businessLigneReleve);


			}

			ServiceLigneReleve.AddLignesReleve(lignesReleve);
			MessageBox.Show("yeah");
		}

		private void BtnEnleverLigne_Click(object sender, RoutedEventArgs e)
		{
			var releveARetirer = gridFromBdd.SelectedItem as BusinessReleve;
			ServiceLigneReleve.DeleteLigneReleve(releveARetirer);
			gridFromBdd.ItemsSource = ServiceReleve.GetReleve();
		}

		
	}
}