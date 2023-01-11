namespace Teatastic.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Tea Tea { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; }
    }
}
