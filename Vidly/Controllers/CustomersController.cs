using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Customer
        public ActionResult Index()
        { 
            return View(_context.Customers.Include(c => c.MembershipType).ToList<Customer>());
        }

        public ActionResult Details(int id, string Name)
        {
            Customer customer = _context.Customers.Include( c => c.MembershipType).SingleOrDefault<Customer>(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList<MembershipType>();

            var viewModel = new CustomerViewModel
            {
                MembershipTypes = membershipType
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerToSave = _context.Customers.Single(c => c.Id == customer.Id);

                customerToSave.Name = customer.Name;
                customerToSave.Birthday= customer.Birthday;
                customerToSave.IsSubscribedToNewsletter= customer.IsSubscribedToNewsletter;
                customerToSave.MembershipTypeId = customer.MembershipTypeId;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault<Customer>(c => c.Id == id);

            if (customer != null)
            {
                var viewmodel = new CustomerViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View(viewmodel);
            }
            else
            {
                return HttpNotFound();
            }
        }


    }
}