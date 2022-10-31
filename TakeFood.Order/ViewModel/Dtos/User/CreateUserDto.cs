using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Order.ViewModel.Dtos.User;

public class CreateUserDto
{
    /// <summary>
    /// Display Name
    /// </summary>
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    [JsonPropertyName("email")]
    [Required]
    public string Email { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    [JsonPropertyName("username")]
    public string Username { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; }

    /// <summary>
    /// Avatar
    /// </summary>
    [JsonPropertyName("avatar")]
    [Required]
    public string Avatar { get; set; } = "";

    /// <summary>
    /// Số điện thoại
    /// </summary>
    [JsonPropertyName("phoneNumber")]
    [Required]
    public string PhoneNumber { get; set; }
}
