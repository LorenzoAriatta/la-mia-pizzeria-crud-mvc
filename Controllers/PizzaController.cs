using la_mia_pizzeria_static.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        // GET: PizzaController
        public ActionResult Index()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzaList = db.Pizza.ToList();
                return View("Index", pizzaList);
            }
        }

        // GET: PizzaController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza detail = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();


                if (detail == null)
                {
                    return View("Error");
                }
                else
                {
                    db.Entry(detail).Collection("ingredients").Load();
                    return View("Details", detail);
                }
            }
        }

        // GET: PizzaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pizza pizzaModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", pizzaModel);
            }

            using (PizzaContext db = new PizzaContext())
            {
                db.Pizza.Add(pizzaModel);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: PizzaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PizzaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
