using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pb304PetShop.DataContext;

namespace Pb304PetShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ShopController : Controller
    {

        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCount = await _context.Products.CountAsync();   
            var products =await _context.Products.Take(6).ToListAsync();
            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> Partial([FromBody] RequestModel requsetModel)
        {
            var products = await _context.Products.Skip(requsetModel.StartFrom).Take(6).ToListAsync();
            return PartialView("_ProductPartialView",products);
        }
    }
    public class  RequestModel
    {
        public int StartFrom { get; set; }
     }
}
