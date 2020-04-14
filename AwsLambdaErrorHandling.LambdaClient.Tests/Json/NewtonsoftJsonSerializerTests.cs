using System.IO;
using System.Text;
using System.Threading.Tasks;
using AwsLambdaErrorHandling.LambdaClient.Json;
using FluentAssertions;
using Xunit;

namespace AwsLambdaErrorHandling.LambdaClient.Tests.Json
{
    public class NewtonsoftJsonSerializerTests
    {
        [Fact]
        public async Task DeserializeAsync_WithGivenCorrectErrorInformationJson_ShouldDeserializeObjectCorrectly()
        {
            // Given
            const string correctJson = @"
{
  ""errorType"": ""Exception"",
  ""errorMessage"": ""Exception thrown during AWS Lambda function runtime."",
  ""stackTrace"": [
    ""at AwsLambdaErrorHandling.Functions.FailLambda.Invoke() in C:\\<code-path>\\AwsLambdaErrorHandling.Functions\\FailLambda.cs:line 7""
  ]
}";

            var serializer = new NewtonsoftJsonSerializer();
            const string expectedErrorType = "Exception";
            const string expectedErrorMessage = "Exception thrown during AWS Lambda function runtime.";
            var expectedStackTrace = new[] { "at AwsLambdaErrorHandling.Functions.FailLambda.Invoke() in C:\\<code-path>\\AwsLambdaErrorHandling.Functions\\FailLambda.cs:line 7" };

            // When
            var deserializedObject = await serializer.DeserializeAsync<LambdaRuntimeErrorModel>(new MemoryStream(Encoding.UTF8.GetBytes(correctJson)));

            // Then
            deserializedObject.Should().NotBeNull();
            deserializedObject.ErrorType.Should().Be(expectedErrorType);
            deserializedObject.ErrorMessage.Should().Be(expectedErrorMessage);
            deserializedObject.StackTrace.Should().BeEquivalentTo(expectedStackTrace);
        }
    }
}
