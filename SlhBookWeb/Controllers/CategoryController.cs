using Microsoft.AspNetCore.Mvc;
using SlhBookWeb.Data;
using SlhBookWeb.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace SlhBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDBContext _db;

        public CategoryController(AppDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }


        //Get Create Url 
        public IActionResult Create()
        {
            return View();
        }

        //method is post we need to write [HttpPost]
        [HttpPost]

        //we need to write [ValidateAntiForgeryToken] To help and prevent the cross site request forgery attack
        // and automatically inject a key in the form method = Post 
        //Not Required But highly recommend that 
        [ValidateAntiForgeryToken]
        //Post New Category
        public IActionResult Create(Category obj)
        {
            //Custom  Error 
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //---  error just for asp - validation - summary
                //ModelState.AddModelError("CustomError", "The Display Order cannot match the Name.");

                //--- Error for Name & asp-validation-summary
                ModelState.AddModelError("name", "The Display Order cannot match the Name.");

            }

            //Check if model is valid ? 
            if (ModelState.IsValid)
            {

                //when we have to add somthing to the table we Use the Add() method
                //and we pass the argument the Add() method like this new Object
                //We still need to push this to db
                _db.Categories.Add(obj);

                //push this Object to database
                _db.SaveChanges();

                //redirect to the index and get updated list || Data with the GET Index action 
                return RedirectToAction("index");
            }
            return View(obj);
        }

        //--- Get Update Url  ---// 
        //-- Get One Category By ID
        public IActionResult Edit(int? id)
        {
            //Custom  Error 
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //---- return the first element in the tab & empty if no element i you need to pass expression ---//
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            //---- return empty if no element you need to pass expression ---//
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        [HttpPost]

        //we need to write [ValidateAntiForgeryToken] To help and prevent the cross site request forgery attack
        // and automatically inject a key in the form method = Post 
        //Not Required But highly recommend that 
        [ValidateAntiForgeryToken]
        //Updat Old Category
        public IActionResult Edit(Category obj)
        {
            //Custom  Error 
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);

                //push this Object to database
                _db.SaveChanges();

                //redirect to the index and get updated list || Data with the GET Index action 
                return RedirectToAction("index");
            }
            return View(obj);
        }

        //--- Delete ---//
        //--- Get Delete Url  ---// 
        //-- Get One Category By ID
        public IActionResult Delete(int? id)
        {
            //Custom  Error 
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);


            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }


        [HttpPost]

        [ValidateAntiForgeryToken]
        //Delete Old Category
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);

            //push this Object to database
            _db.SaveChanges();

            //redirect to the index and get updated list || Data with the GET Index action 
            return RedirectToAction("index");

        }
    }
}