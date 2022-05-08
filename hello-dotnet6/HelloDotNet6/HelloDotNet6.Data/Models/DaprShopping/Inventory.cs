namespace HelloDotNet6.Data.Models.DaprShopping;

public class Inventory : DaprShoppingBaseClass
{
    public long InventoryId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }

}