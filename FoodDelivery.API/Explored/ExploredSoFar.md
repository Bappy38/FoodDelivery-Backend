# Structured Logging

- Implemented Structured Logging using Serilog Library.
- Used three Log Sinks Console, File, ElasticSearch. (Integrating ESearch - TODO)
- Added a middleware to generate request correlation id if not exist already so that can easily monitor request log accross micro-services. And returning that CorrelationId in the response of that request. Propagating that CorrelationId through ExecutionContext so that we can find that inside the LogEnricher.

# Deployments

## Render Deployments

- Go to Render Dashboard
- Click on New and `Select Web Service`
- Then between two options choose `Build and Deploy from a Github Repository`
- Then connect to your github repository
- Then configure the unique name to your web service, region, branch, root directory, runtime (Docker), instance type and environment variables.
- Remember, when we select docker as the runtime, the repository must have the Dockerfile.

Thats all we need to deploy a web service at Render.

Render provides built-in CI to us. That means whenever we push some changes to the branch we selected as deployment config, Render will build the images and deploy the changes automatically.

## AWS Deployments

Check `EC2Deployment.md` file for detail