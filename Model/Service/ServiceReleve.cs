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
				CapteurID = r.capteurID,
				DateReleve = r.dateReleve
				
				//Capteur = r.capteur.Select(c => new BusinessCapteur 
				//{
				//	IdCapteur = c.idCapteur,
				//	NumeroSerie = c.numeroSerie,
				//	Libelle = c.libelle
				//}).FirstOrDefault()

			}).ToList();
			return result;
		}
		/*public static List<BusinessReleve> GetReleveWithLigneReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var result = (from ligneReleve in ctx.ligneReleve
						  join releve in ctx.releve
						  on ligneReleve.releveID equals releve.idReleve
						  //where m1.FirstName == "KEN"
						  select new BusinessReleve
						  {
							  IdReleve = releve.idReleve,
							  CapteurID = releve.capteurID,
							  LigneReleves = new List<BusinessLigneReleve>().Add(new BusinessLigneReleve//()
							  {
								  IdLigneReleve = ligneReleve.idLigneReleve,
								  DateLigneReleve = ligneReleve.dateLigneReleve,
								  Temperature = ligneReleve.temperature,
								  Hygrometrie = ligneReleve.hygrometrie,
								  HeureLigneReleve = ligneReleve.heureLigneReleve,
								  ReleveID = ligneReleve.releveID
							  })
							  
						  }); ;.ToList();
		
			return result;
		}*/

		public static int AddReleve(BusinessReleve businessReleve)
		{
			dataReportEntities ctx = new dataReportEntities();
			var releve = new releve
			{
				capteurID = businessReleve.CapteurID,
			};

			ctx.releve.Add(releve);

			ctx.SaveChanges();
			return releve.idReleve;
		}

	}

}
