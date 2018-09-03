using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCUnit.Models;
using MVCUnitTesting.Models;

namespace MVCUnitTesting.Controllers
{
    public class HomeController : Controller
    {
        private Repository productRepository;

        public HomeController(Repository productRepository)
        {
            this.productRepository = productRepository;
        }

        public HomeController()
        {
            this.productRepository = new WorkingProductRepository();
        }

        public ViewResult Index()
        {
            var products = productRepository.GetAll();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ViewResult FindByGenre(string genre)
        {
            var products = productRepository.GetAll();
            List<Product> results = products.Where(b => b.Genre == genre).ToList();
            return View(results);
        }
    }
}