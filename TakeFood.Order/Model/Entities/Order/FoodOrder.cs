namespace Order.Model.Entities.Order;

public class FoodOrder:ModelMongoDB
{
    public string OrderId { get; set; }

    public string FoodId { get; set; }
    public int Quantity { get; set; }
}
