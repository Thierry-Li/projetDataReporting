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
				Libelle = c.libelle,
				Releve = c.releve.Select(r => new BusinessReleve 
				{ 
					IdReleve = r.idReleve,
					DateReleve = r.dateReleve,
					HeureReleve = r.heureReleve,
					Temperature = r.temperature,
					Hygrometrie = r.hygrometrie
				}).FirstOrDefault()

			}).ToList();
			return result;
		}

		public static void DeleteCapteur(BusinessCapteur businessCapteur)
		{
			dataReportEntities ctx = new dataReportEntities();
			var capteurRecupere = ctx.capteur.Include("releve").Where(c => c.idCapteur == businessCapteur.IdCapteur).FirstOrDefault();

			ctx.releve.RemoveRange(capteurRecupere.releve);
			ctx.capteur.Remove(capteurRecupere);

			ctx.SaveChanges();
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

	}
}
