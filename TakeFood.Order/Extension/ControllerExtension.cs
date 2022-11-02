using System.Resources;
using Order.ViewModel.Resource;
using Order.ViewModel.Response;

namespace Order.Extension;

/// <summary>
/// Controller extension
/// </summary>
public static class ControllerExtensions
{

    /// <summary>
    /// Reponse result success
    /// </summary>
    /// <param name="controller">Controller</param>
    /// <param name="data">Data to response</param>
    /// <param name="lang">Language: Vi or En or Fr or Ja</param>
    /// <param name="parameter">Parameter</param>
    /// <returns></returns>
    public static Response<T> Success<T>(T data, object[] parameter = null)
    {
        var result = new Response<T>
        {
            Code = Utilities.CommonConstants.Success,
            Result = data
        };
        return result;
    }

    /// <summary>
    /// Reponse error
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="code">Code</param>
    /// <param name="lang">Language</param>
    /// <param name="parameter">Parameter</param>
    /// <returns></returns>
    public static ResponseError Error(string code, string message)
    {
        ResourceManager rm = new ResourceManager(typeof(Resource));
        rm.IgnoreCase = true;
        var result = new ResponseError
        {
            Code = code,
            Message = message
        };
        return result;
    }
}
