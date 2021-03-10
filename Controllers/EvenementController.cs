using PortGestion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortGestion.Controllers
{
    public class EvenementController : Controller
    {
        // GET: Evenement
        public ActionResult Index()
        {
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            string idprev = Request.QueryString["idprev"];
            try
            {
                if (Request.HttpMethod == "POST")
                {
                    string idquai = Request.Form["idquai"];
                    string choix = Request.Form["Choix"];
                    string date = Request.Form["date"];
                    string time = Request.Form["time"];
                    string poids = Request.Form["poids"];
               
               
                        Evenement e = new Evenement();
                        float tonne =(float)Convert.ToDouble(poids);
                        string insert = e.Insert(c,choix,idprev,idquai,date,time,tonne);
                        e.InsertSql(c, insert);
                        Response.Write(insert);
                      
                }
                Evenement[] even = (new Evenement()).getEvenement(c, idprev);
                ViewData["event"] = even;
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
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