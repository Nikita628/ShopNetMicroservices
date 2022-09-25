# ShopNetMicroservices

Proof of concept online shop application.


# Architechture of the app.
Core consists of several microservices separated by domain features:

- Catalog
- Basket
- Discount
- Order

Each microservice has its own database.

Some of the microservices connected to RabbitMQ, in order to pub\sub on events.

The core sits behind api gateway wich is implemented with Ocelot.

UI part is implemented with Asp Net WebApp.

All the services are able to run inside docker containers.
Each service has its own Dockerfile, and the whole app is fully contanerized with docker compose.

Some infrastructure management tools are added and running in docker containers as well,
such as - Portainer (container management tool), PgAdmin (Postgres DB management tool).

Main languages and technologies - C# .Net6, Docker, Entity Framework, MediatR + CQRS.

# To run the app:
- make sure that Docker is up and running on your local machine
- docker-compose -f docker-compose.yml up -d
- open localhost:5007
