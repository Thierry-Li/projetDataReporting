using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReporting.Model.Business
{
	public class BusinessLigneReleve
	{
		[Key]
		public int IdLigneReleve { get; set; }
		public DateTime DateLigneReleve { get; set; }
		public TimeSpan HeureLigneReleve { get; set; }
		public double Temperature { get; set; }
		public double Hygrometrie { get; set; }
		public int ReleveID { get; set; }
		public virtual BusinessReleve Releve { get; set; }


	}
}
