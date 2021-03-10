using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Quai
    {
        string idquai;
        float tonne;
        float prof;
        int nbre;
        
        public decimal getMontant(SqlConnection c, Dictionary<string, decimal> valiny, string format,string idqt)
        {
            string replace = this.ReplaceFormatQuai(c, valiny, format,idqt);
            DataTable dt = new DataTable();

            var montant = dt.Compute(replace,"");
            decimal val=valiny[idqt]+ Convert.ToDecimal(montant);
            return val;
        }
        public string  ReplaceFormatQuai(SqlConnection c, Dictionary<string, decimal> valiny, string format,string idqt)
        {
            Service s = new Service();
           
            string[] getIdqt = s.getRegex(format);
            for(int i = 0; i < getIdqt.Length; i++)
            {
                string id = getIdqt[i];
                string val = s.NumberToString(valiny[id].ToString());
                format=format.Replace(id,val);
                valiny[idqt] = valiny[id];
            }
            return format;
        }
        public Dictionary<string, decimal> getQuaiTarif(SqlConnection c,string idquai)
        {
            Dictionary<string, decimal> valiny = new Dictionary<string, decimal>();
            QuaiTarifs[] get = this.getQuaiTarifs(c,idquai);
            for (int i = 0; i < get.Length; i++)
            {
                valiny.Add(get[i].Idqt, get[i].Montant);
            }

            return valiny;
        }
        public QuaiTarifs[] getQuaiTarifs(SqlConnection c,string idquai)
        {
            string requete = "select * from QuaiTarifs where idquai='"+idquai+"' order by minimum";
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
        public Penalite[] getPenalite(SqlConnection c,string idquai)
        {
            try
            {
                string requete = "select * from penalite where idquai='"+idquai+"' order by Minim";
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
        public Quai getQuaiId(SqlConnection c,string id)
        {
            Quai[] q = this.getQuai(c);
            Quai qui = q.Where(s => s.Idquai == id).LastOrDefault();
            return qui;
        }
        public Quai getQuaiId(Quai[] quai,string id)
        {
            Quai qui = quai.Where(s => s.Idquai == id).LastOrDefault();
            return qui;
        }
        public Quai[] getQuai(SqlConnection c)
        {
            try
            {

                Object[] select = new DbConnect().Select2(new Quai(), "Quai", c);
                Quai[] f = new Quai[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Quai)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
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

        public float Tonne
        {
            get
            {
                return tonne;
            }

            set
            {
                tonne = value;
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

        public int Nbre
        {
            get
            {
                return nbre;
            }

            set
            {
                nbre = value;
            }
        }

        
    }
}