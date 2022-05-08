namespace HelloDotNet6.Data.Models.DaprShopping;

public class Product : DaprShoppingBaseClass
{
    public long ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double UnitCost { get; set; }

}