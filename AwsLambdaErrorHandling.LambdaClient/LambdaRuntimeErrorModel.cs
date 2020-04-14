namespace AwsLambdaErrorHandling.LambdaClient
{
    public class LambdaRuntimeErrorModel
    {
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public string[] StackTrace { get; set; }
    }
}
