# Gorod
Examples of "Microservices communication"

# Installation
## RabbitMQ:
To run RabbitMQ in docker run following command:
```
docker run -p 5673:5672 -p 15673:15672 rabbitmq:3.7-management
```

# Example
## Basic Http:
[Example](1_BasicHttp) contains communication via Http between two service.
In this case both service have there one contracts.
