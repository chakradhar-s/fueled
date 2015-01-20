using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MZStore.Models;

namespace MusicStore.Controllers
{
    public class StoreManagerController : Controller
    {
        MusicStoreDBEntities db = new MusicStoreDBEntities();
        //
        // GET: /StoreManager/
        [HttpGet]
        public ActionResult Index()
        {
            List<Album> collection=db.Albums.ToList<Album>();
            return View(collection);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Album obj = db.Albums.SingleOrDefault(a1 => a1.AlbumId == id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Album type)
        {
            Album oldobj = db.Albums.SingleOrDefault(a1 => a1.AlbumId == type.AlbumId);

            oldobj.Title = type.Title;
            oldobj.CategoryName = type.CategoryName;
            oldobj.ArtistName = type.ArtistName;
            oldobj.Price = type.Price;

            db.SaveChanges();
            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public ActionResult Create()
        {
            Album obj = new Album();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Album obj)
        {
            db.Albums.Add(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Album obj = db.Albums.SingleOrDefault(a1 => a1.AlbumId == id);
            return View(obj);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
           Album obj = db.Albums.SingleOrDefault(a1 => a1.AlbumId == id);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Delete(String id)
        {
            int n = int.Parse(id);
            Album obj = db.Albums.SingleOrDefault(a1 => a1.AlbumId == n);
            db.Albums.Remove(obj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
