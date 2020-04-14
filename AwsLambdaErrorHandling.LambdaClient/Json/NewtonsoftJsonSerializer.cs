using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AwsLambdaErrorHandling.LambdaClient.Json
{
    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        public string Serialize(object objectToSerialize) => JsonConvert.SerializeObject(objectToSerialize);

        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            using var sr = new StreamReader(stream);
            return JsonConvert.DeserializeObject<T>(await sr.ReadToEndAsync());
        }
    }
}
