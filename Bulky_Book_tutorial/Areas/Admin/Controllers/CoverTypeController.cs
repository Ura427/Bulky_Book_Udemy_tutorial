﻿using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;


namespace Bulky_Book_tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _unitOfWork.CoverType.GetAll();
            return View(objCoverTypeList);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Created CoverType successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }



        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id <= 0) { return NotFound(); }

            var obj = _unitOfWork.CoverType.GetFirstOrDefault(c => c.Id == id);

            return View(obj);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            _unitOfWork.CoverType.Update(obj);
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
