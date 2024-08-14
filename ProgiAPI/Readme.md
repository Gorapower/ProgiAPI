# ProgiAPI

ProgiAPI is an ASP.NET Core Web API project designed to run as an AWS Lambda function, exposed through Amazon API Gateway. This project demonstrates how to use the `Amazon.Lambda.AspNetCoreServer` package to translate requests from API Gateway into the ASP.NET Core framework and return responses back to API Gateway.

## Project Structure

- **serverless.template**: AWS CloudFormation Serverless Application Model template file for declaring your Serverless functions and other AWS resources.
- **aws-lambda-tools-defaults.json**: Default argument settings for use with Visual Studio and command line deployment tools for AWS.
- **LambdaEntryPoint.cs**: Class that derives from `Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction`. This file bootstraps the ASP.NET Core hosting framework. Change the base class to `Amazon.Lambda.AspNetCoreServer.ApplicationLoadBalancerFunction` when using an Application Load Balancer.
- **LocalEntryPoint.cs**: Contains the executable Main function for local development, bootstrapping the ASP.NET Core hosting framework with Kestrel.
- **Startup.cs**: Usual ASP.NET Core Startup class used to configure the services ASP.NET Core will use.
- **appsettings.json**: Configuration file used for local development.
- **Controllers\MainController.cs**: Example Web API controller to handle basic funcionality.

## Configuration

### API Gateway HTTP API

API Gateway supports both REST API and HTTP API. HTTP API supports two different payload formats. When using the 2.0 format, the base class of `LambdaEntryPoint` must be `Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction`. For the 1.0 payload format, the base class is the same as REST API, which is `Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction`.

**Note:** When using the `AWS::Serverless::Function` CloudFormation resource with an event type of `HttpApi`, the default payload format is 2.0, so the base class of `LambdaEntryPoint` must be `Amazon.Lambda.AspNetCoreServer.APIGatewayHttpApiV2ProxyFunction`.

## Development and Deployment

### Visual Studio

To deploy your Serverless application, right-click the project in Solution Explorer and select *Publish to AWS Lambda*.

To view your deployed application, open the Stack View window by double-clicking the stack name shown beneath the AWS CloudFormation node in the AWS Explorer tree. The Stack View also displays the root URL to your published application.

### Command Line

1. Install Amazon.Lambda.Tools Global Tools if not already installed:
    ```sh
    dotnet tool install -g Amazon.Lambda.Tools
    ```

2. If already installed, check if a new version is available:
    ```sh
    dotnet tool update -g Amazon.Lambda.Tools
    ```

3. Execute unit tests:
    ```sh
    cd "ProgiAPI/test/ProgiAPI.Tests"
    dotnet test
    ```

4. Deploy the application:
    ```sh
    cd "ProgiAPI/src/ProgiAPI"
    dotnet lambda deploy-serverless
    ```

## License

This project is licensed under the MIT License.