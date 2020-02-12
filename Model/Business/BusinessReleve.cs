using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataReporting.Model.Business
{
    public class BusinessReleve
    {
        [Key]
        public int IdReleve { get; set; }
        public int CapteurID { get; set; }
        public DateTime DateReleve { get; set; }
        public virtual BusinessCapteur Capteur { get; set; }
        public virtual List<BusinessLigneReleve> LigneReleves { get; set; }


    }
}
