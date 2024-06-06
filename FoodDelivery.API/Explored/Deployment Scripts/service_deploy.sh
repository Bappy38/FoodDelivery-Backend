#!/bin/bash

# Exit immediately if a command exits with a non-zero status
set -e

# Predefined variables
aws_access_key_id="YOUR_AWS_ACCESS_KEY_ID"
aws_secret_access_key="YOUR_AWS_SECRET_ACCESS_KEY"
aws_default_region="ap-south-1"
aws_output_format="json"
ecr_repo_uri="YOUR_ECR_REPO_URI"
docker_image_tag="latest"
app_port="8080"
env_file_path="/path/to/your/appsettings.json"

# Function to configure AWS CLI
configure_aws_cli() {
  echo "Configuring AWS CLI..."

  # Configure AWS CLI with the predefined credentials and settings
  aws configure set aws_access_key_id "$aws_access_key_id"
  aws configure set aws_secret_access_key "$aws_secret_access_key"
  aws configure set region "$aws_default_region"
  aws configure set output "$aws_output_format"

  echo "AWS CLI configured successfully!"
}

# Function to pull the docker image from ECR and run docker container
run_docker_container() {
  echo "Logging in to ECR..."

  # Login to ECR
  aws ecr get-login-password --region "$aws_default_region" | docker login --username AWS --password-stdin "$ecr_repo_uri"
  
  # Check if the login was successful
  if [[ $? -ne 0 ]]; then
    echo "ECR login failed."
    exit 1
  fi

  echo "ECR login successful."

  # Pull the Docker image
  echo "Pulling Docker image $ecr_repo_uri:$docker_image_tag..."
  docker pull "$ecr_repo_uri:$docker_image_tag"

  # Run the Docker container
  echo "Running Docker container..."
  docker run -v "$env_file_path:/app/appsettings.json" -d -p 80:"$app_port" "$ecr_repo_uri:$docker_image_tag"

  echo "Docker container running on port $app_port"
}

# Main script execution
configure_aws_cli
run_docker_container

echo "Service Deployed"
