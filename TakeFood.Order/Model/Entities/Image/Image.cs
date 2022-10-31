namespace Order.Model.Entities.Image;

public class Image:ModelMongoDB
{
    public string StoreId { get; set; }
    public string CategoryId { get; set; }
    public string Url { get; set; }
}
