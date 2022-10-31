namespace Order.Model.Entities.Topping;

public class Topping:ModelMongoDB
{
    public string Name { get; set; }
    public string State { get; set; }
    public Double Price { get; set; }
    public string CategoryId { get; set; }
    public string StoreID { get; set; }
}
