namespace Order.Settings;

/// <summary>
/// No sql
/// </summary>
public class NoSQL
{
    /// <summary>
    /// Mongo uri
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Setting for connection string
    /// </summary>
    public string ConnectionSetting { get; set; }

    /// <summary>
    /// Mongodb database
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// Collection names
    /// </summary>
    public Collections Collections { get; set; }
}
