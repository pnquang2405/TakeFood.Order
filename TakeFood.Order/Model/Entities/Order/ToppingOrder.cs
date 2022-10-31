namespace Order.Model.Entities.Order;

public class ToppingOrder
{
    public string FoodOrderId { get; set; }
    public string ToppingId { get; set; }
    public int Quantity { get; set; }
}
