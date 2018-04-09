using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using The_Ace_of_Spades_Pizza.Data;
using The_Ace_of_Spades_Pizza.Data.Model;
using The_Ace_of_Spades_Pizza.Data.Repository;
using The_Ace_of_Spades_Pizza.Site.Models;

namespace The_Ace_of_Spades_Pizza.Site.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepository _orderRepository;
        private readonly PizzaRepository _pizzaRepository;
        private readonly CustomerRepository _customerRepository;
        
        public OrderController ()
        {
            _orderRepository = new OrderRepository();
            _pizzaRepository = new PizzaRepository();
            _customerRepository = new CustomerRepository();
        }

        // GET: Orders
        public ActionResult Index()
        {
            var orders = _orderRepository.GetAll().OrderByDescending(x => x.OrderCreateddDateTime);
            
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            //ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            //var pizzas = new SelectList(db.Pizzas, "PizzaId", "Name");
            
            var pizzas = _pizzaRepository.GetAll();

            var model = new OrderCreateModel();            

            model.Pizzas = GetSelectListItems(pizzas);
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
                Pizzas = GetSelectListItems(_pizzaRepository.GetAll())
            };

            int pizzaId = 0;
            Int32.TryParse(postedModel.PizzaId, out pizzaId);

            

            if (ModelState.IsValid)
            {
                postedModel.Customer.FirstName = postedModel.Customer.FirstName.Trim();
                postedModel.Customer.LastName = postedModel.Customer.LastName.Trim();

                var customer = _customerRepository.GetAll().SingleOrDefault(x => x.FirstName == postedModel.Customer.FirstName
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

                    _customerRepository.Create(customer);
                }

                var order = _orderRepository.GetAll().SingleOrDefault(x => x.CustomerId == customer.CustomerId && x.PizzaId == pizzaId);

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

                    _orderRepository.Create(order);
                }
                else
                {
                    order.DeliveryArrivalDateTime = postedModel.DeliveryArrivalDateTime;
                    order.Quantity += postedModel.Quantity;

                    _orderRepository.Update(order);
                }

                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (Exception ex)
                //{
                //    model.isSuccessful = false;
                //    model.ConfirmationMessage = "Error saving to database";
                //}

                model.isSuccessful = true;
                model.ConfirmationMessage = "Successfully placed your order!";
            }
            else
            {
                model.isSuccessful = false;
                model.ConfirmationMessage = "Error: Please see validation messages below!";
            }

            return View(model);
        }

        //// GET: Orders/Details/5
        //public ActionResult Details(int? id)
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

        //// GET: Orders/Edit/5
        //public ActionResult Edit(int? id)
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
        //    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
        //    ViewBag.PizzaId = new SelectList(db.Pizzas, "PizzaId", "Name", order.PizzaId);
        //    return View(order);
        //}

        //// POST: Orders/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CustomerId,PizzaId,Quantity,OrderCreateddDateTime")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(order).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", order.CustomerId);
        //    ViewBag.PizzaId = new SelectList(db.Pizzas, "PizzaId", "Name", order.PizzaId);
        //    return View(order);
        //}

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

    }
}