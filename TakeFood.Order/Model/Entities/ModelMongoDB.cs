using StoreService.Utilities.Converter;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Order.Model.Entities;

/// <summary>
/// Model class
/// </summary>
///
[BsonIgnoreExtraElements]
public class ModelMongoDB
{
    /// <summary>
    /// Id of the model
    /// </summary>
    [Key]
    [Column("id")]
    [BsonElement("id")]
    public string Id { get; set; }

    /// <summary>
    /// Deleted
    /// </summary>
    [Column("is_deleted")]
    [BsonElement("isDeleted")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Created date time
    /// </summary>
    [Timestamp]
    [JsonConverter(typeof(DateTimeConverter))]
    [Column("created_date")]
    [BsonElement("createdDate")]
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Edit date time
    /// </summary>
    [Timestamp]
    [JsonConverter(typeof(DateTimeConverter))]
    [Column("updated_date")]
    [BsonElement("updatedDate")]
    public DateTime? UpdatedDate { get; set; }
}

