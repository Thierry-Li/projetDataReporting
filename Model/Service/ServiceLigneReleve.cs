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
				DateLigneReleve = r.dateLigneReleve,
				HeureLigneReleve = r.heureLigneReleve,
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
				HeureLigneReleve = r.heureLigneReleve,
				Temperature = r.temperature,
				Hygrometrie = r.hygrometrie,
				DateLigneReleve = r.dateLigneReleve,
				ReleveID = r.releveID

			}).Where(ligneReleve => ligneReleve.ReleveID == releveId).ToList();
			return result;
		}

		public static void AddLignesReleve(List<BusinessLigneReleve> businessLignesReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			foreach (var ligne in businessLignesReleve)
			{
				var ligneReleve = new ligneReleve
				{

					dateLigneReleve = ligne.DateLigneReleve,
					heureLigneReleve = ligne.HeureLigneReleve,
					temperature = ligne.Temperature,
					hygrometrie = ligne.Hygrometrie,
					releveID = ligne.ReleveID
				};
				ctx.ligneReleve.Add(ligneReleve);

			}

			ctx.SaveChanges();
		}

		//TODO A Faire
		public static void DeleteLigneReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var releveRecupere = ctx.releve.Where(r => r.idReleve == businessReleve.IdReleve).FirstOrDefault();
			
			ctx.releve.Remove(releveRecupere);

			ctx.SaveChanges();
		}

		
	}
}
