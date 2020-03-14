# Pre requirements
To use all examples in the repository you have to properly configure your environment.

1. [AWS Command Line Interface](#AWS-Command-Line-Interface)
2. [.NET Core SDK](#NET-Core-SDK)
3. [AWS Lambda .NET](#AWS-Lambda-NET)

## AWS Command Line Interface
You will need AWS CLI in your local machine to perform actions (e.g. create new resources, deploy your code) to your AWS account.

1. Download and install _AWS CLI_ from: https://aws.amazon.com/cli
2. Generate `AWS Access Key ID` and `AWS Secret Access Key` for your AWS user and configure AWS CLI by running `aws configure` command. More info at: https://docs.aws.amazon.com/cli/latest/userguide/cli-chap-configure.html
3. Make sure if your CLI was correctly set up by running some simple command, for example listing all your S3 Buckets: `aws s3 ls`

## .NET Core SDK
You will need _.NET Core SDK_ to build backend application.

1. Download and install the newest version of _.NET Core SDK_ from: https://dotnet.microsoft.com/download
2. Make sure if _.NET Core SDK_ was installed correctly by running command: `dotnet --version`

## AWS Lambda .NET
You will need _AWS Lambda .NET_ tool to deploy your .NET Core binaries to the AWS.

1. After installing [.NET Core SDK](#NET-Core-SDK), run command: `dotnet tool install -g Amazon.Lambda.Tools`
2. Make sure if _AWS Lambda .NET_ tool was installed correctly by running command: `dotnet-lambda`