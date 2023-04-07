using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Bulky_Book_tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //Storing our database
        private readonly IUnitOfWork _unitOfWork;

        //Constuctor, that uses dependency injection to initialize _context
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //Storing a list of objects of Category type 
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
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
            #region CustomValidationChecks
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Custom error", "Name and DisplayOrder fields shouldn't be the same");
            }

            if (_unitOfWork.Category.GetFirstOrDefault(c => c.Name == obj.Name) != null)
            {
                ModelState.AddModelError("name", "Category with the same name already exists");
            }

            if (_unitOfWork.Category.GetFirstOrDefault(x => x.DisplayOrder == obj.DisplayOrder) != null)
            {
                ModelState.AddModelError("displayOrder", "Category with the same display order already exists");
            }

            if (obj.DisplayOrder <= 0)
            {
                ModelState.AddModelError("displayOrder", "Display order can't be less than zero");
            }
            #endregion

            if (ModelState.IsValid)//Checking if the obj is valid(all required fields are set)
            {
                _unitOfWork.Category.Add(obj);//Adding our object to database
                _unitOfWork.Save();//Saving changes, so object will appear in database
                TempData["success"] = "Created category successfully"; //Stores temporary data
                return RedirectToAction("Index");//Going back to Index page
            }
            return View(obj);
        }


        //GET
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) { return NotFound(); }

            var obj = _unitOfWork.Category.GetFirstOrDefault(c => c.Id == id);

            #region Unnecessary
            //var obj = _context.Categories.Find(id);
            //var obj = _context.Categories.SingleOrDefault(c => c.Id == id);

            //if(obj == null)
            //{
            //    return NotFound();
            //}
            #endregion

            return View(obj);
        }


        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            _unitOfWork.Category.Update(obj);//Update category in database
            _unitOfWork.Save();
            TempData["success"] = "Edited category successfully";
            return RedirectToAction("Index");
        }





        //GET
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) { return NotFound(); }

            var obj = _unitOfWork.Category.GetFirstOrDefault(i => i.Id == id);

            if (obj == null) { return NotFound(); }


            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Deleted category successfully";
            return RedirectToAction("Index");
        }


    }
}
