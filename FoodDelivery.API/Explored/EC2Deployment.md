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
	- Tag and push your docker image to ECR. Maintain versioning as tag so that you can distinguish between build. And can easily deploy an specific build. You can get these commands from `View Push Commands` action of the created repository.

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

<hr><br><br>

# Automation

We can automate this deployment process like below:
- We can write a bash script which will be responsible for installing all the necessary thing like `docker`, `ElasticSearch Agent` at the startup of an EC2 instance. There's a way we can upload this script while creating the EC2 instance.
- We can write a bash script which will be responsible for Authenticating Docker Client to ECR, Pull the latest image of specified service from ECR, Run the docker container of pulled image with required port mapping. Then when we need to deploy, we will just run this script within our EC2 instance. Also, there can be a similar kind of script which will build and push the docker image to ECR from local machine.

## Creating Instance with Automated Setup Bash Script

Create an EC2 instance like above. Only difference is while creating an instance, there will be a input box / upload file option named `User Data` in `Advanced Detail` section. Paste the script you want to run while spinning up the instance.

## Push Docker Image to ECR

Push your docker image to ECR with the same approach described above.

## Deployment with Automated Bash Script

- Create a EBS volume from Volumes section of EC2 Dashboard
- Attach the volume to the intended EC2 instance
- List the available disk devices to find the newly attached volume with the command `lsblk`. Identify the newly attached volume. It will likely be something like `/dev/xvdf`.
- Use the command `sudo file -s /dev/xvdf` to get the file system of the volume. It will shows only the word `data` if the volume has no file system. In that case, we can create file system with the command `sudo mkfs -t xfs /dev/xvdf`
- Create a mount point directory for the volume with command `sudo mkdir /mnt/data`
- Mount the volume to the mount point directory with command `sudo mount /dev/xvdf /mnt/data`. You can find from volume section whether your volume is `/dev/xvdbf` or not.
- Verify that the volume is mounted correctly by listing the contents of the mount point directory with command `ls /mnt/data`
- Configure the volume to automatically mount on reboot by adding an entry to the file `/etc/fstab`. Search for details.
- Type command `sudo nano /mnt/data/myscript.sh` to create a script and paste the desired script. Press `Ctrl+X` to save, then `Enter` to confirm the filename and exit.
- Change the file permissions to make the script executable with the command `sudo chmod +x /mnt/data/myscript.sh`
- Create environment/appsettings.json file using the same way creating the script file. And provide this file path while executing deployment script.
- Run the script with the command `/mnt/data/myscript.sh`


## Check Container Logs


<hr><br>

# Integrating Load Balancers

<hr><br>

# Improvement (TODO)

- Try adding a Load Balancer in front of multiple AWS Instance
- Try hosting a database by creating EBS (Elastic Block Store) volume