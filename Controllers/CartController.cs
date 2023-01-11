using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Teatastic.Areas.Identity.Data;
using Teatastic.Data;
using Teatastic.Models;

namespace Teatastic.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly TeatasticContext _context;
        private readonly Cart _cart;
        private readonly UserManager<TeatasticUser> _userManager;

        public CartController(TeatasticContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        public IActionResult Index()
        {
            var items = _cart.GetAllCartItems();
            _cart.CartItems = items;

            return View(_cart);
        }

        public IActionResult AddToCart(int id)
        {
            var selectedTea = GetTeaById(id);

            if (selectedTea != null)
            {
                _cart.AddToCart(selectedTea, 1, _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Id);
            }

            return RedirectToAction("Index", "Store");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var selectedTea = GetTeaById(id);

            if (selectedTea != null)
            {
                _cart.RemoveFromCart(selectedTea);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ReduceQuantity(int id)
        {
            var selectedTea = GetTeaById(id);

            if (selectedTea != null)
            {
                _cart.ReduceQuantity(selectedTea);
            }

            return RedirectToAction("Index");
        }

        public IActionResult IncreaseQuantity(int id)
        {
            var selectedTea = GetTeaById(id);

            if (selectedTea != null)
            {
                _cart.IncreaseQuantity(selectedTea);
            }

            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cart.ClearCart();

            return RedirectToAction("Index");
        }

        public Tea GetTeaById(int id)
        {
            return _context.Tea.FirstOrDefault(b => b.Id == id);
        }
    }
}
