using System.Text.Json;

namespace Order.Utilities.Helper;

public static class Utils
{

    /// <summary>
    /// UTF-8 to ascii
    /// </summary>
    public readonly static Dictionary<char, char> UTF8ToAscii = new Dictionary<char, char>() {
            {'à','a' },
            {'á','a' },
            {'ạ','a' },
            {'ả','a' },
            {'ã','a' },
            {'â','a' },
            {'ầ','a' },
            {'ấ','a' },
            {'ậ','a' },
            {'ẩ','a' },
            {'ẫ','a' },
            {'ă','a' },
            {'ằ','a' },
            {'ắ','a' },
            {'ặ','a' },
            {'ẳ','a' },
            {'ẵ','a' },
            {'ì','i' },
            {'í','i' },
            {'ị','i' },
            {'ỉ','i' },
            {'ĩ','i' },
            {'ù','u' },
            {'ú','u' },
            {'ụ','u' },
            {'ủ','u' },
            {'ũ','u' },
            {'ư','u' },
            {'ừ','u' },
            {'ứ','u' },
            {'ự','u' },
            {'ử','u' },
            {'ữ','u' },
            {'đ','d' },
            {'è','e' },
            {'é','e' },
            {'ẹ','e' },
            {'ẻ','e' },
            {'ẽ','e' },
            {'ê','e' },
            {'ề','e' },
            {'ế','e' },
            {'ệ','e' },
            {'ể','e' },
            {'ễ','e' },
            {'ò','o' },
            {'ó','o' },
            {'ọ','o' },
            {'ỏ','o' },
            {'õ','o' },
            {'ô','o' },
            {'ồ','o' },
            {'ố','o' },
            {'ộ','o' },
            {'ổ','o' },
            {'ỗ','o' },
            {'ơ','o' },
            {'ờ','o' },
            {'ớ','o' },
            {'ợ','o' },
            {'ở','o' },
            {'ỡ','o' },
            {'ỳ','y' },
            {'ý','y' },
            {'ỵ','y' },
            {'ỷ','y' },
            {'ỹ','y' }
        };

    /// <summary>
    /// check number
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static bool IsNumber(this Type type)
    {
        return type == typeof(byte) || type == typeof(ushort) || type == typeof(short) || type == typeof(uint)
            || type == typeof(int) || type == typeof(ulong) || type == typeof(long) || type == typeof(decimal)
            || type == typeof(double) || type == typeof(float);
    }

    /// <summary>
    /// Get Json Serializer Options
    /// </summary>
    /// <returns></returns>
    public static JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    // <summary>
    /// Split big list to small list with specific size
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bigList"></param>
    /// <param name="nSize"></param>
    /// <returns></returns>
    public static IEnumerable<List<T>> SplitList<T>(List<T> bigList, int nSize = 30)
    {
        for (int i = 0; i < bigList.Count; i += nSize)
        {
            yield return bigList.GetRange(i, Math.Min(nSize, bigList.Count - i));
        }
    }

    /// <summary>
    /// String to stream
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(s));
        return stream;
    }
}
