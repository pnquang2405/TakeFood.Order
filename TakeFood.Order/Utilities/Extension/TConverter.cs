using System.ComponentModel;

namespace Order.Utilities.Extension;

/// <summary>
/// Convert object to class T 
/// </summary>
public static class TConverter
{
    /// <summary>
    /// Change type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ChangeType<T>(object value)
    {
        return (T)ChangeType(typeof(T), value);
    }

    /// <summary>
    /// Change type of object
    /// </summary>
    /// <param name="t"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static object ChangeType(Type t, object value)
    {
        TypeConverter tc = TypeDescriptor.GetConverter(t);
        return tc.ConvertFrom(value);
    }

    /// <summary>
    /// Register type convert
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TC"></typeparam>
    public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
    {
        TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
    }
}
