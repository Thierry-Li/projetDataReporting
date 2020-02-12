using System;
using System.Collections.Generic;
using System.Linq;

using DataReporting.Model.Business;
using DataReporting.Model.Data;

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
                CapteurID = r.capteurID,
                DateReleve = r.dateReleve
            }).ToList();
            return result;
        }
        public static BusinessReleve GetReleveById(int id)
        {
            dataReportEntities ctx = new dataReportEntities();
            var result = ctx.releve.Select(r => new BusinessReleve
            {
                IdReleve = r.idReleve,
                CapteurID = r.capteurID,
                DateReleve = r.dateReleve
            }).Where(releve => releve.IdReleve == id).FirstOrDefault();
            return result;
        }


        public static List<BusinessReleve> GetReleveByCapteurId(int capteurID)
        {
            dataReportEntities ctx = new dataReportEntities();
            var result = ctx.releve.Select(r => new BusinessReleve()
            {
                IdReleve = r.idReleve,
                CapteurID = r.capteurID,
                DateReleve = r.dateReleve
            }).Where(releve => releve.CapteurID == capteurID).ToList();
            return result;
        }


        public static BusinessReleve AddReleve(BusinessReleve businessReleve)
        {
            dataReportEntities ctx = new dataReportEntities();
            var releve = new releve
            {
                capteurID = businessReleve.CapteurID,
                dateReleve = DateTime.Now
            };

            ctx.releve.Add(releve);
            ctx.SaveChanges();

            businessReleve.IdReleve = releve.idReleve;
            return businessReleve;
        }

        public static void DeleteReleve(BusinessReleve businessReleve)
        {
            dataReportEntities ctx = new dataReportEntities();
            var LigneReleveRecupere = ctx.ligneReleve.Include("releve").Where(l => l.releveID == businessReleve.IdReleve).ToList();
            ctx.ligneReleve.RemoveRange(LigneReleveRecupere);
            ctx.releve.Remove(ctx.releve.Where(r => r.idReleve == businessReleve.IdReleve).FirstOrDefault());

            ctx.SaveChanges();
        }

    }

}
