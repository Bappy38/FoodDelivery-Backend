# Deployment using ECR and EC2

- Prepare your .NET WebAPI as a Docker container.
	- Build the Docker Image with the command `docker build -f FoodDelivery.API/Dockerfile -t food-delivery-api:latest .`
	- Run the Docker Container with the command `docker run -d -p 80:8080 -e DashboardUrl=http://localhost:1234 food-delivery-api:latest`

- Push the Docker Image to AWS ECR
	- Create a Policy with all necessary permissions for ECR
	- Attach the policy to the user who is going to handle the deployment
	- Install AWS CLI
	- Configure/Login to AWS using AccessKey

	- Go to the `ECR Console`
	- Click "Create Repository"
	- Name your repository

	- Authenticate docker to ECR like command `aws ecr get-login-password --region your-region | docker login --username AWS --password-stdin your-account-id.dkr.ecr.your-region.amazonaws.com`. You can get this command from `View Push Commands` action of the created repository.
	- Go to project folder and build the docker image with the command `docker build -f FoodDelivery.API/Dockerfile -t food-delivery-api .`
	- Tag and push your docker image to ECR. You can get these commands from `View Push Commands` action of the created repository.

- Set Up Your EC2 Instance
	- Go to EC2 Dashboard
	- Launch Instance
	- Keep all config as default for now
	- We will access instance from AWS Dashboard, so no need to create key pair

- Set Up EC2 Environment
	- Switch to super user by the command `sudo su`
	- Install docker by the command `yum install docker`.
	- Start docker service by the command `sudo systemctl start docker.service`
	- Check if the docker is running by the command `docker ps`
	- Configure AWS like before at the console of EC2
	- Authenticate Docker Client to ECR like we did while pushing docker image from local. You will find the command in the `View Push Commands` action of respective repository at ECR
	- Pull the docker image from ECR with the command `docker pull your-account-id.dkr.ecr.your-region.amazonaws.com/yourapp-repo:latest`
	- Run the docker container with the command `docker run -d -p 80:8080 your-account-id.dkr.ecr.your-region.amazonaws.com/yourapp-repo:latest`. Don't forget to provide environment variable if you have any.
	- Don't forget to expose port. 80 for HTTP request, 443 for HTTPs request. Also run the docker container accordingly, means map to relevant port, 80 for HTTP, 443 for HTTPS.

<hr>

# Automation

We can automate this deployment process like below:
- We can write a bash script which will be responsible for installing all the necessary thing like docker at the startup of an EC2 instance. There's a way we can upload this script while creating the EC2 instance.
- We can write a bash script which will be responsible for Authenticating Docker Client to ECR, Pull the latest image of specified service from ECR, Run the docker container of pulled image with required port mapping. Then when we need to deploy, we will just run this script within our EC2 instance. Also, there can be a similar kind of script which will build and push the docker image to ECR from local machine.