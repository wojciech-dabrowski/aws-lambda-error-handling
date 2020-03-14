using System;

namespace AwsLambdaErrorHandling.Functions
{
    public class FailingLambda
    {
        public void Invoke() => throw new Exception("Exception thrown during AWS Lambda function runtime.");
    }
}
