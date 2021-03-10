using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Penalite
    {
        string idpenalite;
        string idquai;
        int minim;
        int maxim;
        int unite;
        string devise;
        decimal montant;
        public Penalite[] getPenalite(SqlConnection c)
        {
            try
            {
                string requete = "select * from penalite";
                Object[] select = new DbConnect().SelectRequete(new Penalite(), requete, c);
                Penalite[] f = new Penalite[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Penalite)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Idpenalite
        {
            get
            {
                return idpenalite;
            }

            set
            {
                idpenalite = value;
            }
        }

        public int Minim
        {
            get
            {
                return minim;
            }

            set
            {
                minim = value;
            }
        }

        public int Maxim
        {
            get
            {
                return maxim;
            }

            set
            {
                maxim = value;
            }
        }

        public int Unite
        {
            get
            {
                return unite;
            }

            set
            {
                unite = value;
            }
        }

        public string Devise
        {
            get
            {
                return devise;
            }

            set
            {
                devise = value;
            }
        }

        public decimal Montant
        {
            get
            {
                return montant;
            }

            set
            {
                montant = value;
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
    }
}