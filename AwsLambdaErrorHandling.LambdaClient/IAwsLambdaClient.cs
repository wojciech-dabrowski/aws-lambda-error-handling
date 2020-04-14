using System.Threading.Tasks;

namespace AwsLambdaErrorHandling.LambdaClient
{
    public interface IAwsLambdaClient
    {
        Task<TResponse> InvokeAsync<TRequest, TResponse>(string functionName, TRequest request);
    }
}
