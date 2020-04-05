dotnet restore
dotnet publish -c RELEASE
dotnet lambda deploy-serverless `
    --region eu-west-1 `
    --configuration Release `
    --framework netcoreapp2.1 `
    --template application.yaml `
    --s3-bucket your-deploy-bucket `
    --stack-name aws-lambda-error-handling `
    --stack-wait true `
    --profile default