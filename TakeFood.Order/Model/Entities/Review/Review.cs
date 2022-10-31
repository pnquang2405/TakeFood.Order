namespace Order.Model.Entities.Review;

public class Review : ModelMongoDB
{
    public string OrderId { get; set; }
    public string Description { get; set; }
    public int Star { get; set; }
}
