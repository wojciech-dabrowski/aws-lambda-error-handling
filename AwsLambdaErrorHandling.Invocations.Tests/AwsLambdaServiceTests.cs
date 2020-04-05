using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace AwsLambdaErrorHandling.Invocations.Tests
{
    public class AwsLambdaServiceTests
    {
        [Fact]
        public async Task InvokeSuccessLambdaAsync_WithCorrectlyConfiguredLocalAwsAccount_ShouldReturnCorrectStatusCodeAndExecutedVersion()
        {
            // Given
            const int expectedStatusCode = 200;
            const string expectedExecutedVersion = "$LATEST";
            var runner = new AwsLambdaRunner();

            // When
            var response = await runner.InvokeSuccessLambdaAsync();

            // Then
            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(expectedStatusCode);
                response.ExecutedVersion.Should().Be(expectedExecutedVersion);
                response.FunctionError.Should().BeNullOrEmpty();
            }
        }

        [Fact]
        public async Task InvokeFailLambdaAsync_WithCorrectlyConfiguredLocalAwsAccount_ShouldReturnCorrectStatusCodeExecutedVersionAndFunctionError()
        {
            // Given
            const int expectedStatusCode = 200;
            const string expectedExecutedVersion = "$LATEST";
            const string expectedFunctionError = "Unhandled";
            var runner = new AwsLambdaRunner();

            // When
            var response = await runner.InvokeFailLambdaAsync();

            // Then
            using (new AssertionScope())
            {
                response.Should().NotBeNull();
                response.StatusCode.Should().Be(expectedStatusCode);
                response.ExecutedVersion.Should().Be(expectedExecutedVersion);
                response.FunctionError.Should().Be(expectedFunctionError);
            }
        }
    }
}
