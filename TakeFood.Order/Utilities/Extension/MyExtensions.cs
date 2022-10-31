using Order.Utilities.Helper;
using System.Text.Json;

namespace Order.Utilities.Extension;

/// <summary>
/// My extension
/// </summary>
public static class MyExtensions
{

    /// <summary>
    /// Clone list of objects to list of other objects
    /// </summary>
    /// <param name="source"></param>
    /// <param name="dest"></param>
    public static void CopyPropertiesTo<T, D>(this IList<T> source, IList<T> dest)
    {
        if (source != null && dest != null)
        {
            foreach (var item in source)
            {
                dest.Add(item);
            }
        }
    }

    /// <summary>
    /// Clone source
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static D Clone<D>(this object source)
    {
        string json;
        if (source is string temp)
        {
            json = temp;
        }
        else
        {
            json = JsonSerializer.Serialize(source, Utils.SerializerOptions);
        }
        if (typeof(D) == typeof(string))
        {
            return TConverter.ChangeType<D>(json);
        }
        var rs = JsonSerializer.Deserialize<D>(json, Utils.SerializerOptions);
        return rs;
    }

    /// <summary>
    /// To json string
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string ToJsonString(this object source)
    {
        string json;
        if (source is string temp)
        {
            json = temp;
        }
        else
        {
            json = JsonSerializer.Serialize(source, Utils.SerializerOptions);
        }
        return json;
    }
}
