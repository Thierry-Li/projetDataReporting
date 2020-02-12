using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataReporting.Model.Business
{
	public class BusinessCapteur
	{
		[Key]
		public int IdCapteur { get; set; }
		public int NumeroSerie { get; set; }
		public string Libelle { get; set; }
		public virtual List<BusinessReleve> Releve { get; set; }


	}
}
