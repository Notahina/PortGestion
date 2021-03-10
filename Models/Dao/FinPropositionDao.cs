using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models.Dao
{
    public class FinPropositionDao
    {
        public void UpdateProposition(SqlConnection c,FinProposition[] liste)
        {
            DbConnect db = new DbConnect();
            SqlCommand command = c.CreateCommand();
            SqlTransaction transaction;
            // transaction = c.BeginTransaction("SampleTransaction");
            command.Connection = c;
            try {

                for (int i = 0; i < liste.Length; i++)
                {
                    string dateE = db.formatDate(liste[i].Dateentre);
                    string dateS = db.formatDate(liste[i].Datesortie);
                    string update = "Update Proposition set Dateentre='" +dateE + "',Datesortie='" +dateS+ "' where idpred='" + liste[i].Idpred + "'";
                    command.CommandText =update;

                    command.ExecuteNonQuery();

                }
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        public FinProposition[] getProposition(SqlConnection c)
        {
            Boolean b = false;
            if (c == null)
            {
                c = new DbConnect().getConex();
                c.Open();
                b = true;
            }
            try
            {
                Object[] select = new DbConnect().Select2(new FinProposition(), "Proposition", c);
                FinProposition[] f = new FinProposition[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (FinProposition)select[i];
                }
                return f;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if (b == true) c.Close();
            }
        }
        public void MultiInsert(SqlConnection c,FinProposition[] liste)
        {
            Boolean b=false;
            if (c == null)
            {
                c = new DbConnect().getConex();
                c.Open();
                b = true;
            }
            SqlCommand command = c.CreateCommand();
            SqlTransaction transaction;
           // transaction = c.BeginTransaction("SampleTransaction");
            command.Connection = c;
            //command.Transaction = transaction;
            try
            {
                
             
                for(int i = 0; i < liste.Length; i++)
                {
                    FinProposition f = new FinProposition();
                    string sql = f.insert(liste[i]);
                    FinProposition res = f.VerifyDoublant(c,liste[i].Idpred);
                    if (res != null)
                    {
                        throw new Exception("La prevision " + liste[i].Idpred + "existe deja");
                        
                    }
                    command.CommandText = sql;

                    command.ExecuteNonQuery();
                }
                
                
              //  transaction.Commit();
            }
            catch(Exception e)
            {
                //transaction.Rollback();
                throw e;
            }
            finally
            {
                if (b == true) c.Close();
            }
        }
    }
}