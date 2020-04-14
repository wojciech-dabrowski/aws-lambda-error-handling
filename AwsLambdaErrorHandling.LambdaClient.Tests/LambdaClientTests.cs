using System.Threading.Tasks;
using Amazon.Lambda;
using AwsLambdaErrorHandling.LambdaClient.Exceptions;
using AwsLambdaErrorHandling.LambdaClient.Json;
using AwsLambdaErrorHandling.LambdaClient.Tests.MockConfig;
using FluentAssertions;
using Moq;
using Xunit;

namespace AwsLambdaErrorHandling.LambdaClient.Tests
{
    public class LambdaClientTests
    {
        private const string TestFunctionName = "test-function-name";
        private readonly TestRequestModel _testRequestModel = new TestRequestModel();

        [Fact]
        public async Task InvokeAsync_AmazonLambdaReturningCorrectAnswerAndSerializerReturningCorrectObject_ShouldReturnCorrectResponse()
        {
            // Given
            var expectedResponse = new TestResponseModel
            {
                TestNumberProperty = 10,
                TestStringProperty = "TestString"
            };

            var serializerMock = new Mock<IJsonSerializer>();
            var amazonLambdaMock = new Mock<IAmazonLambda>();

            amazonLambdaMock.ReturnsCorrectResponse(TestFunctionName);
            serializerMock.ReturnsObject(expectedResponse);

            var lambdaClient = new AwsLambdaClient(amazonLambdaMock.Object, serializerMock.Object);

            // When
            var clientResponse = await lambdaClient.InvokeAsync<TestRequestModel, TestResponseModel>(TestFunctionName, _testRequestModel);

            // Then
            clientResponse.Should().NotBeNull();
            clientResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Fact]
        public void InvokeAsync_AmazonLambdaThrowsAmazonLambdaException_ShouldThrowLambdaInvocationException()
        {
            // Given

            var serializerMock = new Mock<IJsonSerializer>();
            var amazonLambdaMock = new Mock<IAmazonLambda>();

            amazonLambdaMock.ThrowsTooManyRequestsException(TestFunctionName);

            var lambdaClient = new AwsLambdaClient(amazonLambdaMock.Object, serializerMock.Object);

            // When
            var func = lambdaClient.Awaiting(x => x.InvokeAsync<TestRequestModel, TestResponseModel>(TestFunctionName, _testRequestModel));

            // Then
            func.Should().ThrowExactly<LambdaInvocationException>();
        }

        [Fact]
        public void InvokeAsync_AmazonLambdaReturnsUnhandledFunctionError_ShouldThrowLambdaRuntimeExceptionWithDeserializedInfo()
        {
            // Given
            var expectedErrorModel = new LambdaRuntimeErrorModel
            {
                ErrorType = "ErrorType",
                ErrorMessage = "ErrorMessage",
                StackTrace = new[] { "One", "Two", "Three" }
            };

            var serializerMock = new Mock<IJsonSerializer>();
            var amazonLambdaMock = new Mock<IAmazonLambda>();

            amazonLambdaMock.ReturnsFunctionErrorResponse(TestFunctionName);
            serializerMock.ReturnsObject(expectedErrorModel);

            var lambdaClient = new AwsLambdaClient(amazonLambdaMock.Object, serializerMock.Object);

            // When
            var func = lambdaClient.Awaiting(x => x.InvokeAsync<TestRequestModel, TestResponseModel>(TestFunctionName, _testRequestModel));

            // Then
            func.Should()
                .ThrowExactly<LambdaRuntimeException>()
                .And.RuntimeError
                .Should()
                .BeEquivalentTo(expectedErrorModel);
        }
    }
}
