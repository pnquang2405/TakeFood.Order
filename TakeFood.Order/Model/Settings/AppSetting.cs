namespace Order.Settings;

/// <summary>
/// App setting
/// </summary>
public class AppSetting
{
    /// <summary>
    /// No SQL
    /// </summary>
    public NoSQL NoSQL { get; set; }

    /// <summary>
    /// JwtConfig
    /// </summary>
    public JwtConfig JwtConfig { get; set; }

    public MailSettings MailSettings { get; set; }
}

/// <summary>
/// Config for jwt
/// </summary>
public class JwtConfig
{
    /// <summary>
    /// Secret for access key
    /// </summary>
    public String Secret { get; set; }

    /// <summary>
    /// Secret for refresh token
    /// </summary>
    public String Secret2 { get; set; }

    /// <summary>
    /// Time expirate of access token
    /// </summary>
    public int ExpirationInHours { get; set; }

    /// <summary>
    /// Time expirate of refresh topken
    /// </summary>
    public int ExpirationInMonths { get; set; }
}

public class MailSettings
{
    /// <summary>
    /// Mail
    /// </summary>
    public String Mail { get; set; }

    /// <summary>
    /// DisplayName
    /// </summary>
    public String DisplayName { get; set; }

    /// <summary>
    /// Password
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Server host
    /// </summary>
    public string Host { get; set; }
    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; set; }
}
