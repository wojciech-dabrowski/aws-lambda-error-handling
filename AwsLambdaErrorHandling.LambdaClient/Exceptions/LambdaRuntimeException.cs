using System;

namespace AwsLambdaErrorHandling.LambdaClient.Exceptions
{
    public class LambdaRuntimeException : Exception
    {
        private const string ExceptionMessage = "Lambda call failed in runtime.";

        public LambdaRuntimeErrorModel RuntimeError { get; }

        public LambdaRuntimeException(LambdaRuntimeErrorModel runtimeError) : base(ExceptionMessage)
        {
            RuntimeError = runtimeError;
        }
    }
}
