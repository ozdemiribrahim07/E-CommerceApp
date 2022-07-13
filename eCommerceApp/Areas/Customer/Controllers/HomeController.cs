using eCommerce.Data.Repo.Abstract;
using eCommerce.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace eCommerceApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitofWork;

        public HomeController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> list = _unitofWork.Product.GetAll(include:"Category");
            return View(list);
        }

        [HttpGet]
        public IActionResult Details(int productid)
        {
            
            ShopCart shopCart = new()
            {
                Count = 1,
                ProductId = productid,
                Product = _unitofWork.Product.GetFirstOrDefault(x => x.Id == productid, include: "Category")

            };
            return View(shopCart);
        }



        [HttpPost]
        [Authorize]
        public IActionResult Details(ShopCart shopCart)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
            shopCart.AppUserId = claim.Value;

            ShopCart db = _unitofWork.ShopCart.GetFirstOrDefault(x => x.AppUserId == claim.Value && x.ProductId == shopCart.ProductId);

            if (db == null)
            {
                _unitofWork.ShopCart.Add(shopCart);
                _unitofWork.Save();

                int SepetCount = _unitofWork.ShopCart.GetAll(x => x.AppUserId == claim.Value).ToList().Count();
                HttpContext.Session.SetInt32("SepetCount", SepetCount);
            }
            else
            {
                db.Count += shopCart.Count;
                _unitofWork.Save();
            }
            
            return RedirectToAction("Index");
        }




    }
}
