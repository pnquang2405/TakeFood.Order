using System.Text.Json.Serialization;

namespace Order.ViewModel.Response;

/// <summary>
/// Object response
/// </summary>
public class Response<Data>
{
    /// <summary>
    /// Code error or not error
    /// If success code is success, else error
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; set; }

    /// <summary>
    /// Message of code content
    /// </summary>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Message { get; set; }

    /// <summary>
    /// Data is result of action, if has error then data null
    /// </summary>
    [JsonPropertyName("result")]
    public Data Result { get; set; }

    /// <summary>
    /// Implicit cast ResponseError to Response
    /// </summary>
    /// <param name="e"></param>
    public static implicit operator Response<Data>(ResponseError e)
    {
        var result = new Response<Data>
        {
            Code = e.Code,
            Message = e.Message
        };
        return result;
    }
}

/// <summary>
/// ResponseError
/// </summary>
public class ResponseError : Response<object>
{
}
