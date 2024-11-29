using Store.Core.Entities.Order;

namespace Store.Core.Dtos.Orders
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int PictureUrl  { get; set; }
    }
} 