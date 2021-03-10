using PortGestion.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class FinProposition
    {
        string idprop;
        string idpred;
        string idquai;
        string name;
        DateTime dateentre;
        DateTime datesortie;
        public FinProposition[] getQuiProposition(SqlConnection c,Prevision[] prev,string idquai)
        {
            try
            {
                Quai quai = (new Quai()).getQuaiId(c, idquai);
                List<FinProposition> proposition = new List<FinProposition>();
                List<FinProposition> pro= new List<FinProposition>();
                for (int i = 0; i < prev.Length; i++)
                {
                    if (i < quai.Nbre)
                    {
                        DateTime entre = prev[i].Datep;
                        DateTime sortie= entre.AddMinutes(prev[i].Duree);
                        FinProposition fprop = new FinProposition();
                        fprop.Idquai = idquai;
                        fprop.Idpred = prev[i].Idpred;
                        fprop.Name = prev[i].Name;
                        fprop.Dateentre = entre;
                        fprop.Datesortie = sortie;
                        proposition.Add(fprop);
                        pro.Add(fprop);
                    }
                    else
                    {
                        proposition = proposition.OrderBy(s => s.Datesortie).ToList();
                        for (int k = 0; k < proposition.Count; k++)
                        {
                            if (prev[i].Datep <= proposition.ElementAt(k).Datesortie)
                            {
                                DateTime entre = proposition.ElementAt(k).Datesortie;
                                DateTime sortie = entre.AddMinutes(prev[i].Duree);
                                FinProposition fprop = new FinProposition();
                                fprop.Idquai = idquai;
                                fprop.Idpred = prev[i].Idpred;
                                fprop.Name = prev[i].Name;
                                fprop.Dateentre = entre;
                                fprop.Datesortie = sortie;
                                proposition.Add(fprop);
                                pro.Add(fprop);
                            }
                            else
                            {
                                DateTime entre = prev[i].Datep; 
                                DateTime sortie = entre.AddMinutes(prev[i].Duree);
                                FinProposition fprop = new FinProposition();
                                fprop.Idquai = idquai;
                                fprop.Idpred = prev[i].Idpred;
                                fprop.Name = prev[i].Name;
                                fprop.Dateentre = entre;
                                fprop.Datesortie = sortie;
                                proposition.Add(fprop);
                                pro.Add(fprop);
                            }
                            proposition.RemoveAt(k);
                            break;
                        }
                        
                    }
                }
                return pro.ToArray();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public FinProposition[] getProposition(SqlConnection c,string date)
        {
            DateTime dte = new Prevision().Getdatetime(date);
            DateTime dtenext = dte.AddDays(1);
            FinProposition[] p = new FinPropositionDao().getProposition(c);
            p = p.Where(s => s.Dateentre >= dte && s.Dateentre < dtenext).ToArray();
            p = p.OrderBy(s => s.Dateentre).ToArray();
            return p;
        }
        public FinProposition VerifyDoublant(SqlConnection c,string idprev)
        {
            FinProposition[] p = new FinPropositionDao().getProposition(c);
            FinProposition res= p.Where(s=> s.Idpred==idprev).LastOrDefault();
            return res;
        }
        public FinProposition[] AjoutDateHeure(FinProposition[] p,Prevision[] prev)// Pevision order by date
        {
            List<FinProposition> fp = new List<FinProposition>();
            for(int i = 0; i < prev.Length; i++)
            {
                FinProposition f = this.getPropositionIdPrev(p,prev[i].Idpred);
                FinProposition fprop = new FinProposition();
                fprop.Idquai = f.Idquai;
                fprop.Idpred = prev[i].Idpred;
                fprop.Name = prev[i].Name;
                DateTime entre ;
                if (i == 0)
                {
                    entre= prev[i].Datep;
                }
                else
                {
                    int size = fp.Count - 1;
                    entre=fp.ElementAt(size).Datesortie;
                    if (entre < prev[i].Datep)
                    {
                        entre = prev[i].Datep;
                    }else
                    {
                        entre = entre.AddMinutes(10);
                    }
                    
                }
                DateTime sortie = entre.AddMinutes(prev[i].Duree);
                fprop.Dateentre =entre;
                fprop.Datesortie = sortie;
                fp.Add(fprop);
            }
            return fp.ToArray();
        }
        public FinProposition getPropositionIdPrev(FinProposition[] fp,string idprev)
        {
            return fp.Where(s => s.Idpred == idprev).LastOrDefault();
        }
        public string insert(FinProposition fp)
        {
            fp.Idprop = "CONCAT('Pro_',NEXT VALUE FOR idpro)";
            return new DbConnect().Generateinsert(fp,"Proposition");
        }
        public string Idprop
        {
            get
            {
                return idprop;
            }

            set
            {
                idprop = value;
            }
        }

        public string Idpred
        {
            get
            {
                return idpred;
            }

            set
            {
                idpred = value;
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

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public DateTime Dateentre
        {
            get
            {
                return dateentre;
            }

            set
            {
                dateentre = value;
            }
        }

        public DateTime Datesortie
        {
            get
            {
                return datesortie;
            }

            set
            {
                datesortie = value;
            }
        }
    }
}