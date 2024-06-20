# Local Setup

- Pull the PostgreSQL docker image by the command `docker pull postgres`.
- Run the PostgreSQL container by the command `docker run --name postgres-container -e POSTGRES_PASSWORD=admin1234 -e POSTGRES_USER=admin -e POSTGRES_DB=FoodDeliveryDB -p 5432:5432 -d postgres`.
- Create migration with the command `dotnet ef migrations add {migrationName} --project {path to project folder}`. For example in our case, `dotnet ef migrations add InitialCreate --project "FoodDelivery.API"`.
- Apply the migration to the database to bring it up to date with the current data model by the command `dotnet ef database update --project {path to project folder}`.
- IF we need to revert the database to a previous state, we can specify the migration to roll back with the command `dotnet ef database update {PreviousMigrationName}`
- We can also remove last applied migration with the command `dotnet ef migrations remove`
- Install pgAdmin to access your postgres instance. Provide username, password, hostname to connect to the DB instance.

# Render's Postgres Server Setup

- Create Postgres Server at Render
- Find host, database, username, password from PSQL Command
- Now, apply the already created migration from your local if there's any changes. By doing this, your seeded data and schema changes will pushed to the cloud server.
- Now connect your server with PGAdmin. And check that the necessary changes propagated to cloud db server.
- Provide the connection string to the environment variable of your backend server.

That's it

# Hosting Postgres Server in AWS EC2