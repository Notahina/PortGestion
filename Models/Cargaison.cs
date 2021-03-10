using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Cargaison
    {
        string idCarg;
        string type;
        float degagement;
        public Cargaison[] getCargaison(SqlConnection c)
        {
            try
            {
                Object[] select = new DbConnect().Select2(new Cargaison(), "Cargaison", c);
                Cargaison[] f = new Cargaison[select.Length];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = (Cargaison)select[i];
                }
                return f;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Cargaison getCargaison(SqlConnection c,string idCarg)
        {
            Cargaison[] get = this.getCargaison(c);
            return get.Where(s => s.IdCarg == idCarg).FirstOrDefault();
        }
        public string IdCarg
        {
            get
            {
                return idCarg;
            }

            set
            {
                idCarg = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public float Degagement
        {
            get
            {
                return degagement;
            }

            set
            {
                degagement = value;
            }
        }
    }
}