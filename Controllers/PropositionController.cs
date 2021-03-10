using PortGestion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortGestion.Controllers
{
    public class PropositionController : Controller
    {
        public void UpdateProposition()
        {
            string idprev = Request.QueryString["idprev"];

        }
        // GET: Proposition
        public ActionResult Liste()
        {
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                string date = "";
                if (Request.HttpMethod == "POST")
                {
                    date = Request.Form["date"];
                    FinProposition[] prop = new FinProposition().getProposition(c, date);
                    Quai quai = (new Quai()).getQuaiId(c, prop[0].Idquai);
                    ViewData["nbre"] = quai.Nbre;
                    ViewData["proposition"] = prop;
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
        }
        public ActionResult Propos()
        {
            return View();
        }
    }
}