# Local Setup

- Pull the PostgreSQL docker image by the command `docker pull postgres`
- Run the PostgreSQL container by the command `docker run --name postgres-container -e POSTGRES_PASSWORD=admin1234 -e POSTGRES_USER=admin -e POSTGRES_DB=FoodDeliveryDB -p 5432:5432 -d postgres`