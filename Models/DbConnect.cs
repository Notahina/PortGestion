using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PortGestion.Models
{
    public class DbConnect
    {
        public SqlConnection getConex()
        {
            SqlConnection c = null;
            //String chaineConnexion = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Mick\documents\visual studio 2010\Projects\WebApplication2\WebApplication2\App_Data\Database1.mdf;Integrated Security=True;User Instance=True";
            String chaineConnexion = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Fianarana\S5\Dev\PortGestion\PortGestion\App_Data\base.mdf;Integrated Security=True";
            c = new SqlConnection(chaineConnexion);
            return c;
        }
        public string Generateinsert(Object o,string table)
        {
            string classe = o.GetType().Name;
            string insert = "INSERT INTO " + table + " VALUES (";
            Type t = o.GetType();
            FieldInfo[] fi = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            for (int i = 0; i < fi.Length; i++)
            {
                string nom = ((fi[i].Name).Substring(0, 1)).ToUpper() + (fi[i].Name).Substring(1);
                PropertyInfo method = t.GetProperty(nom);
                if (fi[i].FieldType == typeof(string))
                {
                    if (i == 0)
                    {
                        insert += "";
                        insert += method.GetValue(o) + ",";
                    }
                    else
                    {
                        insert += "'";
                        insert += method.GetValue(o) + "',";
                    }

                }
                if (fi[i].FieldType == typeof(DateTime))
                {
                    string val = this.formatDate((DateTime)method.GetValue(o));
                    insert += "'";
                    insert += val + "',";
                }
                if (fi[i].FieldType == typeof(int))
                {
                    insert += "" + method.GetValue(o) + ",";
                }
                if (fi[i].FieldType == typeof(decimal))
                {
                    decimal d = (decimal)method.GetValue(o);
                    insert += "" + d + ",";
                }
                if (fi[i].FieldType == typeof(float))
                {
                    insert += "" + method.GetValue(o) + ",";
                }
            }
            int isa = insert.Count();
            insert = insert.Substring(0, isa - 1) + ")";
            return insert;
        }
        public Object[] SelectRequete(Object a, string requete, SqlConnection connect)
        {
            Boolean boolean; SqlConnection sql;
            if (connect == null)
            {
                connect = new DbConnect().getConex();
                connect.Open();
                boolean = true;
            }
            else
            {
                boolean = false;
            }

            SqlConnection con = connect;
            StringBuilder builder = new StringBuilder();

            Type e = a.GetType();
            List<Object> tab = new List<Object>();

            String classn = e.Name;
            //    Console.Write(requete);

            SqlCommand req = new SqlCommand(requete, connect);
            FieldInfo[] liste = e.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            SqlDataReader reader = req.ExecuteReader();



            // MethodInfo method = e.GetMethod("Age", BindingFlags.Instance | BindingFlags.NonPublic);

            while (reader.Read())
            {
                object instance = Activator.CreateInstance(e);
                for (int i = 0; i < liste.Count(); i++)
                {
                    string nom = ((liste[i].Name).Substring(0, 1)).ToUpper() + (liste[i].Name).Substring(1);
                    PropertyInfo method = e.GetProperty(nom);
                    if (liste[i].FieldType == typeof(int))
                    {
                        String fsd = liste[i].Name;
                        //  object valeur = reader.GetInt32(reader.GetOrdinal(fsd));
                        int farmsize = Convert.ToInt32(reader[(liste[i].Name)]);
                        method.SetMethod.Invoke(instance, new Object[] { farmsize });

                    }
                    else if (liste[i].FieldType == typeof(string))
                    {
                        string str = reader[liste[i].Name].ToString();
                        method.SetMethod.Invoke(instance, new Object[] { str });


                    }
                    else if (liste[i].FieldType == typeof(DateTime))
                    {
                        DateTime date;
                      
                        if (reader[liste[i].Name] != DBNull.Value)
                        {
                            date = (DateTime)reader[liste[i].Name];
                        }
                        else
                        {
                            date = new DateTime();
                        }
                        
                        method.SetMethod.Invoke(instance, new Object[] { date });

                    }
                    else if (liste[i].FieldType == typeof(decimal))
                    {
                        //   decimal recievedDecimal = reader.GetDecimal(reader.GetOrdinal(liste[i].Name));
                        decimal result = Convert.ToDecimal(reader[liste[i].Name]);
                        method.SetMethod.Invoke(instance, new Object[] { result });


                    }
                    else if (liste[i].FieldType == typeof(float))
                    {
                        //   decimal recievedDecimal = reader.GetDecimal(reader.GetOrdinal(liste[i].Name));
                        float result = (float)Convert.ToDouble(reader[liste[i].Name]);
                        method.SetMethod.Invoke(instance, new Object[] { result });

                    }

                }
                tab.Add(instance);
            }
            reader.Close();
            if (boolean == true)
            {
                connect.Close();
            }
            Object[] finale = tab.ToArray();
            return finale;
        }
        public Object[] Select2(Object a, string table, SqlConnection connect)
        {
            Boolean boolean; SqlConnection sql;
            if (connect == null)
            {
                connect = new DbConnect().getConex();
                connect.Open();
                boolean = true;
            }
            else
            {
                boolean = false;
            }

            SqlConnection con = connect;
            StringBuilder builder = new StringBuilder();
            String requete = "";
            Type e = a.GetType();
            List<Object> tab = new List<Object>();

            String classn = e.Name;



            requete = "Select *  from " + table;

            //    Console.Write(requete);

            SqlCommand req = new SqlCommand(requete, connect);
            FieldInfo[] liste = e.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            SqlDataReader reader = req.ExecuteReader();



            // MethodInfo method = e.GetMethod("Age", BindingFlags.Instance | BindingFlags.NonPublic);

            while (reader.Read())
            {
                object instance = Activator.CreateInstance(e);
                for (int i = 0; i < liste.Count(); i++)
                {
                    string nom = ((liste[i].Name).Substring(0, 1)).ToUpper() + (liste[i].Name).Substring(1);
                    PropertyInfo method = e.GetProperty(nom);
                    if (liste[i].FieldType == typeof(int))
                    {
                        String fsd = liste[i].Name;
                        //  object valeur = reader.GetInt32(reader.GetOrdinal(fsd));
                        int farmsize = Convert.ToInt32(reader[(liste[i].Name)]);
                        method.SetMethod.Invoke(instance, new Object[] { farmsize });

                    }
                    else if (liste[i].FieldType == typeof(string))
                    {
                        string str = reader[liste[i].Name].ToString();
                        method.SetMethod.Invoke(instance, new Object[] { str });


                    }
                    else if (liste[i].FieldType == typeof(DateTime))
                    {
                        DateTime date = (DateTime)reader[liste[i].Name];
                        method.SetMethod.Invoke(instance, new Object[] { date });

                    }
                    else if (liste[i].FieldType == typeof(decimal))
                    {
                        //   decimal recievedDecimal = reader.GetDecimal(reader.GetOrdinal(liste[i].Name));
                        decimal result = Convert.ToDecimal(reader[liste[i].Name]);
                        method.SetMethod.Invoke(instance, new Object[] { result });


                    }
                    else if (liste[i].FieldType == typeof(float))
                    {
                        //   decimal recievedDecimal = reader.GetDecimal(reader.GetOrdinal(liste[i].Name));
                        float result =(float) Convert.ToDouble(reader[liste[i].Name]);
                        method.SetMethod.Invoke(instance, new Object[] { result });

                    }

                }
                tab.Add(instance);
            }
            reader.Close();
            if (boolean == true)
            {
                connect.Close();
            }
            Object[] finale = tab.ToArray();
            return finale;
        }
        public FieldInfo[] returnfield(object a)
        {
            Type e = a.GetType();
            Console.WriteLine(e.IsClass);
            FieldInfo[] fieldInfo = e.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            return fieldInfo;



        }

        public void Insert(Object cl)
        {
            StringBuilder builder = new StringBuilder();

            Type e = cl.GetType();
            String classn = e.Name;
            FieldInfo[] liste = this.returnfield(cl);
            String[] stockage = new String[liste.Count()];
            String op = "";
            for (int x = 0; x < liste.Count(); x++)
            {

                ///		str="get"+liste[x].Name.ToLower();
                MethodInfo method = e.GetMethod("get_" + liste[x].Name.ToLower());
                stockage[x] = this.transformation(method, liste[x], cl);
                stockage[x] = "'" + stockage[x] + "'" + ",";
                builder.Append(stockage[x]);
                builder.ToString();


                Console.WriteLine(builder);
            }
            String fs = builder.ToString();
            String requete = fs.Substring(0, fs.Count() - 1);


            String finale = "INSERT INTO " + classn + " Values" + "(" + requete + ")";
            DbConnect connect = new DbConnect();
            SqlConnection sql = connect.getConex();

            this.Insert2(finale, sql);
            //   return finale;
        }
        public int Insert2(String requete, SqlConnection connection)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.InsertCommand = new SqlCommand(requete, connection);
            dataAdapter.InsertCommand.ExecuteNonQuery();
            dataAdapter.InsertCommand.Dispose();
            //connection.Close();
            return 1;
        }
        /*public Utilsateur[] tableau1(String Nom,String Prenom,String Date1,String Date2) {
            String utilisateur = "Select * from utilisateur";
        
        }*/
        public Object[] RequeteUtilisateur(String requete, Object a, DbConnect connect)
        {
            Boolean boolean;
            if (connect == null)
            {
                connect = new DbConnect();
                boolean = true;
            }
            else
            {
                boolean = false;
            }

            SqlConnection con = connect.getConex();
            StringBuilder builder = new StringBuilder();
            Type e = a.GetType();
            List<Object> tab = new List<Object>();
            String classn = e.Name;
            SqlConnection sql = connect.getConex();
            sql.Open();
            SqlCommand req = new SqlCommand(requete, sql);
            FieldInfo[] liste = this.returnfield(a);
            SqlDataReader reader = req.ExecuteReader();



            // MethodInfo method = e.GetMethod("Age", BindingFlags.Instance | BindingFlags.NonPublic);

            while (reader.Read())
            {
                object instance = Activator.CreateInstance(e);
                for (int i = 0; i < liste.Count(); i++)
                {

                    MethodInfo method = e.GetMethod("set_" + liste[i].Name.ToLower());
                    if (liste[i].FieldType == typeof(int))
                    {
                        String fsd = liste[i].Name;
                        //  object valeur = reader.GetInt32(reader.GetOrdinal(fsd));
                        int farmsize = Convert.ToInt32(reader[(liste[i].Name)]);
                        method.Invoke(instance, new Object[] { farmsize });

                    }
                    else if (liste[i].FieldType == typeof(String))
                    {
                        String str = reader[liste[i].Name].ToString();
                        method.Invoke(instance, new Object[] { str });


                    }
                    else if (liste[i].FieldType == typeof(DateTime))
                    {
                        DateTime date = (DateTime)reader[liste[i].Name];
                        method.Invoke(instance, new Object[] { date });

                    }
                    else if (liste[i].FieldType == typeof(decimal))
                    {
                        //   decimal recievedDecimal = reader.GetDecimal(reader.GetOrdinal(liste[i].Name));
                        decimal result = Convert.ToDecimal(reader[liste[i].Name]);
                        method.Invoke(instance, new Object[] { result });

                    }

                }
                tab.Add(instance);
            }
            if (boolean == true)
            {
                sql.Close();
            }
            Object[] finale = tab.ToArray();
            return finale;
        }
        public String transformation(MethodInfo a, FieldInfo field, Object clas)
        {
            String s = "";
            if (field.FieldType == typeof(int))
            {
                int nombre = (int)a.Invoke(clas, null);
                s = "" + nombre + "";

            }
            else if (field.FieldType == typeof(String))
            {
                s = (String)a.Invoke(clas, null);
            }
            else if (field.FieldType == typeof(decimal))
            {
                decimal k = (decimal)a.Invoke(clas, null);
                s = "" + k + "";
            }
            else if (field.FieldType == typeof(DateTime))
            {
                DateTime k = (DateTime)a.Invoke(clas, null);
                s = "" + k + "";
            }
            return s;
        }
        public String formatDate(DateTime date)
        {
            int day = date.Day;
            int m = date.Month;
            int yyyy = date.Year;
            int h = date.Hour;
            int mn = date.Minute;
            int s = date.Second;
            string dte = yyyy + "-" + m + "-" + day+ " "+h+":"+mn+":"+s;
            return dte;
        }
    }
}