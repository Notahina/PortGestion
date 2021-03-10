using PortGestion.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PortGestion.Models
{
    public class Evenement
    {
        string idev;
        string idpred;
        string idquai;
        DateTime dateA;
        DateTime dateD;
        float tonne;
        public Evenement()
        {

        }
        public Evenement[] getEvenement(SqlConnection c,string idprev)
        {
            Evenement[] e = (new EvenementDAO()).getEvenementIdPrev(c, idprev);
            return e;
        }
        public void InsertSql(SqlConnection c,string requete)
        {
            DbConnect bd = new DbConnect();
            int val = bd.Insert2(requete, c);
        }
        public string Insert(SqlConnection c,string choix,string idprev,string idquai,string date,string heure,float poids)
        {
            
            Prevision p = new Prevision();
            DateTime datet = (new Prevision()).Getdatetime(date);
            Prevision prev = p.getIdPrevision(c, idprev);
            DateTime dateheure = this.SetHeure(datet, heure);
            if (choix.Equals("1"))
            {
                this.ControlleQuaiReturn(c,idquai,idprev);
                this.ControlleQuai(c, idquai, datet);
                this.ControlleDateA(dateheure, prev.Datep);
                this.ControlleDateA(c, idprev, dateheure);
                string id = "CONCAT('E',NEXT VALUE FOR idev)";
              
                string format = (new DbConnect()).formatDate(dateheure);
                string insert = "INSERT INTO Evenement VALUES("+id+",'"+idprev+"','"+idquai+"','"+format+"',NULL,"+poids+")";
                return insert;
            }else
            {

                string format = (new DbConnect()).formatDate(dateheure);
                this.ControlleExitDateA(c,idquai,idprev,dateheure);
                string update = "UPDATE Evenement set DateD='"+format+"' where idpred='"+idprev+"' and idquai='"+idquai+"' and DateD is null";
                return update;
            }
            
        }
        public void ControlleQuaiReturn(SqlConnection c, string idquai, string idprev)
        {
            Evenement[] even = (new EvenementDAO()).getEvenementIdPrevQuai(c, idprev, idquai);
            if (even.Length >= 1)
            {
                throw new Exception("le bateau ne peut pas revenir sur le quai");
            }
        }
        public void ControlleExitDateA(SqlConnection c,string idquai,string idprev,DateTime date)
        {
            Evenement[] even = (new EvenementDAO()).getEvenementIdPrevQuai(c,idprev,idquai);
            if (even.Length == 0)
            {
                throw new Exception("le bateau n'est pas  dans le quai");
            }else
            {
                if (even[0].DateA > date)
                {
                    throw new Exception("Votre date sortie est inferieur la date arrive du bateau");
                }
                
            }
        }
        public void ControlleDateA(DateTime date1,DateTime date2)//DateA & DAtePrevision
        {
            if (date1 < date2)
            {
                throw new Exception("Votre date est inferieru a la date effective ");
            }
        }
        public void ControlleDateA(SqlConnection c,string idprev,DateTime date1)//DAte arriver sy date Depart teo aloha
        {
            Evenement[] eve = (new EvenementDAO()).getEvenementIdPrev(c, idprev);
            int count = eve.Length;
            
            if (count != 0)
            {
                Evenement eve0 = eve[0];
                DateTime ini = new DateTime();
                if (ini == eve0.DateD)
                {
                    throw new Exception("Le bateau n'as pa encore quitter le quai");
                }
                if (date1 < eve0.DateD)
                {
                    throw new Exception("La date arrive est inferieur la date depart du precedent");
                }
            }
            
            
        }
        public void ControlleQuai(SqlConnection c,string idquai,DateTime date1)
        {
            try
            {
                Quai quai = (new Quai()).getQuaiId(c, idquai);
                Evenement[] evenull = (new EvenementDAO()).getEvenementNull(c, quai.Idquai);
                int nbre = evenull.Length;
                if (nbre == quai.Nbre)
                {
                    throw new Exception("le bateau n'est pas  dans le quai");//Le quai "+idquai+" est encore occuper
                }
            }catch(Exception e)
            {
                throw e;
            }
        }
        public DateTime SetHeure(DateTime date,string Heure)
        {
            string[] sub = Heure.Split(':');
            int h = Convert.ToInt32(sub[0]);
            int mn = Convert.ToInt32(sub[1]);
          //  int s = Convert.ToInt32(sub[2]);
            DateTime t = date;
            t =t.AddHours(h);
            t = t.AddMinutes(mn);
            //t= date.AddSeconds(s);
            return t;
        }
        public string Idev
        {
            get
            {
                return idev;
            }

            set
            {
                idev = value;
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

        public DateTime DateA
        {
            get
            {
                return dateA;
            }

            set
            {
                dateA = value;
            }
        }

        public DateTime DateD
        {
            get
            {
                return dateD;
            }

            set
            {
                dateD = value;
            }
        }

        public float Tonne
        {
            get
            {
                return tonne;
            }

            set
            {
                tonne = value;
            }
        }
    }
}