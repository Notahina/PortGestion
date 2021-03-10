using PortGestion.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Tarifs
    {
        string idfact;
        string idevent;
        string idprev;
        string idquai;
        string devise;
        int durre;
        int dureePenalite;
        float poids;
        float total;
        float montantPenalite;
        /*public Tarifs TotalPenalite(SqlConnection c,string idprev)
        {
            try
            {
                Tarifs t = new Tarifs();
                Penalite[] penal = (new Penalite()).getPenalite(c);
                Prevision prev = new Prevision();
                Prevision prevision = prev.getIdPrevision(c, idprev);
                DureeEvenement dE = (new DureeEvenement()).getDureePrev(c, idprev);
                int duree = dE.Duree - prevision.Duree;
                t.Idprev = idprev;
                t.Devise = penal[0].Devise;
                if (duree <= 0)
                {
                    t.Total = 0;
                }
                else
                {
                    t.Durre = duree;
                    for (int i = 0; i < penal.Length; i++)
                    {
                        if (penal[i].Unite > 2)
                        {
                            int du1 = penal[i].Unite;
                            int du2 = 0;
                            if (penal[i].Maxim < 0 || penal[i].Maxim > duree)
                            {
                                du2 = duree;
                            }
                            else if (penal[i].Maxim < duree)
                            {
                                du2 = penal[i].Maxim;
                            }
                            float tmontant = (float)penal[i].Montant;
                            t.Total = t.Total + tmontant;
                            t.tantque(du1, du2, t.Total);
                            if (penal[i].Maxim < 0 || penal[i].Maxim >= duree)
                            {
                                break;
                            }
                        }
                        if (penal[i].Unite == 1)
                        {
                            t.Total = t.Total + (float)penal[i].Montant;
                            if (penal[i].Maxim < 0 || penal[i].Maxim >= duree)
                            {
                                break;
                            }
                        }
                        if (penal[i].Unite < 0)
                        {
                            int dt = duree - penal[i].Minim;
                            t.Total = t.Total + (float)(dt * penal[i].Montant);
                        }
                        //t.Total = t.Total * prev.Longueur;
                    }
                }
                return t;
            }catch(Exception e)
            {
                throw e;
            }
        }*/
        public Boolean ControlleMaxduree(int Max,int duree)
        {
            if (Max < 0 || Max >= duree)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public decimal TotalEscal(SqlConnection c,Tarifs[] tarif)
        {
            try
            {
                Changes change = new Changes();
                Changes[] lchange = change.getChanges(c);
                decimal val = 0;
                for (int i = 0; i < tarif.Length; i++)
                {
                    if (tarif[i].Devise.Equals("Ariary"))
                    {
                        val = val + (decimal)tarif[i].Total;
                    }
                    Changes get = change.getChangeDevise(lchange, tarif[i].Devise);
                    if (get != null)
                    {
                        val = val + ((decimal)tarif[i].Total * get.Montant);
                    }
                }
                return val;
            }
            catch(Exception e)
            {
                throw e;
            }
           
        }
        /*public Tarifs[] EscalPlafond(SqlConnection c, Evenement[] even)
        {
            QuaiTarifs q = new QuaiTarifs();
            QuaiTarifs[] liste = q.getQuaiTarifs(c);
            List<Tarifs> fact = new List<Tarifs>();
            for (int i = 0; i < even.Length; i++)
            {
                QuaiTarifs[] li = q.getIdQuaiTarifs(liste, even[i].Idquai);
                TimeSpan time = even[i].DateD - even[i].DateA;
                int min = time.Minutes;
                int heure = time.Hours * 60;
                int duree = (int)time.TotalMinutes;
                Tarifs fd = new Tarifs();
                fd.Idfact = "";
                fd.Idevent = even[i].Idev;
                fd.Idprev = even[i].Idpred;
                fd.Idquai = even[i].Idquai;
                fd.Poids = even[i].Tonne;
                fd.Durre = duree;
                for(int k=0; k < li.Length; k++)
                {
                    if ((k + 1) == li.Length)
                    {
                        if (duree > li[i].Duree)
                        {
                            int reste = duree - li[k].Duree;
                            decimal montant = reste * li[k].Montant;
                            fd.Total =(float) montant+fd.Total;
                            fd.Devise = li[i].Devise;
                            break;
                        }
                    }
                    if(duree>li[k+1].Duree || duree <= li[k+1].Duree)
                    {
                        if (k == 0)
                        {
                            int temps = li[k + 1].Duree;
                            if (duree <= li[k + 1].Duree)
                            {
                                temps = duree;
                            }
                            int tempsquai = li[k].Duree;
                            float t = (float)li[k].Montant;
                            fd.tantque(tempsquai, temps, t);
                            fd.Devise = li[k].Devise;
                            if (duree <= li[k + 1].Duree) break;
                        } else
                        {
                            float t = fd.Total +(float)li[k].Montant;
                            fd.Total = t;
                            fd.Devise = li[k].Devise;
                            if (duree <= li[k + 1].Duree) break;
                        }
                    }
                }
                fd.Total = fd.Total * even[i].Tonne;
                fact.Add(fd);

            }
            return fact.ToArray();
        }
      /*  public Tarifs[] Escal(SqlConnection c,Evenement[] even)
        {
            Quai q = new Quai();
            Quai[] liste = q.getQuai(c);
            List<Tarifs> fact = new List<Tarifs>();
            for(int i = 0;i<even.Length;i++)
            {
                Quai qu = q.getQuaiId(liste, even[i].Idquai);
                TimeSpan time = even[i].DateD - even[i].DateA;
                int min = time.Minutes;
                int heure = time.Hours*60;
                int duree = (int)time.TotalMinutes;
                Tarifs fd = new Tarifs();
                fd.Idfact = "";
                fd.Idevent = even[i].Idev;
                fd.Idprev = even[i].Idpred;
                fd.Idquai = qu.Idquai;
                fd.Devise = qu.Devise;
                if (qu.Tonne>even[i].Tonne && qu.Duree>duree)
                {
                    fd.Durre =duree; ;
                    fd.Poids = even[i].Tonne;
                    fd.Total = qu.Montant;
                    fact.Add(fd);
                }
                else
                {
                    fd.Poids = even[i].Tonne;
                   
                    if (even[i].Tonne >= qu.Tonne)
                    {
                        int tq = (int)qu.Tonne;
                        int t = (int)even[i].Tonne;
                        fd.tantque(tq,t, qu.Montant);
                    }
                    if (duree > qu.Duree)
                    {
                        float val = fd.Total;
                        val = val + qu.Montant;                    
                        fd.tantque(qu.Duree, duree, qu.Montant);
                    }
                    fact.Add(fd);
                }
            }
            return fact.ToArray();
        }*/
        public void tantque(int quai,int events,float total){
            int val = -events;
            float t = 0;
            while ((-val) >= quai)
            {
                
                val = quai + val;
                
                t = total + t;
                this.Total = t;
            }
        }
       
        public string Idfact
        {
            get
            {
                return idfact;
            }

            set
            {
                idfact = value;
            }
        }

        public string Idevent
        {
            get
            {
                return idevent;
            }

            set
            {
                idevent = value;
            }
        }

        public string Idprev
        {
            get
            {
                return idprev;
            }

            set
            {
                idprev = value;
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

        public float Total
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public int Durre
        {
            get
            {
                return durre;
            }

            set
            {
                durre = value;
            }
        }

        public float Poids
        {
            get
            {
                return poids;
            }

            set
            {
                poids = value;
            }
        }

        public int DureePenalite
        {
            get
            {
                return dureePenalite;
            }

            set
            {
                dureePenalite = value;
            }
        }

        public float MontantPenalite
        {
            get
            {
                return montantPenalite;
            }

            set
            {
                montantPenalite = value;
            }
        }
    }
}