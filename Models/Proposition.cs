using PortGestion.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Proposition
    {
        string idquai;
        string idpred;
        string idcarg;
        string name;
        DateTime datep;
        float longueur;
        float prof;
        public Proposition[] getProposition(SqlConnection c, string date)
        {
            try 
            {
                Proposition[] proposer = new PropositionDao().getProposer(c);
                DateTime dte = new Prevision().Getdatetime(date);
                DateTime dtenext = dte.AddDays(1);
                proposer = proposer.Where(s => s.Datep >= dte && s.Datep < dtenext).ToArray();  
                proposer = proposer.OrderBy(s => s.Datep).ToArray();
                //List<Proposition> val = new List<Proposition>();
                
                //Proposition[] result =val.ToArray() ; 
                return proposer;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Proposition getPropositionIdquai(List<Proposition> prop, string idquai)
        {
            return prop.Where(s => s.Idquai == idquai).LastOrDefault();
        }
        public Proposition getPropositionIdprev(List<Proposition> prop, string idprev)
        {
            return prop.Where(s => s.Idpred == idprev).LastOrDefault();
        }
        public Proposition getProposer(Proposition[]prop,string idprev,string idquai)
        {
            Proposition p = prop.Where(s => s.Idpred == idprev && s.Idquai == idquai).LastOrDefault();
            if (p == null) throw new Exception("Ne peux se rejoindre");
            return p;
        }
        public string Idquai
        {
            get
            {
                return idquai;
            }

            set
            {
                idquai = value;
            }
        }

        public string Idpred
        {
            get
            {
                return idpred;
            }

            set
            {
                idpred = value;
            }
        }

        public string Idcarg
        {
            get
            {
                return idcarg;
            }

            set
            {
                idcarg = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public DateTime Datep
        {
            get
            {
                return datep;
            }

            set
            {
                datep = value;
            }
        }

        public float Longueur
        {
            get
            {
                return longueur;
            }

            set
            {
                longueur = value;
            }
        }

        public float Prof
        {
            get
            {
                return prof;
            }

            set
            {
                prof = value;
            }
        }

       
    }
}