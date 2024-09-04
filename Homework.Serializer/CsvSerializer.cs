using System.Reflection;

namespace Homework.Serializer;

public class CsvSerializer
{
    public string Serialize<T>(T obj)
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var values = properties.Select(p => p.GetValue(obj)?.ToString() ?? string.Empty);
        return string.Join(",", values);
    }

    public T Deserialize<T>(string csv) where T : new()
    {
        var obj = new T();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var values = csv.Split(',');

        for (var i = 0; i < properties.Length; i++)
        {
            if (i < values.Length)
            {
                properties[i].SetValue(obj, Convert.ChangeType(values[i], properties[i].PropertyType));
            }
        }

        return obj;
    }
}