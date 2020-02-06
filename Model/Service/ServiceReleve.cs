using DataReporting.Model.Business;
using DataReporting.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReporting.Model.Service
{
	public class ServiceReleve
	{
		public static List<BusinessReleve> GetReleve()
		{
			dataReportEntities ctx = new dataReportEntities();
			var result = ctx.releve.Select(r => new BusinessReleve
			{
				IdReleve = r.idReleve,
				DateReleve = r.dateReleve,
				HeureReleve = r.heureReleve,
				Temperature = r.temperature,
				Hygrometrie = r.hygrometrie,
				//Capteur = r.capteur.Select(c => new BusinessCapteur 
				//{
				//	IdCapteur = c.idCapteur,
				//	NumeroSerie = c.numeroSerie,
				//	Libelle = c.libelle
				//}).FirstOrDefault()

			}).ToList();
			return result;
		}

		public static void DeleteReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var releveRecupere = ctx.releve.Where(r => r.idReleve == businessReleve.IdReleve).FirstOrDefault();
			//var releveRecupere = ctx.capteur.Include("capteur").Where(c => c.idCapteur == businessReleve.IdReleve).FirstOrDefault();

			//ctx.releve.RemoveRange(releveRecupere.releve);
			ctx.releve.Remove(releveRecupere);

			ctx.SaveChanges();
		}

		public static releve AddReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var releve = new releve
			{
				dateReleve = businessReleve.DateReleve,
				heureReleve = businessReleve.HeureReleve,
				temperature = businessReleve.Temperature,
				hygrometrie = businessReleve.Hygrometrie
			};

			ctx.releve.Add(releve);

			ctx.SaveChanges();
			return releve;
		}

	}

}
