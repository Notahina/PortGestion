using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models.Dao
{
    public class EvenementDAO
    {
        public Evenement[] getEvenementIdPrevQuai(SqlConnection c, string idprev,string idquai)
        {
            string requete = "SELECT * FROM Evenement where idpred='" + idprev + "'and idquai='"+idquai+"'";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Evenement(), requete, c);
                Evenement[] f = new Evenement[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Evenement)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Evenement[] getEvenementIdPrev(SqlConnection c, string idprev)
        {
            string requete = "SELECT * FROM Evenement where idpred='" + idprev + "' order by DateD  desc ";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Evenement(), requete, c);
                Evenement[] f = new Evenement[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Evenement)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public Evenement[] getEvenementNull(SqlConnection c,string idquai)
        {
            string requete="SELECT * FROM EvenementNull where idquai='"+idquai+"'";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Evenement(),requete, c);
                Evenement[] f = new Evenement[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Evenement)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}