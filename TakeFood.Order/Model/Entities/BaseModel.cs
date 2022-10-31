using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Order.Model.Entities;

/// <summary>
/// Base Model Update User 
/// </summary>
public class BaseUpdateUserModel
{
    /// <summary>
    /// Edit date time
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? UpdatedDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Edit by
    /// </summary>
    public ByModel UpdatedBy { get; set; }
}
