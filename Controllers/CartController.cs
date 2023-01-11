using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Teatastic.Data;
using Teatastic.Models;

namespace Teatastic.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly TeatasticContext _context;
        private readonly Cart _cart;

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
                _cart.AddToCart(selectedTea, 1);
            }

            TempData["succes"] = "Tea added to cart succesfully";
            return RedirectToAction("Index", "Teas");
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
            //TODO: cartitems need to be removed in db
            //_context.CartItems.Remove();
            return RedirectToAction("Index");
        }

        public Tea GetTeaById(int id)
        {
            return _context.Tea.FirstOrDefault(t => t.Id == id);
        }
    }
}
