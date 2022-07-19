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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                Pizza mod = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(mod == null)
                {
                    return NotFound();
                }

                return View(mod);
            }
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pizza pizzaModel)
        {
            if (!ModelState.IsValid)
            {
                return View(pizzaModel);
            }

            using(PizzaContext db = new PizzaContext())
            {
                Pizza mod = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(mod != null)
                {
                    mod.Name = pizzaModel.Name;
                    mod.Image = pizzaModel.Image;
                    mod.Description = pizzaModel.Description;
                    mod.Price = pizzaModel.Price;
                    mod.ingredients = pizzaModel.ingredients;

                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: PizzaController/Delete/5.
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza mod = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (mod == null)
                {
                    return NotFound();
                }

                //db.Remove(mod);
                //return RedirectToAction("Index");
                return View("Delete", mod);
            }
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(toDelete);
            //}

            using(PizzaContext db = new PizzaContext())
            {
                Pizza toDelete = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(toDelete != null)
                {
                    db.Pizza.Remove(toDelete);
                    db.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
