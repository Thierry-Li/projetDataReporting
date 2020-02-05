using System.ComponentModel.DataAnnotations;

namespace DataReporting.Model.Business
{
	public class BusinessCapteur
	{
		[Key]
		public int IdCapteur { get; set; }
		public int NumeroSerie { get; set; }
		public string Libelle { get; set; }
		public virtual BusinessReleve Releve { get; set; }


	}
}
