using eCommerce.Data.Repo.Abstract;
using eCommerce.Entities.Concrete;
using eCommerce.Entities.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerceApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    
    public class ShopCartController : Controller
    {
        private readonly ShopCartVM _shopCartVM;
        private readonly IUnitOfWork _unitofWork;

        public ShopCartController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account", new {area="Identity"}); 
            }

            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            ShopCartVM vm = new ShopCartVM()
            {
                CartList = _unitofWork.ShopCart.GetAll(x => x.AppUserId == claim.Value, include: "Product"),
                ProductOrder = new()
            };

            foreach (var item in vm.CartList)
            {
                item.Price = item.Product.ProductPrice * item.Count;
                vm.ProductOrder.OrderPrice += (item.Price);
            }


            return View(vm);
        }

       
        public IActionResult Order()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Account", new { area = "Identity" });
            }

            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            ShopCartVM vm = new ShopCartVM()
            {
                ProductOrder = new(),
                CartList = _unitofWork.ShopCart.GetAll(x => x.AppUserId == claim.Value, include: "Product")
            };

            vm.ProductOrder.AppUser = _unitofWork.AppUser.GetFirstOrDefault(x => x.Id == claim.Value);
            vm.ProductOrder.Phone = vm.ProductOrder.AppUser.Phone;
            vm.ProductOrder.Name = vm.ProductOrder.AppUser.FullName;
            vm.ProductOrder.FullAdress = vm.ProductOrder.AppUser.FullAdress;

            foreach (var item in vm.CartList)
            {
                item.Price = item.Product.ProductPrice * item.Count;
                vm.ProductOrder.OrderPrice += (item.Price);
            }
            return View(vm);
        }

        [HttpPost]
        [ActionName("Order")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult OrderPost()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            ShopCartVM vm = new ShopCartVM()
            {
                ProductOrder = new(),
                CartList = _unitofWork.ShopCart.GetAll(x => x.AppUserId == claim.Value, include: "Product")
            };

            AppUser appUser = _unitofWork.AppUser.GetFirstOrDefault(x => x.Id == claim.Value);

            vm.ProductOrder.AppUser = appUser;

            vm.ProductOrder.OrderDate = System.DateTime.Now;
            vm.ProductOrder.AppUserId = claim.Value;
            vm.ProductOrder.OrderStatus = "Sipariş Verildi";

            vm.ProductOrder.Phone = vm.ProductOrder.AppUser.Phone;
            vm.ProductOrder.Name = vm.ProductOrder.AppUser.FullName;
            vm.ProductOrder.FullAdress = vm.ProductOrder.AppUser.FullAdress;


            foreach (var item in vm.CartList)
            {
                item.Price = item.Product.ProductPrice * item.Count;
                vm.ProductOrder.OrderPrice += (item.Price);
            }

            _unitofWork.ProductOrder.Add(vm.ProductOrder);
            _unitofWork.Save();



            foreach (var item in vm.CartList)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = item.ProductId,
                    ProductOrderId = vm.ProductOrder.Id,
                    Price = item.Price,
                    Count = item.Count
                };

                _unitofWork.OrderDetail.Add(orderDetail);
                _unitofWork.Save();

            }


            List<ShopCart> carts = _unitofWork.ShopCart.GetAll(x => x.AppUserId == claim.Value).ToList();
            _unitofWork.ShopCart.RemoveRange(carts);
            _unitofWork.Save();

            return RedirectToAction(nameof(Index), "Home", new { area = "Customer" });
        }

        public IActionResult Remove(int id)
        {
            var cart = _unitofWork.ShopCart.GetFirstOrDefault(x => x.Id == id);

            if (cart.Count > 1 )
            {
                cart.Count -= 1;
            }
            else
            {
                _unitofWork.ShopCart.Remove(cart);
                var count = _unitofWork.ShopCart.GetAll(x => x.AppUserId == cart.AppUserId).ToList().Count - 1;
                HttpContext.Session.SetInt32("SepetCount", count);
            }
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));

        }



    }
}
