using MongoDB.Bson.Serialization.Attributes;

namespace Order.Model.Entities.Address;

/// <summary>
/// Address of a place
/// </summary>
public class Address : ModelMongoDB
{
    /// <summary>
    /// information
    /// </summary>
    [BsonElement("infomation")]
    public string Information;

    /// <summary>
    /// address
    /// </summary>
    [BsonElement("address")]
    public string Addrress;

    /// <summary>
    /// Address type
    /// </summary>
    [BsonElement("addressType")]
    public string AddressType;
    /// <summary>
    /// Latitude
    /// </summary>
    [BsonElement("latitude")]
    public double Lat;
    /// <summary>
    /// Longtitude
    /// </summary>
    [BsonElement("longtitude")]
    public double Lng;
}
