using MongoDB.Bson.Serialization.Attributes;

namespace Order.Model.Entities.User;

public class Account : ModelMongoDB
{
    /// <summary>
    /// UserId
    /// </summary>
    [BsonElement("userId")]
    public string UserId { get; set; }
    /// <summary>
    /// Email
    /// </summary>
    [BsonElement("email")]
    public string Email { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    [BsonElement("password")]
    public string Password { get; set; }
}
