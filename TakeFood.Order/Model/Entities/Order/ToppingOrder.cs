namespace Order.Model.Entities.Order;

public class ToppingOrder:ModelMongoDB
{
    public string FoodOrderId { get; set; }
    public string ToppingId { get; set; }
    public int Quantity { get; set; }
}
