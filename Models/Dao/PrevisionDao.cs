using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models.Dao
{
    public class PrevisionDao
    {
        public void UpdatePrevision(SqlConnection c,string update)
        {
            SqlCommand command = c.CreateCommand();
            SqlTransaction transaction;
            // transaction = c.BeginTransaction("SampleTransaction");
            command.Connection = c;
            try
            {

 //               string dateheure = new DbConnect().formatDate(p.Datep);
//                string update = "Update Prevision set datep='" + dateheure + "' where idpred='" + p.Idpred + "'";
                command.CommandText = update;

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Prevision[] getPrevision(SqlConnection c)
        {
            string requete = "SELECT * FROM Prevision";
            try
            {

                Object[] select = new DbConnect().SelectRequete(new Prevision(), requete, c);
                Prevision[] f = new Prevision[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Prevision)select[i];
                }
                return f;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}