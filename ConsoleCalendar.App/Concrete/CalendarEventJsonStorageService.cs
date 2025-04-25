using ConsoleCalendar.App.Abstract;
using Newtonsoft.Json;

namespace ConsoleCalendar.App.Concrete;

public class CalendarEventJsonStorageService : IStorageService<CalendarEvent>
{
    private readonly string filePath;

    public CalendarEventJsonStorageService(string filePath)
    {
        this.filePath = filePath;
    }

    public List<CalendarEvent> Load()
    {
        if (!File.Exists(filePath))
        {
            return new List<CalendarEvent>();
        }

        using var reader = new StreamReader(filePath);
        var json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<CalendarEvent>>(json) ?? new List<CalendarEvent>();
    }

    public void Save(List<CalendarEvent> data)
    {
        if (!File.Exists(filePath))
        {
            CreateDirectoryIfDoesNotExist(filePath);
        }

        using var writer = new StreamWriter(filePath);
        var json = JsonConvert.SerializeObject(data, Formatting.Indented);
        writer.Write(json);
    }

    private void CreateDirectoryIfDoesNotExist(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}
