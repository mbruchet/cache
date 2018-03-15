# Introduction 
Caching can significantly improve the performance and scalability of an app by reducing the work required to generate content. Caching works best with data that changes infrequently. Caching makes a copy of data that can be returned much faster than from the original source. You should write and test your app to never depend on cached data
Hopex Platform supports several differents caches. Basicaly the platform is provided with three providers : 
- <b>Redis</b> (DistributedRedisCacheRegister)
- <b>SQL Server</b> (DistributedSqlServerCacheRegister)
- <b>Hopex</b> (HopexCacheClientRegister) - reliable Microservice shareable between several machine and nodes)

# Build status

![Build Status](https://megainternational.visualstudio.com/_apis/public/build/definitions/af6cd424-225a-48a0-b165-a19a1b4bccf4/64/badge)


# Architecture

The service is based on :
- <b>ICacheClientRegister</b>
> This interface is used to register a cache provider on the platform side and to be rendered is usable by the others microservices
- <b>HopexCacheDistributed</b> 
> This class is used to render the service. 
> This class will be use to communicate with the reliable cache microservice
- <b>CacheServiceOptions</b>
> This POCO enables to configure the services

> You can specify your register by specifying the property CacheTypeName in format <b>Type, assembly</b>.
>You can also specify the environment, the language and the connection string to be used by your service

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

# Create your own service
The Cache Microservice is designed to be overrided quickly and easily by yourself.