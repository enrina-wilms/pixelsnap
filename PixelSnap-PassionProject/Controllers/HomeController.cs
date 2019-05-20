using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using PixelSnap_PassionProject.Models;
using PixelSnap_PassionProject.Models.ViewModels;

namespace PixelSnap_PassionProject.Controllers
{
    public class HomeController : Controller
    {
        private PixelSnapCMS db = new PixelSnapCMS();
        public ActionResult Index()
        {
            return View(db.Images.ToList());
        }

       
    }
}