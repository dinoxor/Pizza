using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using The_Ace_of_Spades_Pizza.Data_Access_Layer;
using The_Ace_of_Spades_Pizza.Models;

namespace The_Ace_of_Spades_Pizza.Controllers
{
    public class OrdersController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Pizza).OrderByDescending(x => x.OrderCreateddDateTime);
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            var pizzas = new SelectList(db.Pizzas, "PizzaId", "Name");

            var model = new OrderCreateModel();

            var selectedPizzaList = GetSelectListItems(db.Pizzas);

            model.Pizzas = selectedPizzaList;
            model.DeliveryArrivalDateTime = DateTime.Now.AddHours(1);

            model.Quantity = 1;

            return View(model);
        }


        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<Pizza> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.PizzaId.ToString(),
                    Text = element.Name
                });
            }

            return selectList;
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderCreateModel postedModel)
        {
            var model = new OrderCreateModel
            {
                Pizzas = GetSelectListItems(db.Pizzas)
            };

            int pizzaId = 0;
            Int32.TryParse(postedModel.PizzaId, out pizzaId);

            postedModel.Customer.FirstName = postedModel.Customer.FirstName.Trim();
            postedModel.Customer.LastName = postedModel.Customer.LastName.Trim();

            if (ModelState.IsValid)
            {
                var customer = db.Customers.SingleOrDefault(x => x.FirstName == postedModel.Customer.FirstName
                                                                         && x.LastName == postedModel.Customer.LastName
                                                                         && x.PhoneNumber == postedModel.Customer.PhoneNumber);
                if (customer == null)
                {
                    customer = new Customer
                    {
                        FirstName = postedModel.Customer.FirstName,
                        LastName = postedModel.Customer.LastName,
                        PhoneNumber = postedModel.Customer.PhoneNumber
                    };

                    db.Customers.Add(customer);

                    db.SaveChanges();
                }

                var order = db.Orders.SingleOrDefault(x => x.CustomerId == customer.CustomerId && x.PizzaId == pizzaId);

                if (order == null)
                {
                    order = new Order
                    {
                        CustomerId = customer.CustomerId,
                        PizzaId = pizzaId,
                        OrderCreateddDateTime = DateTime.Now,
                        DeliveryArrivalDateTime = postedModel.DeliveryArrivalDateTime,
                        Quantity = postedModel.Quantity
                    };

                    db.Orders.Add(order);
                }
                else
                {
                    order.DeliveryArrivalDateTime = postedModel.DeliveryArrivalDateTime;
                    order.Quantity += postedModel.Quantity;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    model.isSuccessful = false;
                    model.ConfirmationMessage = "Error saving to database";
                }

                model.isSuccessful = true;
                model.ConfirmationMessage = "Successfully placed your order!";
            }
            else
            {
                model.isSuccessful = false;
                model.ConfirmationMessage = "Error: Please see validation messages above!";
            }

            return View(model);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
            ViewBag.PizzaId = new SelectList(db.Pizzas, "PizzaId", "Name", order.PizzaId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerId,PizzaId,Quantity,OrderCreateddDateTime")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
            ViewBag.PizzaId = new SelectList(db.Pizzas, "PizzaId", "Name", order.PizzaId);
            return View(order);
        }

        //// GET: Orders/Delete/5
        //public ActionResult Delete(string firstName, string lastName, string phoneNumber, int pizzaId)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order order = db.Orders.Find(id);
        //    if (order == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(order);
        //}

        //// POST: Orders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Order order = db.Orders.Find(id);
        //    db.Orders.Remove(order);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
