using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bulky_Book_tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objCoverTypeList = _unitOfWork.Product.GetAll();
            return View(objCoverTypeList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            if (id == null || id <= 0) {
                //create product
                return View(product); }
            else { //update product
                   }

           

            return View(product);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product obj)
        {
            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Edited CoverType successfully";
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id <= 0) { return NotFound(); }

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(i => i.Id == id);

            if (obj == null) { return NotFound(); }


            _unitOfWork.CoverType.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Deleted CoverType successfully";
            return RedirectToAction("Index");
        }


    }
}
