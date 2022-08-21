using Newtonsoft.Json;
using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class JsonFileDataProvider
    {
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
            var jsonFileData = File.ReadAllText(path);
            if (jsonFileData == null)
            {
                throw new Exception($"No data in {path} found.");
            }
            var potentialData = JsonConvert.DeserializeObject<ICollection<IDeclaration>>(jsonFileData, settings);
            if (potentialData is null)
            {
                throw new Exception($"Can not serialize {path} to List<IDeclaration> containing {jsonFileData}");
            }
            return potentialData;
        }
        private readonly ICollection<IDeclaration> data;
        public JsonFileDataProvider(string filePath)
        {
            data = ReadFromFile(filePath);
        }

        public async Task<ICollection<IDeclaration>> GetDeclarations()
        {
            return data;
        }
    }
}
