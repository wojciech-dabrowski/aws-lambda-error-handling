using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using AwsLambdaErrorHandling.LambdaClient.Exceptions;
using AwsLambdaErrorHandling.LambdaClient.Json;

namespace AwsLambdaErrorHandling.LambdaClient
{
    public class AwsLambdaClient : IAwsLambdaClient
    {
        private readonly IAmazonLambda _amazonLambda;
        private readonly IJsonSerializer _jsonSerializer;

        public AwsLambdaClient(IAmazonLambda amazonLambda, IJsonSerializer jsonSerializer)
        {
            _amazonLambda = amazonLambda;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<TResponse> InvokeAsync<TRequest, TResponse>(string functionName, TRequest request)
        {
            var invokeRequest = new InvokeRequest
            {
                FunctionName = functionName
                , Payload = _jsonSerializer.Serialize(request)
            };

            var lambdaResponse = await InvokeLambda(invokeRequest).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(lambdaResponse.FunctionError))
            {
                return await _jsonSerializer.DeserializeAsync<TResponse>(lambdaResponse.Payload).ConfigureAwait(false);
            }

            var runtimeError = await _jsonSerializer.DeserializeAsync<LambdaRuntimeErrorModel>(lambdaResponse.Payload).ConfigureAwait(false);
            throw new LambdaRuntimeException(runtimeError);
        }

        private async Task<InvokeResponse> InvokeLambda(InvokeRequest invokeRequest)
        {
            try
            {
                return await _amazonLambda.InvokeAsync(invokeRequest).ConfigureAwait(false);
            }
            catch (AmazonLambdaException e)
            {
                throw new LambdaInvocationException(e);
            }
        }
    }
}
