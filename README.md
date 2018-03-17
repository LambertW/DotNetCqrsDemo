# DotNetCqrsDemo
## BG

This project is followed the articles series [Real-World CQRS/ES with ASP.NET and Redis ](https://www.exceptionnotfound.net/real-world-cqrs-es-with-asp-net-and-redis-part-1-overview/) which is post by  @exceptionnotfound.

You can also found the origin project [here](https://github.com/exceptionnotfound/DotNetCqrsDemo).

## Why create this project

 @exceptionnotfound post the articles 2 years ago and it has a lot of changes with the tech.

Such like:

1. asp.net -> asp.net core
2. 3rd party DI container **StructureMap** -> MS DI.
3. CQRSlite methods upgrade for asp.net core.
4. AutoMapper version upgrade which has some break change.

and so on. So I try to fix the project's problems when I read the articles.

## How to Start

1. Read the articles first, then try to compare the projects.

2. In this project, we use redis to store the event data. If you had install **Docker**, you can start  the container by 

   ```
   docker run --name local-redis -d -p 6379:6379 redis:4.0
   ```

3. Run the projects **Commands**, **Queries** with **Multiple startup projects** setting.

4. Then **start new instance** with **Initializer** project(also you can skip this step).

5. Use **Postman** to try to add data from http://localhost:53958/api/ and receive data from http://localhost:58167/api/ .

6. Use RavenDB to instead of **InMemoryEventStore**. You can start like this:

   1. Use docker to start the RavenDB container.

   ```
   docker run -d -e PUBLIC_SERVER_URL=http://172.17.0.2:8080 -e PUBLIC_TCP_SERVER_URL=http://172.17.0.2:38888 -p 8080:8080 -p 38888:38888 ravendb/ravendb:ubuntu-latest
   ```
   2. Setup the RavenDB with https://localhost:8080 , then create the new document **Northwind** (You can change the document name and then change the appsettings.json **Raven section** in **Commands** project.)


## Finanlly

Happy Coding!