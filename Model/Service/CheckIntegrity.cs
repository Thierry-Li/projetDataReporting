using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace DataReporting.Model.Service
{
	public static class CheckIntegrity
	{

		public static void CheckLinesIntegrity(string lineOfFile)
		{
			
			
		}
/*
		public static void CheckLinesIntegrity(string lineOfFile)
		{
			int counter = 0;
			string line;
			string[] comp_array;
			string[] reg_express = new string[] { @"\d{4}(-\d{2})(-\d{2})$", @"\d{2}(:\d{2})(:\d{2})$", @"\d{2}(.\d{1})$", @"\d{2}(.\d{1})(%)" };
			List<string> errors = new List<string>();
			List<string> line_errors = new List<string>();
			string InitialDirectory = "";

			StreamReader file = new StreamReader(InitialDirectory);

			while ((line = file.ReadLine()) != null)
			{
				comp_array = Regex.Split(line, " ");
				//Console.WriteLine(line);
				for (int i = 0; i <= 3; i++)
				{
					Regex rgx = new Regex(reg_express[i]);
					bool match = rgx.IsMatch(Convert.ToString(comp_array[i]));

					if (match == false)
					{
						errors.Add(comp_array[0]);
						line_errors.Add(comp_array[i + 1]);
					}
				}
				counter++;



				file.Close();

				if (errors != null)
				{
					Console.WriteLine("Le fichier contient {0} erreur(s)  de format.", errors.Count);
					for (int i = 0; i < errors.Count; i++)
					{
						Console.WriteLine("Ligne {0} erreur de format : {1} ", errors[i], line_errors[i]);
					}
				}

				else
				{ Console.WriteLine("Le fichier ne contient pas d'erreurs."); }

				// Suspend the screen.  
				Console.ReadLine();
			}
		}*/


	}
}
