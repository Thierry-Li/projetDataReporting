using DataReporting.Model.Business;
using DataReporting.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace DataReporting.Model.Service
{
	public class ServiceCapteur
	{
		public static List<BusinessCapteur> GetCapteur()
		{
			dataReportEntities ctx = new dataReportEntities();
			var result = ctx.capteur.Select(c => new BusinessCapteur
			{
				IdCapteur = c.idCapteur,
				NumeroSerie = c.numeroSerie,
				Libelle = c.libelle
			}).ToList();
			return result;
		}
		public static BusinessCapteur GetCapteurByNumeroSerie(int numSerie)
		{
			dataReportEntities ctx = new dataReportEntities();
			var capteur = ctx.capteur.Select(c => new BusinessCapteur {
				IdCapteur = c.idCapteur,
				NumeroSerie = c.numeroSerie,
				Libelle = c.libelle
			}).Where(c => c.NumeroSerie == numSerie).FirstOrDefault();
			
			return capteur;
		}
		public static BusinessCapteur GetCapteurByReleveId(int id)
		{
			dataReportEntities ctx = new dataReportEntities();
			var resultCapteur = (from capteur in ctx.capteur
								 join releve in ctx.releve on capteur.idCapteur equals releve.capteurID
								 where releve.idReleve == id
								 select new BusinessCapteur
								 {
									 IdCapteur = capteur.idCapteur,
									 NumeroSerie = capteur.numeroSerie,
									 Libelle = capteur.libelle
								 }).FirstOrDefault();


			return resultCapteur;
		}

		public static capteur AddCapteur(BusinessCapteur businessCapteur)
		{
			dataReportEntities ctx = new dataReportEntities();
			var capteur = new capteur
			{
				numeroSerie = businessCapteur.NumeroSerie,
				libelle = businessCapteur.Libelle
			};

			ctx.capteur.Add(capteur);

			ctx.SaveChanges();
			return capteur;
		}

		public static void DeleteCapteur(BusinessCapteur businessCapteur)
		{
			dataReportEntities ctx = new dataReportEntities();
			var capteurRecupere = ctx.capteur.Include("releve").Where(c => c.idCapteur == businessCapteur.IdCapteur).FirstOrDefault();

			ctx.releve.RemoveRange(capteurRecupere.releve);
			ctx.capteur.Remove(capteurRecupere);

			ctx.SaveChanges();
		}


	}
}
