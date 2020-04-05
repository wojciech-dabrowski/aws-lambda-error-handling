using Newtonsoft.Json;

namespace AwsLambdaErrorHandling.Invocations
{
    [JsonObject]
    public class SuccessLambdaResponseModel
    {
        public string Response { get; }

        public SuccessLambdaResponseModel(string response)
        {
            Response = response;
        }
    }
}
