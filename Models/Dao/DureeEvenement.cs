using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models.Dao
{
    public class DureeEvenement
    {
        string idpred;
        int duree;

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

        public int Duree
        {
            get
            {
                return duree;
            }

            set
            {
                duree = value;
            }
        }

        public DureeEvenement getDureePrev(SqlConnection c,string idprev)
        {
            string requete = "select * from DEnvent where idpred='"+idprev+"'";
            Object[] select = new DbConnect().SelectRequete(new DureeEvenement(), requete, c);
            DureeEvenement[] f = new DureeEvenement[select.Length];
            for (int i = 0; i < f.Length; i++)
            {
                f[i] = (DureeEvenement)select[i];
            }
            return f[0];
        }
    }
}