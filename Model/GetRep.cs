using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;       

namespace Projet4.Model
{
    public class GetRep
    {
        //global chemin TXT
        private static string v_GlobalFilePassReleve = "";
        public static string FilePassReleve
        {
            get { return v_GlobalFilePassReleve; }
            set { v_GlobalFilePassReleve = value; }
        }
        //Global id capteur
        private static string v_GlobalIDCapteur = "";
        public static string GlobalIDCapteur
        {
            get { return v_GlobalIDCapteur; }
            set { v_GlobalIDCapteur = value; }
        }

    }


}
