using PortGestion.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortGestion.Controllers
{
    public class ChangesController : Controller
    {
        // GET: Changes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult insert()
        {
            DbConnect db = new DbConnect();
            SqlConnection c = db.getConex();
            c.Open();
            try
            {
                Changes change = new Changes();
                Changes[] jchange = change.getChanges(c);
                ViewBag.Jchanges = jchange;
                if (Request.HttpMethod == "POST")
                {
                    
                        string devise = Request.Form["devise"];
                        string date = Request.Form["date"];
                        string valeur = Request.Form["valeur"];
                        
                        string ins = change.insert(c, devise, date, valeur);
                        //Response.Write(ins);
                        db.Insert2(ins, c);

                                        
                }
            }catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                c.Close();
            }
            return View();
        }
    }
}