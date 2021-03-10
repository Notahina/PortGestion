using PortGestion.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Prevision
    {
        string idpred;
        string idCarg;
        string name;
        DateTime datep;
        string heure;
        int duree;
        float longueur;
        float prof;
        public Tarifs[] getTarifsQuai(SqlConnection c, string idprev, Evenement[] events)
        {

            Quai quai = new Quai();
            Tarifs[] valiny = new Tarifs[events.Length];
            for (int i = 0; i < events.Length; i++)
            {
               
                int dureeEvents = (new Service()).Diff2DateMinute(events[i].DateD, events[i].DateA);
               
                Tarifs tarif = new Tarifs();
                tarif.Idprev = idprev;
                tarif.Idquai = events[i].Idquai;
                Dictionary<string, decimal> get = quai.getQuaiTarif(c, events[i].Idquai);
                QuaiTarifs[] pe = quai.getQuaiTarifs(c, events[i].Idquai);
                    for (int k = 0; k < pe.Length; k++)
                    {
                    float montant = 0;
                    if (pe[k].PUD.Equals(""))
                    {
                            montant = (float)pe[k].Montant ;
                    }else
                    {
                        
                        decimal m = quai.getMontant(c, get, pe[k].PUD,pe[k].Idqt);
                        montant = (float)m;
                        get[pe[k].Idqt] =m;
                    }
                    
                        if (pe[k].Unite > 2)
                        {
                            
                            montant = this.getMontantPremier(pe[k].Minimum, pe[k].Maximum, pe[k].Unite, dureeEvents, montant);
                            tarif.Total = montant;
                            if (tarif.ControlleMaxduree(pe[k].Maximum, dureeEvents) == true) break;
                        }
                        if (pe[k].Unite == 1)
                        {
                            tarif.Total = tarif.Total + montant;
                            if (tarif.ControlleMaxduree(pe[k].Maximum, dureeEvents) == true) break;
                        }
                        if (pe[k].Unite < 0)
                        {
                            int dt = dureeEvents - pe[k].Minimum;
                            tarif.Total += (float)(dt * montant);
                        }
                        tarif.Devise = pe[k].Devise;
                    }
                    tarif.Durre = dureeEvents;
                tarif.Poids = events[i].Tonne;
                tarif.Total = tarif.Total* events[i].Tonne;
                valiny[i] = tarif;
            }
            return valiny;
        }
        public decimal getFacture(SqlConnection c,Tarifs[] getTarif)
        {
            Changes change = new Changes();
            decimal facture=0;
            for(int i = 0; i < getTarif.Length; i++)
            {
                float montant = getTarif[i].MontantPenalite + getTarif[i].Total;
                facture += change.getConversionDevise(c, getTarif[i].Devise, montant);
            }
            return facture;
        }
        public Tarifs[] getTarif(SqlConnection c,string idprev)
        {
            Evenement[] events = this.getEvenement(c, idprev);
            //Tarifs[] tPenality = this.getTarifsPenalite(c, idprev, events);
            /*Tarifs[] tPlafond = (new Tarifs()).EscalPlafond(c, events);
            for(int i = 0; i < tPenality.Length; i++)
            {
                for(int k = 0; k < tPlafond.Length; k++)
                {
                    if (tPlafond[k].Idquai == tPenality[i].Idquai)
                    {
                        tPlafond[k].MontantPenalite = tPenality[i].MontantPenalite;
                        tPlafond[k].DureePenalite = tPenality[i].DureePenalite;
                    }
                }
            }*/
            Tarifs[] tPlafond = this.getTarifsQuai(c, idprev, events);
            return tPlafond;
        }
        public Tarifs[] getTarifsPenalite(SqlConnection c,string idprev,Evenement[] events)
        {
            
            Quai quai = new Quai();
            Tarifs[] valiny = new Tarifs[events.Length];
            for(int i = 0; i< events.Length; i++)
            {
                int dureePrevDetail= this.getDureePrevsionDetails(c, idprev, events[i].Idquai);
                int dureeEvents = (new Service()).Diff2DateMinute(events[i].DateD, events[i].DateA);
                int d = dureeEvents - dureePrevDetail;
                Tarifs tarif = new Tarifs();
                tarif.Idprev = idprev;
                tarif.Idquai = events[i].Idquai;
                
                if (d > 0)
                {
                    Penalite[] pe = quai.getPenalite(c, events[i].Idquai);
                    for (int k=0; k < pe.Length; k++)
                    {
                        if (pe[k].Unite>2)
                        {
                            float montant = (float)pe[k].Montant;
                            montant = this.getMontantPremier(pe[k].Minim, pe[k].Maxim, pe[k].Unite, d, montant);
                            tarif.MontantPenalite = montant;
                            if (tarif.ControlleMaxduree(pe[k].Maxim, d) == true) break;
                        }
                        if (pe[k].Unite == 1)
                        {
                            tarif.MontantPenalite = tarif.MontantPenalite + (float)pe[k].Montant;
                            if (tarif.ControlleMaxduree(pe[k].Maxim, d) == true) break;
                        }
                        if (pe[k].Unite < 0)
                        {
                            int dt = duree - pe[i].Minim;
                            tarif.MontantPenalite +=  (float)(dt * pe[i].Montant);
                        }
                        tarif.Devise = pe[k].Devise;
                    }
                    tarif.DureePenalite = d;
                }else
                {
                    tarif.DureePenalite = 0;
                    tarif.MontantPenalite = 0;
                }
                valiny[i] = tarif;
            }
            return valiny;
        }
        
        public float getMontantPremier(int Min,int Max,int dureeUnite,int duree,float montant)
        {
            int d = 0;
            if (Max < 0 || Max > duree)
            {
                d = duree;
            }
            else if (Max < duree)
            {
                d = Max;
            }
            Service s = new Service();
            float val = s.CalculMontantPremier(dureeUnite, d, montant);
            return val;
        }
        public Evenement[] getEvenement(SqlConnection c, string idprev)
        {
            Evenement[] e = (new EvenementDAO()).getEvenementIdPrev(c, idprev);
            return e;
        }
        public PrevisionDetails[] getPrevisionDetails(SqlConnection c,string idprev)
        {
            PrevisionDetails[] prev = (new PrevisionDetails()).getPrevisionDetails(c, idprev);
            return prev;
        }
        public int getDureePrevsionDetails(SqlConnection c, string idprev, string idquai)
        {
            PrevisionDetails[] get = this.getPrevisionDetails(c, idprev);
            PrevisionDetails p = get.Where(s => s.Idprev == idprev && s.Idquai == idquai).LastOrDefault();
            TimeSpan time = p.DateD - p.DateA;
            int val = (int)time.TotalMinutes;
            return val;
        }
        public Prevision[] getPrevision(SqlConnection c,string date)
        {
            DateTime dte = this.Getdatetime(date);
            DateTime dtenext = dte.AddDays(1);
            Prevision[] p = new PrevisionDao().getPrevision(c);
            p = p.Where(s => s.Datep >= dte && s.Datep < dtenext).ToArray();
            p= p.OrderBy(s => s.Datep).ToArray();
            return p;
        }
        public Prevision getIdPrevision(SqlConnection c,string idprev)
        {
            Prevision f;
            try
            {
                string requete = "SELECT * FROM  Prevision Where idpred='" + idprev + "'";
                Object[] select = new DbConnect().SelectRequete(new Prevision(), requete, c);
                f = (Prevision)select[0];
                
            }
            catch (Exception e)
            {
                throw e;
            }
            return f;
        }
        public string Insert(Prevision prev)
        {
            string id = "CONCAT('P',NEXT VALUE FOR idpred)";
            prev.idpred = id;
            string insert = new DbConnect().Generateinsert(prev,"Prevision");
            return insert;
        }
        public void SetHeure(string Heure)
        {
            string[] sub = Heure.Split(':');
            int h = Convert.ToInt32(sub[0]);
            int mn= Convert.ToInt32(sub[1]);
            int s= Convert.ToInt32(sub[2]);
            this.datep=this.Datep.AddHours(h);
            this.datep =this.Datep.AddMinutes(mn);
            this.datep =this.Datep.AddSeconds(s);
        }
        public DateTime SetHeure(DateTime date,string Heure)
        {
            string[] sub = Heure.Split(':');
            int h = Convert.ToInt32(sub[0]);
            int mn = Convert.ToInt32(sub[1]);
            int s = Convert.ToInt32(sub[2]);
            date = date.AddHours(h);
            date = date.AddMinutes(mn);
            date = date.AddSeconds(s);
            return date;
        }
        public DateTime Getdatetime(string date)
        {
            DateTime d = Convert.ToDateTime(date);// "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime now = DateTime.Now;
            DateTime hier = now.AddDays(-1);
            int res = DateTime.Compare(d, hier);
            if (res < 0)
            {
                throw new Exception("Verifier votre date");
            }
            return d;
        }
        public float getfloat(string f)
        {
            try
            {
                float res = (float)Convert.ToDouble(f);
                return res;
            }catch(Exception e)
            {
                throw new Exception("Entre chiffre non des lettres");
            }
        }
        public  Boolean DoublanName(SqlConnection c,string date,string name)
        {
            try
            {
                Prevision[] prev = this.getPrevision(c, date);
                Prevision p = prev.Where(s => s.Name == name).LastOrDefault();
                if (p != null)
                {
                    throw new Exception("Cette Bateux existe deja");
                }
                return true;
            }catch(Exception e)
            {
                throw e;
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

        public DateTime Datep
        {
            get
            {
                return datep;
            }

            set
            {
                datep = value;
            }
        }
        public string Heure
        {
            get
            {
                return heure;
            }

            set
            {
                heure = value;
            }
        }
        public float Longueur
        {
            get
            {
                return longueur;
            }

            set
            {
                longueur = value;
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

        public int Duree
        {
            get
            {
                return duree;
            }

            set
            {
                duree = value;
            }
        }

        
    }
}