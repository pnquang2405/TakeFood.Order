namespace Order.Model.Entities.Order;

public class FoodOrder
{
    public string OrderId { get; set; }

    public string FoodId { get; set; }
    public int Quantity { get; set; }
}
