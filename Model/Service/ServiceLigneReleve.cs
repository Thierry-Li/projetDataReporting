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
				Temperature = r.temperature,
				Hygrometrie = r.hygrometrie,
				DateLigneReleve = r.dateLigneReleve,
				ReleveID = r.releveID,
				HeureLigneReleve = r.heureLigneReleve
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

		public static void DeleteLigneReleve(BusinessLigneReleve businessLigneReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var ligneReleveRecupere = ctx.ligneReleve.Where(l => l.idLigneReleve == businessLigneReleve.IdLigneReleve).ToList();

			ctx.ligneReleve.RemoveRange(ligneReleveRecupere);
			ctx.SaveChanges();
		}

	}
}
