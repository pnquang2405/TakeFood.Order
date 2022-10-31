using MongoDB.Bson.Serialization.Attributes;

namespace Order.Model.Entities.User;

/// <summary>
/// System User
/// </summary>
public class User : ModelMongoDB
{
    /// <summary>
    /// Display Name
    /// </summary>
    [BsonElement("name")]
    public string Name { get; set; }

    /// <summary>
    /// Avatar
    /// </summary>
    [BsonElement("avatar")]
    public string Avatar { get; set; } = "";

    /// <summary>
    /// Role Id
    /// </summary>
    [BsonElement("roleIds")]
    public IList<string> RoleIds { get; set; }

    /// <summary>
    /// Số điện thoại
    /// </summary>
    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Đã kích hoạt hay chưa
    /// </summary>
    [BsonElement("state")]
    public string State { get; set; }

    /// <summary>
    /// Gender
    /// </summary>
    [BsonElement("geder")]
    public bool Gender { get; set; }

}
