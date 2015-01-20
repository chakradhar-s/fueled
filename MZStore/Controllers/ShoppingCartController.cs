using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MZStore.Models;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        MusicStoreDBEntities db = new MusicStoreDBEntities();
        string cartid=null;        
         //
        // GET: /ShoppingCart/                 
      
        public ActionResult Index()
        {           
            List<Cart> cList;
            //List<Album> selectedAlbums=new List<Album>();
            if (Session["CartId"] == null)
            {
                cList = null;
            }
            else
            {
                cartid=Session["CartId"].ToString();
                var q1 = from c1 in db.Carts
                         where c1.CartId == cartid
                         select c1;
                cList = q1.ToList();                
            }
            //foreach(Cart c2 in cList)
            //{
            //    Album a1 =  db.Albums.SingleOrDefault(x2 => x2.AlbumId == c2.AlbumId);
            //    selectedAlbums.Add(a1);
            //}
            //shop.MyAlbums = selectedAlbums;
            //shop.Mycart = cList;
          
            return View(cList);           
        }
        public ActionResult AddingCartItems(int almId,int quantity)
        {
            Cart obj = new Cart();
            if(Session["CartId"]!=null)
            {
               obj.CartId = Session["CartId"].ToString() ;
            }
            else
            {
                obj.CartId = GetCartId;
                Session["CartId"] = obj.CartId;
            }

            obj.AlbumId = almId;// int.Parse(Request.Form["id"]);
            obj.Count =  quantity;//int.Parse(Request.Form["quantity"]);
            obj.DateCreated = System.DateTime.Now;
            db.Carts.Add(obj);
            db.SaveChanges();           
            Session["return"] = true;
           Uri to= HttpContext.Request.UrlReferrer;              
           //to.Segments[1].TrimEnd('/'), to.Segments[2].TrimEnd('/'), to.Segments[3]
           //return RedirectToAction(to.Segments[2].TrimEnd('/'), to.Segments[1].TrimEnd('/'), new { id = to.Segments[3] });
           return RedirectToAction("Details", "Home", new { id = almId });          

        }
        public string GetCartId
        {
            get
            { 
                return Guid.NewGuid().ToString();                
            }                         
            
        }
        public ActionResult RemovingCart(int id)
        {
           // var q1 = from cq in db.Carts where cq.CartId == cmId && cq.AlbumId == almId select cq;
           Cart q1 =db.Carts.SingleOrDefault(ce =>ce.RecordId == id);
            db.Carts.Remove(q1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
