using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace AwsLambdaErrorHandling.Invocations
{
    public class AwsLambdaRunner
    {
        private readonly IAmazonLambda _lambdaClient = new AmazonLambdaClient();

        public async Task<InvokeResponse> InvokeSuccessLambdaAsync()
        {
            const string successLambdaName = "aws-lambda-error-handling-SuccessLambda-<your-unique-key>";
            var invokeRequest = new InvokeRequest { FunctionName = successLambdaName };

            return await _lambdaClient.InvokeAsync(invokeRequest);
        }

        public async Task<InvokeResponse> InvokeFailLambdaAsync()
        {
            const string failLambdaName = "aws-lambda-error-handling-FailLambda-<your-unique-key>";
            var invokeRequest = new InvokeRequest { FunctionName = failLambdaName };

            return await _lambdaClient.InvokeAsync(invokeRequest);
        }
    }
}
