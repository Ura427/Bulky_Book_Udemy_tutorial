using Bulky_Book_tutorial.Data;
using Bulky_Book_tutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky_Book_tutorial.Controllers
{
    public class CategoryController : Controller
    {
        //Storing our database
        private readonly ApplicationDbContext _context;

        //Constuctor, that uses dependency injection to initialize _context
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Storing a list of objects of Category type 
            IEnumerable<Category> objCategoryList = _context.Categories;
            return View(objCategoryList);
        }


        //GET
        [HttpGet]//Is used, when you only want to receive data
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]//Is used, when you want to change or update data
        [ValidateAntiForgeryToken]//attribute, that is used to prevent cross-site request forgery (CSRF) attacks.
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid && obj.DisplayOrder > 0 )//Checking if the obj is valid(all required fields are set)
            {
                _context.Categories.Add(obj);//Adding our object to database
                _context.SaveChanges();//Saving changes, so object will appear in database
                return RedirectToAction("Index");//Going back to Index page
            }
           return View(obj);
        }


        [HttpGet]
        public IActionResult Delete() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name == obj.Name);//Search an object in database with the same name as our parameter(obj) has 
            _context.Categories.Remove(category);//Remove category from database
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
