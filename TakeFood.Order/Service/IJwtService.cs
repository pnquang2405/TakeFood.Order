namespace Order.Service;

/// <summary>
/// Interface Jwt Service
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// GenerateAccessToken
    /// </summary>
    /// <returns></returns>
    string GenerateSecurityToken(string id, IList<String> roles);

    /// <summary>
    /// Generate RefreshToken
    /// </summary>
    /// <returns></returns>
    string GenerateRefreshToken(string id);

    /// <summary>
    /// Valid RefreshToken
    /// </summary>
    /// <returns></returns>
    bool ValidRefreshToken(String token);

    /// <summary>
    /// Valid AccessToken
    /// </summary>
    /// <returns></returns>
    bool ValidSecurityToken(string token);

    /// <summary>
    /// Get user id from token
    /// </summary>
    /// <returns></returns>
    string GetId(string token);
    /// <summary>
    /// Get user role from token
    /// </summary>
    /// <returns></returns>
    List<string> GetRoles(string token);
}
