#!/bin/bash

# Function to configure AWS CLI
configure_aws_cli() {
  read -p "Enter AWS Access Key ID: " aws_access_key_id
  read -s -p "Enter AWS Secret Access Key: " aws_secret_access_key
  echo
  read -p "Enter Default region name [ap-south-1]: " aws_default_region
  aws_default_region=${aws_default_region:-us-west-2}
  read -p "Enter Default output format [json]: " aws_output_format
  aws_output_format=${aws_output_format:-json}

  echo "Configuring AWS CLI..."
  
  # Configure AWS CLI with the provided credentials and settings
  aws configure set aws_access_key_id "$aws_access_key_id"
  aws configure set aws_secret_access_key "$aws_secret_access_key"
  aws configure set region "$aws_default_region"
  aws configure set output "$aws_output_format"

  echo "AWS CLI configured successfully!"
}

# Function to pull the docker image from ECR and run docker container

run_docker_container() {
  read -p "Enter your AWS ECR repository URI (e.g., <aws_account_id>.dkr.ecr.<region>.amazonaws.com/your-repo): " ecr_repo_uri
  read -p "Enter the Docker image tag [latest]: " docker_image_tag
  docker_image_tag=${docker_image_tag:-latest}
  read -p "Enter the port on which your application listens [443]: " app_port
  app_port=${app_port:-443}
  read -p "Enter the .env file path: " env_file_path

  # Login to ECR
  aws ecr get-login-password --region $aws_default_region | docker login --username AWS --password-stdin $ecr_repo_uri

  # Pull the Docker image
  docker pull $ecr_repo_uri:$docker_image_tag

  # Run the Docker container
  docker run --env-file env_file_path  -d -p 443:$app_port $ecr_repo_uri:$docker_image_tag

  echo "Docker container running on port $app_port"
}

# Main script execution
configure_aws_cli
run_docker_container

echo "Service Deployed"