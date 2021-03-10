using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class PrevisionDetails
    {
        string id;
        string idprev;
        string idquai;
        DateTime dateA;
        DateTime dateD;
        public int getDuree(SqlConnection c,string idprev,string idquai)
        {
            PrevisionDetails[] get = this.getPrevisionDetails(c, idprev);
            PrevisionDetails p = get.Where(s => s.Idprev == idprev && s.Idquai == idquai).LastOrDefault();
            TimeSpan time = p.DateD - p.DateA;
            int val = (int)time.TotalMinutes;
            return val;
        }
        public string  InsertPrevisionDetails(SqlConnection c,string idprev,string idquai,DateTime datea ,string heure,DateTime dated,string heured)
        {
            DbConnect db = new DbConnect();
            Service ser = new Service();
            PrevisionDetails prevdet = new PrevisionDetails();
            prevdet.Id = "CONCAT('PRD',NEXT VALUE FOR idprevd)";
            prevdet.Idquai = idquai;
            prevdet.Idprev = idprev;
            DateTime dtA = ser.SetHeure(datea, heure);
            DateTime dtD = ser.SetHeure(dated,heured);
            prevdet.DateA = dtA;
            prevdet.DateD = dtD;
            string insert = db.Generateinsert(prevdet, "PrevisionDetails");
            db.Insert2(insert, c);
            return insert;
        }
        public PrevisionDetails[] getPrevisionDetails(SqlConnection c, string idprev)
        {
            try
            {
                PrevisionDetails[] prev;
                string requete = "SELECT * FROM  PrevisionDetails Where idprev='" + idprev + "'";
                Object[] select = new DbConnect().SelectRequete(new PrevisionDetails(), requete, c);
                prev = new PrevisionDetails[select.Length];
                for (int i = 0; i < prev.Length; i++)
                {
                    prev[i] = (PrevisionDetails)select[i];
                }
                return prev;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Idprev
        {
            get
            {
                return idprev;
            }

            set
            {
                idprev = value;
            }
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

        public DateTime DateA
        {
            get
            {
                return dateA;
            }

            set
            {
                dateA = value;
            }
        }

        public DateTime DateD
        {
            get
            {
                return dateD;
            }

            set
            {
                dateD = value;
            }
        }
    }
}