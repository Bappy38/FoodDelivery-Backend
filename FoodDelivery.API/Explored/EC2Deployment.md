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

**Note:** While running docker container it's a best practice to specify how much RAM and how many CPUs it can use at max. If we don't specify such constraints then the resource distribution accross multiple containers running in that EC2 instance will not fair. It can happen that a single container using all resources of host's, but other containers can't occupy enough resource to run. Even if we run a single container into an EC2 instance, it can consumes all available memory and can cause the host system to become unstable or even crash.

```

docker run -d --cpus="2.0" --memory="1g" --name container-a my-cpu-intensive-app
docker run -d --cpus="1.0" --memory="2g" --name container-b my-memory-intensive-app

```

<hr><br>

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

```

// To get running docker container list
docker ps

// To get real-time logs of docker container
docker logs -f "CONTAINER_ID"

```

<hr><br>

# Integrating Load Balancer

## Build and Push Docker Image to ECR

- Build the Docker Image with the command `docker build -f FoodDelivery.API/Dockerfile -t food-delivery-api:latest .`

- Push the Docker Image to AWS ECR

  - Create a Policy with all necessary permissions for ECR
  - Attach the policy to the user who is going to handle the deployment
  - Install AWS CLI
  - Configure/Login to AWS using AccessKey

  - Go to the `ECR Console`
  - Click "Create Repository" (IF you didn't created already)
  - Name your repository

  - Authenticate docker to ECR like command `aws ecr get-login-password --region your-region | docker login --username AWS --password-stdin your-account-id.dkr.ecr.your-region.amazonaws.com`. You can get this command from `View Push Commands` action of the created repository.
  - Go to project folder and build the docker image with the command `docker build -f FoodDelivery.API/Dockerfile -t food-delivery-api .`
  - Tag and push your docker image to ECR. Maintain versioning as tag so that you can distinguish between build. And can easily deploy an specific build. You can get these commands from `View Push Commands` action of the created repository.

## Deployment with EC2, ECR and EFS

**EFS File System can be shared accross multiple EC2 instances. That's why we are using EFS while trying to configure a Load Balancer. Because, there will be multiple instances to which the Load Balancer will distribute traffic.**

- Create a Security Group for EC2 Instance with default VPC ID
- Create a Security Group for EFS Instance with default VPC ID
- Go to EC2-Security-Group and configure it
  - Create an Inbound Rules of `Type: HTTP | Protocol: TCP | Port-Range: 80 | Source: Custom | 0.0.0.0/0` for Serving HTTP request
  - Create an Inbound Rules of `Type: HTTPS | Protocol: TCP | Port-Range: 443 | Source: Custom | 0.0.0.0/0` for Serving HTTPS request
  - Create an Inbound Rules of `Type: NFS | Protocol: TCP | Port-Range: 2049 | Source: Custom | EFS-Security-Group` for allowing EC2 to accept message from EFS
  - Create an Inbound Rules of `Type: SSH | Protocol: TCP | Port-Range: 22 | Source: Custom | 0.0.0.0/0` for allowing SSH Connect
  - Create an Outbound Rules of `Type: All traffic | Protocol: All | Port-Range: All | Destination: Custom | 0.0.0.0/0` for allowing EC2 to connect with any IP
  - Create an Outbound Rules of `Type: NFS | Protocol: TCP | Port-Range: 2049 | Destination: Custom | EFS-Security-Group` for allowing EC2 to connect with EFS
- Go to EFS-Security-Group and configure it
  - Create an Inbound Rules of `Type: NFS | Protocol: TCP | Port-Range: 2049 | Source: Custom | EC2-Security-Group` for allowing EFS to accept message from EC2
  - Create an Outbound Rules of `Type: All traffic | Protocol: All | Port-Range: All | Destination: Custom | 0.0.0.0/0` for allowing EFS to connect with any IP
- Create an `EFS File System` and assigns the EFS-Security-Group we created earlier.
- Create an `EC2 Instance` by providing our `Startup Script` in `User Data` section to install Docker and Necessary things. Also assigns the EC2-Security-Group we created earlier.
- Inside EC2 instance, install `amazon-efs-utils` package with the command `sudo yum install -y amazon-efs-utils`. **Later, we can add this step in the startup script.**
- Create a local folder named `efs` with the command `sudo mkdir -p /efs` to mount with the efs volume. **Later, we can add this step in the startup script.**
- Go to the EFS file system we created. Click on `Attach` button and copy the mount command from `Mount Via DNS - Using the EFS Mount Helper` section. The command will be something like `sudo mount -t efs -o tls fs-xxxx:/ /efs`. **Don't forget to put a slash before the local folder name `efs`**. Execute this command to mount the EFS File System inside EC2. **Later, we can add this step in the startup script.**
- Create the deployment script named `deploy.sh` with the command `sudo nano /efs/deploy.sh` if it not already exist in the EFS file system. Paste the deployment script. **Don't forget to provide AWS Credentials and ECR Repository Url.**
- Create the environment file of your application like `food-delivery-app-config.json` with the command `sudo nano /efs/food-delivery-app-config.json`. Paste the necessary configs and save it.
- Provide the environment file path in the deployment script like `/efs/food-delivery-app-config.json` and save it.
- Execute the deployment script with the command `/efs/deploy.sh`. Your service will be deployed successfully.

## Integrating Load Balancer

- Create multiple EC2 instances by following above guidelines
- Go to the `EC2 Management Console`
- In the navigation pane, under "Load Balacing", choose "Target Groups".
- Create a Target Group.
	- Name: Enter a name for the target group. Example: Food-delivery-app-instances
	- Target Type: Choose `instance` (other options are IP and lambda)
	- Protocol: Choose HTTP or HTTPS
	- Port: Enter the port number your application listens on (e.g. 80, 443)
	- Health Checks: Configure health check settings (path, protocol, port)
	- Register Targets: Select the instances you want to include in the target group. Ensure these instances are running in the selected availability zones.
- In the navigation pane, under "Load Balancing", choose "Load Balancers"
- Create a `Application Load Balancer`
- Configure Basic Settings:
	- Name: Enter a name for your load balancer.
	- Scheme: Choose "Internet-facing"
	- IP Address Type: Choose "IPv4"
- Configure Listeners and Availability Zones:
	- Listeners: Add a listeners for HTTP or HTTPS (port 80 or 443)
	- Availabilty Zones: Select your VPC (default) and at least two availabilty zones with associated subnets.
- Configure Security Settings: (For HTTPS)
	- Select an SSL Certificate: IF you're using HTTPS, select an existing SSL certificate from AWS Certificate Manager (ACM) or upload a new certificate
- Configure Security Groups: Choose an existing security group or create a new one that allows inbound traffic on the ports your load balancer will listen on (e.g. HTTP, HTTPS).
- Once all these steps done, copy the DNS name of load balancers and make api call to test.

<hr><br>

# Improvement (TODO)

- Try hosting a database by creating EBS (Elastic Block Store) volume
