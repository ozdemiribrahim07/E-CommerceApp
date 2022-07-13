using eCommerce.Data.Repo.Abstract;
using eCommerce.Entities.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        public OrderVM orderVM { get; set; }
        private readonly IUnitOfWork _unitofWork;

        public OrderController(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public IActionResult Index()
        {
            var list = _unitofWork.ProductOrder.GetAll(p => p.OrderStatus != "Teslim Edildi");
            return View(list);
        }



    }
}
