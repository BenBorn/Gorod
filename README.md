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
In this case both services have there one contracts.

## HttpClient:
[Example](2_HttpClient) contains communication via HttpClient.  
The contracts will be shared between both services.

## Masstransit:
[Example](3_ServiceBus) contains communication via HttpClient and ServiceBus.  
