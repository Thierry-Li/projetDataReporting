using System;
using System.ComponentModel.DataAnnotations;

namespace DataReporting.Model.Business
{
    public class BusinessReleve
    {
        [Key]
        public int IdReleve { get; set; }
        public DateTime DateReleve { get; set; }
        public TimeSpan HeureReleve { get; set; }
        public double Temperature { get; set; }
        public double Hygrometrie { get; set; }
        public virtual BusinessCapteur Capteur { get; set; }

    }
}
