using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class QuaiTarifs
    {
        string idqt;
        string idquai;
        string devise;
        int minimum;
        int maximum;
        int unite;
        decimal montant;
        string pUD;
        
        public QuaiTarifs[] getIdQuaiTarifs(QuaiTarifs[] getQuaiTarifs, string idquai)
        {
            QuaiTarifs[] q = getQuaiTarifs.Where(s => s.Idquai == idquai).ToArray();
            return q;
        }
     
        
        public QuaiTarifs[] getQuaiTarifs(SqlConnection c)
        {
            string requete = "select * from QuaiTarifs ";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new QuaiTarifs(), requete, c);
                QuaiTarifs[] f = new QuaiTarifs[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (QuaiTarifs)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Idqt
        {
            get
            {
                return idqt;
            }

            set
            {
                idqt = value;
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

        public string PUD
        {
            get
            {
                return pUD;
            }

            set
            {
                pUD = value;
            }
        }

        public int Minimum
        {
            get
            {
                return minimum;
            }

            set
            {
                minimum = value;
            }
        }

        public int Maximum
        {
            get
            {
                return maximum;
            }

            set
            {
                maximum = value;
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
    }
}