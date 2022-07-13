using eCommerce.Data.Repo.Abstract;
using eCommerce.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> list = _unitOfWork.Category.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return RedirectToAction("Index");

            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0 || id == null)
            {
                return NotFound();
            }

            var result = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
            
        }



        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                return RedirectToAction("Index");

            }

            return View(category);

        }

        
        public IActionResult Delete(int id)
        {
            if (id <= 0 || id == null)
            {
                return NotFound();
            }

            var result = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }


            _unitOfWork.Category.Remove(result);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }







    }
}
