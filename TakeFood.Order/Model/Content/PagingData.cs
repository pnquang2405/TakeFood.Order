namespace TakeFood.StoreService.Model.Content;

public class PagingData<T>
{
    /// <summary>
    /// Number of items 
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// The data 
    /// </summary>
    public IEnumerable<T> Data { get; set; }

    /// <summary>
    /// Get pagination data
    /// </summary>
    /// <param name="count"></param>
    /// <param name="data"></param>
    public PagingData(int count, IEnumerable<T> data)
    {
        Count = count;
        Data = data;
    }
}
