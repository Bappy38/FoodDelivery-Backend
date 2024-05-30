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

### Create an EC2 Instance

- Login to your AWS Management Console and search for EC2
- Click on `Launch Instance`
- Name your Instance
- Select `AWS Linux`
- Select `t2.micro` as instance type
- Create a key-pair to connect your instance securely. Select `food-delivery` as name `RSA` as key-pair type and `.ppk` as private key file format.
- Then Launch the Instance

### Connect to the Instance

- We can connect to the instance from AWS Management Console

### Setting Up the Environment

- Let's switch to the Super User by typing the command `sudo su`.
- Install git by the command `yum install git`. The command tells the package manager to search for the Git package in the repositories configured on the system, download it and install it along with any dependencies it requires.
- Install docker by the command `yum install docker`.
- Start the docker service on linux by the command `sudo systemctl start docker.service`.
- Check whether the docker service is running or not by the command `docker ps`.
- 


- Download the .NET SDK installer script by the command `wget https://dot.net/v1/dotnet-install.sh`.
- Run the installer script with the desired .NET version to install by the command `bash dotnet-install.sh --runtime aspnetcore --version 8.0.0`.
- Run the installer script again to install the .NET SDK for .NET 8 by the command `bash dotnet-install.sh --install-dir /usr/share/dotnet --version 8.0.301`