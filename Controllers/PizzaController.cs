using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                Pizza detail = db.Pizza.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();


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
        [HttpGet]
        public ActionResult Create()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<Category> categories = db.Categories.ToList();

                PizzaCategories pizzaModel = new PizzaCategories();
                pizzaModel.Categories = categories;
                pizzaModel.Pizz = new Pizza();

                return View(pizzaModel);
            }
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PizzaCategories pizzaModel)
        {
            using (PizzaContext db = new PizzaContext())
            {
                if (!ModelState.IsValid)
                {
                    PizzaCategories model = new PizzaCategories();
                    model.Categories = db.Categories.ToList();
                    model.Pizz = pizzaModel.Pizz;

                    return View(model);
                }

                Pizza newPizza = new Pizza();
                newPizza.Name = pizzaModel.Pizz.Name;
                newPizza.Image = pizzaModel.Pizz.Image;
                newPizza.Description = pizzaModel.Pizz.Description;
                newPizza.Price = pizzaModel.Pizz.Price;
                newPizza.Category = pizzaModel.Pizz.Category;
                newPizza.CategoryId = pizzaModel.Pizz.CategoryId;
                db.Pizza.Add(pizzaModel.Pizz);
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

                PizzaCategories model = new PizzaCategories();
                List<Category> categories = db.Categories.ToList();

                model.Categories = categories;
                model.Pizz = mod;

                return View(model);
            }
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PizzaCategories model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using(PizzaContext db = new PizzaContext())
            {
                Pizza mod = db.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(mod != null)
                {
                    mod.Name = model.Pizz.Name;
                    mod.Image = model.Pizz.Image;
                    mod.Description = model.Pizz.Description;
                    mod.Price = model.Pizz.Price;
                    mod.ingredients = model.Pizz.ingredients;

                    db.Update(mod);

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
