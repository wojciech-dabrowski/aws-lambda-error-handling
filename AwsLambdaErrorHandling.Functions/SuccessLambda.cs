namespace AwsLambdaErrorHandling.Functions
{
    public class SuccessLambda
    {
        public dynamic Invoke() => new { Response = "AWS Lambda function invoked correctly!" };
    }
}
