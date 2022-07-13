using eCommerce.Data.Repo.Abstract;
using eCommerce.Entities.Concrete;
using eCommerce.Entities.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace eCommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var product = _unitOfWork.Product.GetAll();
            return View(product);
        }


        [HttpGet]
        public IActionResult AddUpdate(int id)
        {
            ProductVM vm = new()
            {
                Product = new(),
                ListCategory = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                })

            };

            if (id <= 0 || id == null)
            {
                return View(vm);
            }

            vm.Product = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (vm.Product == null)
            {
                return View(vm);
            }

            return View(vm);

        }



        [HttpPost]
        public IActionResult AddUpdate(ProductVM vm , IFormFile file)
        {
            string rootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploadRoot = Path.Combine(rootPath, @"img\products");
                var extension = Path.GetExtension(file.FileName);

                if (vm.Product.ProductImage != null)
                {
                    var oldpic = Path.Combine(rootPath, vm.Product.ProductImage);
                    if (System.IO.File.Exists(oldpic))
                    {
                        System.IO.File.Delete(oldpic);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(uploadRoot,fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                vm.Product.ProductImage = @"\img\products\" + fileName + extension;

            }


            if (vm.Product.Id <= 0)
            {
                _unitOfWork.Product.Add(vm.Product);
            }
            else
            {
                _unitOfWork.Product.Update(vm.Product);
            }

            _unitOfWork.Save();
            return RedirectToAction("Index");
            

        }



        public IActionResult Delete(int id)
        {
            if (id <= 0 || id == null)
            {
                return NotFound();
            }

            var result = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }


            _unitOfWork.Product.Remove(result);
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }

    }
}
