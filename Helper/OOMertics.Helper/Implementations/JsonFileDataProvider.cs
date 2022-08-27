using Newtonsoft.Json;
using OOMetrics.Abstractions;

namespace OOMertics.Helper.Implementations
{
    public class JsonFileDataProvider : IDeclarationProvider
    {
        private readonly ICollection<IDeclaration> data;
        public JsonFileDataProvider(string filePath)
        {
            data = ReadFromFile(filePath);
        }
        public static void DumpIntoFile(string path, ICollection<IDeclaration> data)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data, typeof(ICollection<IDeclaration>));
            }
        } 
        public static ICollection<IDeclaration> ReadFromFile(string path)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore,
            };
            try
            {
                var jsonFileData = File.ReadAllText(path) ?? string.Empty;
                var potentialData = JsonConvert.DeserializeObject<ICollection<IDeclaration>>(jsonFileData, settings);
                if(potentialData == null)
                {
                    throw new Exception($"Serialization of: {jsonFileData} resulted in null.");
                }
                return potentialData;
            }
            catch (Exception ex)
            {
                throw new Exception($"Can not read declarations from {path} due to: {ex.Message}", ex);
            }
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<ICollection<IDeclaration>> GetDeclarations() => data;
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
    }
}
