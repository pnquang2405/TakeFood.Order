using MongoDB.Bson.Serialization.Attributes;

namespace Order.Model.Entities.Store;


/// <summary>
/// Store
/// </summary>
public class Store:ModelMongoDB
{
    /// <summary>
    /// Category's name
    /// </summary>
    [BsonElement("Name")]
    public string Name { get; set; }
    /// <summary>
    /// Address Id
    /// </summary>
    [BsonElement("addressId")]
    public string AddressId { get; set; }
    /// <summary>
    /// PhoneNumber
    /// </summary>
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; }
    /// <summary>
    /// State
    /// </summary>
    [BsonElement("State")]
    public string State { get; set; }
    /// <summary>
    /// User id of owner
    /// </summary>
    [BsonElement("OwnerId")]
    public string OwnerId { get; set; }
    /// <summary>
    /// Sum of Star
    /// </summary>
    [BsonElement("sumStar")]
    public int SumStar { get; set; }
    /// <summary>
    /// Category's name
    /// </summary>
    [BsonElement("numReiview")]
    public int NumReiview { get; set; }
    /// <summary>
    /// Tax Id
    /// </summary>
    [BsonElement("taxId")]
    public string TaxId { get; set; }
    [BsonElement("STK")]
    public string STK { get; set; }
    [BsonElement("email")]
    public string email { get; set; }

    [BsonElement("cmnd")]
    public string cmnd { get; set; }
}
