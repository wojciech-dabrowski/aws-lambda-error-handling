using System;

namespace AwsLambdaErrorHandling.LambdaClient.Exceptions
{
    public class LambdaInvocationException : Exception
    {
        private const string ExceptionMessage = "Lambda call failed during invocation.";

        public LambdaInvocationException(Exception innerException) : base(ExceptionMessage, innerException) {}
    }
}
