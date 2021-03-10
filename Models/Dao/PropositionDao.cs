using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models.Dao
{
    public class PropositionDao
    {
        public Proposition[] getProposer(SqlConnection c)
        {
            try
            {

                Object[] select = new DbConnect().Select2(new Proposition(), "Propo", c);
                Proposition[] f = new Proposition[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Proposition)select[i];
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