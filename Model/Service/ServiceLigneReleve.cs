using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataReporting.Model.Business;
using DataReporting.Model.Data;

namespace DataReporting.Model.Service
{
	public class ServiceLigneReleve
	{
		public static List<BusinessLigneReleve> GetLignesReleve()
		{
			dataReportEntities ctx = new dataReportEntities();
			var result = ctx.ligneReleve.Select(r => new BusinessLigneReleve
			{
				IdLigneReleve = r.idLigneReleve,
				DateLigneReleve = r.dateReleve,
				HeureLigneReleve = r.heureReleve,
				Temperature = r.temperature,
				Hygrometrie = r.hygrometrie,
				ReleveID = r.releveID,

			}).ToList();
			return result;
		}


		public static List<BusinessLigneReleve> GetLignesReleveById(int releveId)
		{
			dataReportEntities ctx = new dataReportEntities();
			var result = ctx.ligneReleve.Select(r => new BusinessLigneReleve()
			{
				IdLigneReleve = r.idLigneReleve,
				HeureLigneReleve = r.heureReleve,
				Temperature = r.temperature,
				Hygrometrie = r.hygrometrie,
				DateLigneReleve = r.dateReleve,
				ReleveID = r.releveID

			}).Where(ligneReleve => ligneReleve.ReleveID == releveId).ToList();
			return result;
		}

		public static BusinessReleve AddLigneReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var releve = new BusinessReleve
			{
				//dateReleve = businessReleve.DateReleve,
				//heureReleve = businessReleve.HeureReleve,
				//temperature = businessReleve.Temperature,
				//hygrometrie = businessReleve.Hygrometrie
			};

			//ctx.releve.Add(releve);

			ctx.SaveChanges();
			return releve;
		}

		public static void DeleteLigneReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var releveRecupere = ctx.releve.Where(r => r.idReleve == businessReleve.IdReleve).FirstOrDefault();
			//var releveRecupere = ctx.capteur.Include("capteur").Where(c => c.idCapteur == businessReleve.IdReleve).FirstOrDefault();

			//ctx.releve.RemoveRange(releveRecupere.releve);
			ctx.releve.Remove(releveRecupere);

			ctx.SaveChanges();
		}

		
	}
}
