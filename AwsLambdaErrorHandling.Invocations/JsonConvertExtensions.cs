using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AwsLambdaErrorHandling.Invocations
{
    public static class JsonConvertExtensions
    {
        public static async Task<T> Deserialize<T>(this Stream stream)
        {
            using var streamReader = new StreamReader(stream);
            return JsonConvert.DeserializeObject<T>(await streamReader.ReadToEndAsync().ConfigureAwait(false));
        }
    }
}
