using System.Diagnostics;
using Newtonsoft.Json;

namespace Homework.Serializer;

public class Program
{
    public static void Main(string[] args)
    {
        var serializer = new CsvSerializer();
        var fInstance = F.Get();

        // Замер времени сериализации
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        for (var i = 0; i < 1000; i++)
        {
            var csv = serializer.Serialize(fInstance);
        }

        stopwatch.Stop();
        Console.WriteLine($"Время на сериализацию = {stopwatch.ElapsedMilliseconds} мс");

        // Замер времени десериализации
        var csvData = serializer.Serialize(fInstance);
        stopwatch.Restart();

        for (var i = 0; i < 1000; i++)
        {
            var deserializedInstance = serializer.Deserialize<F>(csvData);
        }

        stopwatch.Stop();
        Console.WriteLine($"Время на десериализацию = {stopwatch.ElapsedMilliseconds} мс");

        stopwatch.Restart();
        for (var i = 0; i < 1000; i++)
        {
            var json = JsonConvert.SerializeObject(fInstance);
        }
        stopwatch.Stop();
        Console.WriteLine($"Стандартный механизм (Newtonsoft.Json) - Время на сериализацию = {stopwatch.ElapsedMilliseconds} мс");

        // Замер времени десериализации с использованием Newtonsoft.Json
        var jsonData = JsonConvert.SerializeObject(fInstance);
        stopwatch.Restart();
        for (var i = 0; i < 1000; i++)
        {
            var deserializedInstance = JsonConvert.DeserializeObject<F>(jsonData);
        }
        stopwatch.Stop();
        Console.WriteLine($"Стандартный механизм (Newtonsoft.Json) - Время на десериализацию = {stopwatch.ElapsedMilliseconds} мс");
    }
}