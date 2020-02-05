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

	}

}
