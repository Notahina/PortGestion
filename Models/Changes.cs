using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Changes
    {
        string id;
        string devise;
        DateTime dates;
        decimal montant;
        public decimal getConversionDevise(SqlConnection c,string devise, float montant)
        {
            Changes[] changes = this.getChanges(c, devise);
            decimal valiny = changes[0].Montant * (decimal)montant;
            return valiny;
        }
        public void ifExist(SqlConnection c, string date,string devise)
        {
            string requete = "select * from changes Where Dates='"+date+"' and Devise='"+devise+"'";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Changes(), requete, c);
                if (select.Length != 0) throw new Exception("Cette Changes a ete deja saisissé");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Changes[] getChanges(SqlConnection c,string devise)
        {
            DateTime now = DateTime.Now;
            string dte = now.ToString("yyyy-MM-dd");
            string requete = "select * from Changes where dates<='" + dte + "' and devise='"+devise+"'order by dates desc    OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Changes(), requete, c);
                Changes[] f = new Changes[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Changes)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Changes[] getChanges(SqlConnection c)
        {
            DateTime now = DateTime.Now;
            string dte = now.ToString("yyyy-MM-dd");
            string requete = "select * from Changes where dates<='"+dte+"' order by dates desc    OFFSET 0 ROWS FETCH NEXT 2 ROWS ONLY";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Changes(), requete, c);
                Changes[] f = new Changes[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Changes)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Changes getChangeDevise(Changes[] chan,string devise)
        {
            Changes ch = chan.Where(s => s.Devise == devise).FirstOrDefault();
            return ch;
        }
        public string insert(SqlConnection sc,string devise,string date,string montant)
        {
            DateTime time = this.controlleDate(date);
            string  valeur = this.ControlleValeur(montant);
            string idval = "CONCAT('D',NEXT VALUE FOR idchange)";
            Changes c = new Changes();
            c.Id = idval;
            c.Devise = devise;
            c.Dates = time;
            //c.Montant = valeur;
            DbConnect db = new DbConnect();
            string format = db.formatDate(time);
            this.ifExist(sc, format, c.Devise);
            string ins = "INSERT INTO Changes VALUES ("+c.Id+",'"+c.Devise+"','"+format+"',"+valeur+")";
            return ins;
        }
        public DateTime controlleDate(string date)
        {
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(date);
            }
            catch(Exception e)
            {
                throw new Exception("Entre une date");
            }
            DateTime dem = DateTime.Now;
            string strdate = dem.ToString("dd/MM/yyyy");
            dem = Convert.ToDateTime(strdate);
            dem = dem.AddDays(1);
            TimeSpan time = dt - dem;
            int day = (int)time.TotalDays;
            if (day>=0) throw new Exception("Date ne peut etre date futur");
            return dt;
        }
        public string  ControlleValeur(string montant)
        {
            if (montant.Equals("")) throw new Exception("Veilliez remplir montant");
            string[] split = montant.Split(',');
            if (split.Length == 1) throw new Exception("Montant doit avoir deux chiffre aprex virgule ex:4000,00");
            decimal num1 = Convert.ToDecimal(split[0]);
            decimal num2 = Convert.ToDecimal(split[1]);
            decimal num = num1 + (num2 / 100);
            if (num1 < 0) throw new Exception("Le montant ne peut pas etre negatif");
            return num1+"."+num2;
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

        public DateTime Dates
        {
            get
            {
                return dates;
            }

            set
            {
                dates = value;
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
    }
}