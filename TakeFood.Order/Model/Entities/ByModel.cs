using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Order.Model.Entities
{
    public class ByModel
    {
        /// <summary>
        /// Id user
        /// </summary>
        [JsonPropertyName("userId")]
        [BsonElement("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Display name of user
        /// </summary>
        [JsonPropertyName("displayName")]
        [BsonElement("displayName")]
        public string DisplayName { get; set; }
    }
}
