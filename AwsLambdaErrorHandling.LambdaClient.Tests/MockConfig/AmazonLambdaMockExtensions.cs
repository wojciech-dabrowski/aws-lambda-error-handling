using Amazon.Lambda;
using Amazon.Lambda.Model;
using Moq;

namespace AwsLambdaErrorHandling.LambdaClient.Tests.MockConfig
{
    public static class AmazonLambdaMockExtensions
    {
        public static void ReturnsCorrectResponse(this Mock<IAmazonLambda> amazonLambdaMock, string functionName) => SetupInvokeAsync(amazonLambdaMock, functionName);

        public static void ReturnsFunctionErrorResponse(this Mock<IAmazonLambda> amazonLambdaMock, string functionName)
            => SetupInvokeAsync(amazonLambdaMock, functionName, "Unhandled");

        public static void ThrowsTooManyRequestsException(this Mock<IAmazonLambda> amazonLambdaMock, string functionName)
            => amazonLambdaMock.Setup(m => m.InvokeAsync(It.Is<InvokeRequest>(r => r.FunctionName.Equals(functionName)), default))
                               .ThrowsAsync(new TooManyRequestsException("Rate Exceeded."));

        private static void SetupInvokeAsync(Mock<IAmazonLambda> amazonLambdaMock, string functionName, string? functionError = null)
            => amazonLambdaMock.Setup(m => m.InvokeAsync(It.Is<InvokeRequest>(r => r.FunctionName.Equals(functionName)), default))
                               .ReturnsAsync(
                                    new InvokeResponse
                                    {
                                        FunctionError = functionError
                                        , StatusCode = 200
                                    }
                                );
    }
}
