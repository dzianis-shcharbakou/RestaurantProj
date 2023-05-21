namespace Mango.Web.Models.ShoppingCart
{
    public class CartHeaderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string CouponCode { get; set; }
        public decimal OrderTotal { get; set; }
    }
}
