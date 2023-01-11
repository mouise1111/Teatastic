//using Teatastic.Data;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Teatastic.Models;

//namespace Teatastic.Models
//{
//    public class Cart
//    {
//        private readonly TeatasticContext _context;

//        public Cart(TeatasticContext context)
//        {
//            _context = context;
//        }

//        public string Id { get; set; } 
//        public List<CartItem> CartItems { get; set; }

//        public static Cart GetCart(IServiceProvider services)
//        {
//            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

//            var context = services.GetService<TeatasticContext>();
//            string CartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

//            session.SetString("Id", CartId);

//            return new Cart(context) { Id = CartId };
//        }

//        public CartItem GetCartItem(Tea tea)
//        {
//            return _context.CartItems.SingleOrDefault(ci =>
//                ci.Tea.Id == tea.Id && ci.UserId == Id);
//        }

//        public void AddToCart(Tea tea, int quantity, string userId)
//        {
//            var cartItem = GetCartItem(tea);

//            if (cartItem == null)
//            {
//                cartItem = new CartItem
//                {
//                    Tea = tea,
//                    Quantity = 1,
//                    UserId = userId
//                };

//                _context.CartItems.Add(cartItem);
//            }
//            else
//            {
//                cartItem.Quantity += quantity;
//            }
//            _context.SaveChanges();
//        }

//        public int ReduceQuantity(Tea tea)
//        {
//            var cartItem = GetCartItem(tea);
//            var remainingQuantity = 0;

//            if (cartItem != null)
//            {
//                if (cartItem.Quantity > 1)
//                {
//                    remainingQuantity = --cartItem.Quantity;
//                }
//                else
//                {
//                    _context.CartItems.Remove(cartItem);
//                }
//            }
//            _context.SaveChanges();

//            return remainingQuantity;
//        }

//        public int IncreaseQuantity(Tea tea)
//        {
//            var cartItem = GetCartItem(tea);
//            var remainingQuantity = 0;

//            if (cartItem != null)
//            {
//                if (cartItem.Quantity > 0)
//                {
//                    remainingQuantity = ++cartItem.Quantity;
//                }
//            }
//            _context.SaveChanges();

//            return remainingQuantity;
//        }

//        public void RemoveFromCart(Tea tea)
//        {
//            var cartItem = GetCartItem(tea);

//            if (cartItem != null)
//            {
//                _context.CartItems.Remove(cartItem);
//            }
//            _context.SaveChanges();
//        }

//        public void ClearCart()
//        {
//            var cartItems = _context.CartItems.Where(ci => ci.UserId == Id);

//            _context.CartItems.RemoveRange(cartItems);

//            _context.SaveChanges();
//        }

//        public List<CartItem> GetAllCartItems()
//        {
//            return CartItems ??= _context.CartItems.Where(ci => ci.UserId == Id)
//                    .Include(ci => ci.Tea)
//                    .ToList();
//        }

//        public int GetCartTotal()
//        {
//            return (int)_context.CartItems
//                .Where(ci => ci.UserId == Id)
//                .Select(ci => ci.Tea.Price * ci.Quantity)
//                .Sum();
//        }
//    }
//}
using Microsoft.EntityFrameworkCore;
using Teatastic.Data;
namespace Teatastic.Models
{
    public class Cart
    {
        private readonly TeatasticContext _context;

        public Cart(TeatasticContext context)
        {
            _context = context;
        }

        public string Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<TeatasticContext>();
            string cartId = session.GetString("Id") ?? Guid.NewGuid().ToString();

            session.SetString("Id", cartId);

            return new Cart(context) { Id = cartId };
        }

        public CartItem GetCartItem(Tea tea)
        {
            return _context.CartItems.SingleOrDefault(ci =>
                ci.Tea.Id == tea.Id && ci.CartId == Id);
        }

        public void AddToCart(Tea tea, int quantity)
        {
            var cartItem = GetCartItem(tea);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Tea = tea,
                    Quantity = quantity,
                    CartId = Id
                };

                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }
            _context.SaveChanges();
        }

        public int ReduceQuantity(Tea tea)
        {
            var cartItem = GetCartItem(tea);
            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    remainingQuantity = --cartItem.Quantity;
                }
                else
                {
                    _context.CartItems.Remove(cartItem);
                }
            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public int IncreaseQuantity(Tea tea)
        {
            var cartItem = GetCartItem(tea);
            var remainingQuantity = 0;

            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                {
                    remainingQuantity = ++cartItem.Quantity;
                }
            }
            _context.SaveChanges();

            return remainingQuantity;
        }

        public void RemoveFromCart(Tea tea)
        {
            var cartItem = GetCartItem(tea);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _context.CartItems.Where(ci => ci.CartId == Id);

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();
        }

        public List<CartItem> GetAllCartItems()
        {
            return CartItems ??
                (CartItems = _context.CartItems.Where(ci => ci.CartId == Id)
                    .Include(ci => ci.Tea)
                    .ToList());
        }

        public int GetCartTotal()
        {
            return _context.CartItems
                .Where(ci => ci.CartId == Id)
                .Select(ci => (int)(ci.Tea.Price * ci.Quantity))
                .Sum();
        }
    }
}

