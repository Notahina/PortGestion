using PortGestion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortGestion.Controllers
{
    public class PrevisionDetailsController : Controller
    {
        // GET: PrevisionDetails
        public ActionResult insert()
        {
            string idprev = Request.QueryString["idprev"];
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                PrevisionDetails p = new PrevisionDetails();
                if (Request.HttpMethod == "POST")
                {
                    string idquai = Request.Form["idquai"];
                    string choix = Request.Form["Choix"];
                    string date = Request.Form["date"];
                    string time = Request.Form["time"];
                    string date2 = Request.Form["date2"];
                    string time2 = Request.Form["time2"];
                    DateTime dte1 = Convert.ToDateTime(date);
                    DateTime dte2 = Convert.ToDateTime(date2);
                    string insert = p.InsertPrevisionDetails(c, idprev, idquai, dte1, time, dte2, time2);
                }
                PrevisionDetails[] pdetails = p.getPrevisionDetails(c, idprev);
                ViewBag.Details = pdetails;
            }catch(Exception e)
            {
                ViewBag.Erreur = e.Message;
                throw e;
            }
            finally
            {
                c.Close();
            }
            
                return View();
        }
    }
}