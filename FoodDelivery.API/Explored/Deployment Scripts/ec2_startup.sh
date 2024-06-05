#!/bin/bash

# Check if the script is run as root. If not, use sudo to switch to root
if [ "$EUID" -ne 0 ]; then
  echo "Please run as root"
  sudo su
fi

# Install Docker
yum install -y docker

# Start the Docker service
systemctl start docker.service

# Enable Docker service to start on boot
systemctl enable docker.service

# Print Docker status
systemctl status docker.service
