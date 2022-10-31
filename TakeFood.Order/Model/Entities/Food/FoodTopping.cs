namespace Order.Model.Entities.Food;

public class FoodTopping:ModelMongoDB
{
    public string FoodId { get; set; }
    public string ToppingId { get; set; }
}
