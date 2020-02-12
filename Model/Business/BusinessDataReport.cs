using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReporting.Model.Service
{
	public class BusinessDataReport
	{
		public int Numero {get; set;}
		public string Notes { get; set; }
		public int NumeroSerieCapteur { get; set; }
		public int TotalRecords { get; set; }

		public BusinessTemperature Temperature { get; set; }
		public BusinessHygrometrie Hygrometrie { get; set; }
		public BusinessDateAndTime DateAndTime { get; set; } 
	}
}
