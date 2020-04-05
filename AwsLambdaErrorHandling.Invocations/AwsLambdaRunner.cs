using System.Threading.Tasks;
using Amazon.Lambda;
using Amazon.Lambda.Model;

namespace AwsLambdaErrorHandling.Invocations
{
    public class AwsLambdaRunner
    {
        private readonly IAmazonLambda _lambdaClient = new AmazonLambdaClient();
        private const string SuccessLambdaName = "aws-lambda-error-handling-SuccessLambda-<your-unique-key>";
        private const string FailLambdaName = "aws-lambda-error-handling-FailLambda-<your-unique-key>";

        public async Task<InvokeResponse> InvokeSuccessLambdaAsync()
        {
            var invokeRequest = new InvokeRequest { FunctionName = SuccessLambdaName };

            return await _lambdaClient.InvokeAsync(invokeRequest);
        }

        public async Task<InvokeResponse> InvokeFailLambdaAsync()
        {
            var invokeRequest = new InvokeRequest { FunctionName = FailLambdaName };

            return await _lambdaClient.InvokeAsync(invokeRequest);
        }
    }
}
