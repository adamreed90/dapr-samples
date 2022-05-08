namespace HelloDotNet6.Data.Models.DaprShopping;

public class Order : DaprShoppingBaseClass
{
    public long OrderId { get; set; }
    public Customer Customer { get; set; }
    public IEnumerable<OrderItem> OrderItems { get; set; }

}

public class OrderItem
{
    public long OrderItemId { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

}