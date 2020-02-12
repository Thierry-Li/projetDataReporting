using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReporting.Model.Service
{
	public class BusinessDateAndTime
	{
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public TimeSpan StorageInterval { get; set; }
		public TimeSpan ElapsedTime { get; set; }


	}
}
