using MongoDB.Bson.Serialization.Attributes;

namespace Order.Model.Entities.Role;

public class Role : ModelMongoDB
{
    /// <summary>
    /// Role's Name
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }
    /// <summary>
    /// Role's Description
    /// </summary>
    [BsonElement("description")]
    public string Description { get; set; }
}
