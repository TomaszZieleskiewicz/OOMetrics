using Newtonsoft.Json;
using OOMetrics.Metrics.Interfaces;

namespace OOMertics.Helper.Implementations
{
    public class JsonFileDataProvider
    {
        public static void DumpIntoFile(string path, IEnumerable<IDeclaration> data)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, data, typeof(IEnumerable<IDeclaration>));
            }
        }
        public static IEnumerable<IDeclaration> ReadFromFile(string path)
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
            var potentialData = JsonConvert.DeserializeObject<IEnumerable<IDeclaration>>(jsonFileData, settings);
            if (potentialData is null)
            {
                throw new Exception($"Can not serialize {path} to List<IDeclaration> containing {jsonFileData}");
            }
            return potentialData;
        }
        private readonly IEnumerable<IDeclaration> data;
        public JsonFileDataProvider(string filePath)
        {
            var potentialData = ReadFromFile(filePath);
            data = potentialData.Cast<IDeclaration>();
        }

        public IEnumerable<IDeclaration> GetDeclarations()
        {
            return data;
        }
    }
}
