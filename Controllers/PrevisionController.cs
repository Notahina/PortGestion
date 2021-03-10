using PortGestion.Models;
using PortGestion.Models.Dao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortGestion.Controllers
{
    public class PrevisionController : Controller
    {
        // GET: Prevision
        public ActionResult Index()
        {
            return View();
        }
       /* public ActionResult Update()
        {
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                string idprev = Request.QueryString["idprev"];
                if (Request.HttpMethod == "POST")
                {
                    
                    string date = Request.Form["date"];
                    string heure = Request.Form["heure"];
                    string sortie = Request.Form["sortie"];
                    Prevision prev = new Prevision();
                    string update = prev.updatePrevisionEffective(idprev, date, heure, sortie);
            ///Update prevision
                    PrevisionDao daoprev = new PrevisionDao();
                    daoprev.UpdatePrevision(c, update);
                ///Get nouveau prevision
                    Prevision[] nouvprev = prev.getPrevision(c, date);
                    ///Get ancien Proposition & if exist updte & else insert
                    FinProposition[] fn = new FinProposition().getProposition(c, date);
                    FinProposition exist = new FinProposition().getPropositionIdPrev(fn, idprev);
                    if (exist != null)
                    {
                        FinProposition[] nouveau = new FinProposition().AjoutDateHeure(fn, nouvprev);
                        FinPropositionDao dao = new FinPropositionDao();
                        dao.UpdateProposition(c, nouveau);
                    }else
                    {
                        Response.Write("Cette prevision n'as pas encore de proposisiton");
                        Response.Write("<a href='/Prevision/liste' >Insert Quai</a>");
                    }
                    
                }
            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                c.Close();
            }
                return View();   
        }*/
        public ActionResult insert()
        {
            DbConnect db= new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                Cargaison[] cargaison = new Cargaison().getCargaison(c);
                ViewData["cargaison"] = cargaison;
                if (Request.HttpMethod == "POST")
                {
                    string date= Request.Form["date"];
                    string heure = Request.Form["heure"];
                    string sambo = Request.Form["sambo"];
                    string idcarg = Request.Form["carg"];
                    
                    string longu = Request.Form["long"];
                    string prof = Request.Form["prof"];
                    Prevision prev = new Prevision();
                    string dateheure = date + " " ;
                    string dateD = Request.Form["date2"];
                    string heureD = Request.Form["heure2"];
                    //Classe
                    prev.IdCarg = idcarg;
                    prev.Name = sambo;
                    prev.Longueur = prev.getfloat(longu);
                    
                    prev.Prof = prev.getfloat(prof);
                    prev.Heure = heure;
                //Controlle date
                    prev.Datep= prev.Getdatetime(date);
                    prev.SetHeure(heure);
                    
                    //Duree 
                    prev.Duree =0;
                    //Insert 
                    //Boolean b = prev.DoublanName(c, date, sambo);
                    string ins = prev.Insert(prev);
                    Response.Write(ins);
                    db.Insert2(ins,c);

                }
              
            }catch(Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                c.Close();
            }
            return View();
        }
        public ActionResult liste()
        {
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    string date = Request.Form["date"];
                    Prevision[] prev = new Prevision().getPrevision(c, date);
                    ViewData["prevision"] = prev;
                }
            }
            catch (Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                c.Close();
            }
                return View();
        }
        public ActionResult propos()
        {
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    string idquai = Request.Form["idquai"];
                    string idprev = Request.Form["idprev"];
                    string date = Request.Form["date"];
                    //Response.Write(idquai);
                    //Response.Write(idprev);
                    string[] tabquai = idquai.Split(',');
                    string[] tabprev = idprev.Split(',');
                    string[] dte = date.Split(',');
                    string[] dte2 = dte[0].Split(' ');
                    List<FinProposition> pr = new List<FinProposition>(); 
                    for(int i = 0; i < tabprev.Length; i++)
                    {
                        FinProposition nv = new FinProposition();
                        nv.Idpred = tabprev[i];
                        nv.Idquai = tabquai[0];
                        pr.Add(nv);
                    }
                    FinProposition[] res = pr.ToArray();
                    Prevision[] prev = new Prevision().getPrevision(c, dte2[0]);
                    //FinProposition[] valiny = new FinProposition().AjoutDateHeure(res,prev);
                    FinProposition[] valiny = new FinProposition().getQuiProposition(c, prev, tabquai[0]);
                    FinPropositionDao dao = new FinPropositionDao();
                    dao.MultiInsert(c,valiny);
                    Redirect("/Proposition/liste");
                    }
            } catch (Exception e)
            {
                Response.Write(e.Message);
            }
            finally
            {
                c.Close();
            }
            return View();
        }
    }
}