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
			gridTest.ItemsSource = ServiceCapteur.GetCapteur();
			//gridFromBdd.ItemsSource = ServiceLigneReleve.GetLignesReleve();
			gridFromBdd.ItemsSource = ServiceReleve.GetReleve();
			List<BusinessReleve> Releves= ServiceReleve.GetReleve();

			

			List<int> RowIndexes = new List<int>();
			var SelectedItemsCount = gridFromBdd.SelectedItems;

			BusinessReleve businessReleve = new BusinessReleve();
			//var temperature = businessReleve.Temperature.Where(t => selectedItems.Any(selectedItems => t.temperature == selectedItems.temperature));

			//for (int i = 0; i <= SelectedItemsCount; i++)
			//{
			//	RowIndexes.Add(gridFromBdd.Items.IndexOf(gridFromBdd.Items[i]));
			//}
			//List<float> temperatures = new List<float>();
			//foreach (int index in RowIndexes)
			//{
			//	temperatures.Add(index);
			//}
			//float average = temperatures.Average();
			//moyenneField.AppendText(average.ToString());

			//float AverageTemperature()
			//{
			//	foreach (float temperature in temperatures)
			//	{
			//
			//	}
			//}

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
			var cell = gridTest.SelectedIndex;
		
			List<string> listDeReleve = new List<string>();
			foreach (var item in listBoxReleve.Items)
			{
				listDeReleve.Add(item.ToString());
			}


		}

		private void BtnEnleverLigne_Click(object sender, RoutedEventArgs e)
		{
			var releveARetirer = gridFromBdd.SelectedItem as BusinessReleve;
			ServiceLigneReleve.DeleteLigneReleve(releveARetirer);
			gridFromBdd.ItemsSource = ServiceReleve.GetReleve();
		}
	}
}