using PortGestion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortGestion.Controllers
{
    public class TarifsController : Controller
    {
        // GET: Tarifs
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Voir()
        {
            string idprev = Request.QueryString["idprev"];
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
              
                Prevision prev = new Prevision();
                Tarifs[] penalite = prev.getTarif(c, idprev);
               
                decimal total = prev.getFacture(c,penalite);
                
                ViewData["Tarifs"] = penalite;
                ViewBag.Total = total;
                
            }
            catch (Exception e)
            {
                ViewData["error"] = e.Message;
            }
            finally
            {
                c.Close();
            }
            return View();
        }
    }
}