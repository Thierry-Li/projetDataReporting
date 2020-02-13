using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReporting.Model.Service
{
    public class SuperGlobal
    {
        //global SN
        private static string v_GlobalSN = "";
        public static string GlobalSN
        {
            get { return v_GlobalSN; }
            set { v_GlobalSN = value; }
        }
        //global TempMin
        private static string v_GlobalTempMin = "";
        public static string GlobalTempMin
        {
            get { return v_GlobalTempMin; }
            set { v_GlobalTempMin = value; }
        }
        //global TempMax
        private static string v_GlobalTempMax = "";
        public static string GlobalTempMax
        {
            get { return v_GlobalTempMax; }
            set { v_GlobalTempMax = value; }
        }
        //global TempaVG
        private static string v_GlobalTempAVG = "";
        public static string GlobalTempAVG
        {
            get { return v_GlobalTempAVG; }
            set { v_GlobalTempAVG = value; }
        }
        //global TotalRecords
        private static string v_GlobalTotalRecords = "";
        public static string GlobalTotalRecords
        {
            get { return v_GlobalTotalRecords; }
            set { v_GlobalTotalRecords = value; }
        }
        //global HygroMin
        private static string v_GlobalHygroMin = "";
        public static string GlobalHygroMin
        {
            get { return v_GlobalHygroMin; }
            set { v_GlobalHygroMin = value; }
        }
        //global HygroMax
        private static string v_GlobalHygroMax = "";
        public static string GlobalHygroMax
        {
            get { return v_GlobalHygroMax; }
            set { v_GlobalHygroMax = value; }
        }
        //global HygroAVG
        private static string v_GlobalHygroAVG = "";
        public static string GlobalHygroAVG
        {
            get { return v_GlobalHygroAVG; }
            set { v_GlobalHygroAVG = value; }
        }
        //global StorageInterval
        private static string v_GlobalStorageInterval = "";
        public static string GlobalStorageInterval
        {
            get { return v_GlobalStorageInterval; }
            set { v_GlobalStorageInterval = value; }
        }
        //global StartDate
        private static string v_GlobalStartDate = "";
        public static string GlobalStartDate
        {
            get { return v_GlobalStartDate; }
            set { v_GlobalStartDate = value; }
        }
        //global EndDate
        private static string v_GlobalEndDate = "";
        public static string GlobalEndDate
        {
            get { return v_GlobalEndDate; }
            set { v_GlobalEndDate = value; }
        }
        //global ElapsedDate
        private static string v_GlobalElapsedDate = "";
        public static string GlobalElapsedDate
        {
            get { return v_GlobalElapsedDate; }
            set { v_GlobalElapsedDate = value; }
        }

        //global PathFile
        private static string v_GlobalPath = "";
        public static string GlobalPath
        {
            get { return v_GlobalPath; }
            set { v_GlobalPath = value; }
        }

        //global Commentaire
        private static string v_GlobalComm = "";
        public static string GlobalComm
        {
            get { return v_GlobalComm; }
            set { v_GlobalComm = value; }
        }
    }
}
