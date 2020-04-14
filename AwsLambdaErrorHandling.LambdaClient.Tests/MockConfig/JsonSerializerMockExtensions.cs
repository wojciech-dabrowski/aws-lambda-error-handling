using System.IO;
using AwsLambdaErrorHandling.LambdaClient.Json;
using Moq;

namespace AwsLambdaErrorHandling.LambdaClient.Tests.MockConfig
{
    public static class JsonSerializerMockExtensions
    {
        public static void ReturnsObject<T>(this Mock<IJsonSerializer> jsonSerializerMock, T expectedObject)
            => jsonSerializerMock.Setup(m => m.DeserializeAsync<T>(It.IsAny<Stream>())).ReturnsAsync(expectedObject);
    }
}
