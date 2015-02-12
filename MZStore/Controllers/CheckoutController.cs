using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MZStore.Models;

namespace MZStore.Controllers
{
    public class CheckoutController : Controller
    {
       
        // GET: Checkout
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        
        public ActionResult Complete(Order ob)
        {
            MusicStoreDBEntities db = new MusicStoreDBEntities();
            int err=1;
            Order oid = new Order();
            oid.OrderDate = System.DateTime.Now;
            oid.FirstName = ob.FirstName;
            oid.LastName = ob.LastName;
            oid.Address = ob.Address;
            oid.City = ob.City;
            oid.PostalCode = ob.PostalCode;
            oid.State = ob.State;
            oid.Country = ob.Country;
            oid.Phone = ob.Phone;
            oid.Email = ob.Email;
            oid.Total = int.Parse(Session["Total"].ToString());
            db.Orders.Add(oid);
            int res=db.SaveChanges();
            if(res==1)
            {
               err= Orders(oid);
            }
            if(err==0)
            {
                ViewData["suc"] = "success";
                Session["CartId"] = null;
            }
            return View();
        }

        [NonAction]
        public int Orders(Order od)
        {
            MusicStoreDBEntities db = new MusicStoreDBEntities();
            OrderDetail odts = new OrderDetail();
            string s = Session["CartId"].ToString();
            var q1 = from c1 in db.Carts where c1.CartId == s select c1;
           List<Cart> cl= q1.ToList();
            
            foreach(Cart c in cl)
            {
                odts.OrderId = od.OrderId;
                odts.AlbumId = c.AlbumId;
                odts.Quantity = c.Count;
                odts.UnitPrice = c.Album.Price;
                db.OrderDetails.Add(odts);
            }
            db.SaveChanges();
           ViewData["orderdetailId"]= odts.OrderDetailId;
            return 0;
        }
    }
}