using System.IO;
using System.Threading.Tasks;

namespace AwsLambdaErrorHandling.LambdaClient.Json
{
    public interface IJsonSerializer
    {
        string Serialize(object objectToSerialize);
        Task<T> DeserializeAsync<T>(Stream stream);
    }
}
