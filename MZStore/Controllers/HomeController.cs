using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MZStore.Models;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        MusicStoreDBEntities db = new MusicStoreDBEntities();
        
        //
        // GET: /Home/
        [HttpGet]

        public ActionResult Index()
        {
            var q1 = from a1 in db.Albums orderby a1.Price descending select a1;
            List<Album> alist = q1.Take(5).ToList();
            return View(alist);
        }
        [HttpGet]
        public ActionResult Category(string value)
        {
            List<Album> albm = db.Albums.ToList();
            IEnumerable<Album> details = from categry in albm
                          where categry.CategoryName==value
                          select categry;
            ViewBag.Category = value;
            return View(details);
        }
       
        public ActionResult Details(int id)
        {
            
           int itemId = id;
            Album obj = db.Albums.SingleOrDefault(a1 => a1.AlbumId == itemId);
            if (Session["return"]==null ||(bool)Session["return"]==false) 
            {
                ViewData["visted"] = false;
            }
            else
            ViewData["visted"]=true;
            Session["return"] = false;
            return View(obj);
        }
        public ActionResult Store()
        {
            IEnumerable<Album> collection =db.Albums.ToList<Album>();
            return View(collection);
        }
        
        

        
        

    }
}
