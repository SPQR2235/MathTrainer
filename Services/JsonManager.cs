using System.Text.Json;

namespace MathTrainer.Services
{
    abstract class JsonManager<T>(string path)
    {
        private static readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true
        };

        protected static string DataFolder =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "My Games",
                "SPQR_rsnt",
                "MathHomeworkGenerator");

        protected readonly string FilePath = path;

        public abstract T CreateDefault();

        protected T Load()
        {
            if (!File.Exists(FilePath))
                return CreateDefault();

            try
            {
                return JsonSerializer.Deserialize<T>(
                    File.ReadAllText(FilePath),
                    _options)
                    ?? CreateDefault();
            }
            catch (JsonException)
            {
                File.Delete(FilePath);
                return CreateDefault();
            }
        }

        protected void Save(T data)
        {
            Directory.CreateDirectory(
                Path.GetDirectoryName(FilePath)!);

            File.WriteAllText(
                FilePath, JsonSerializer.Serialize(data, _options));
        }
    }
}
