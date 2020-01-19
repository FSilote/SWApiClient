# Kneat Code Challenge
#### Calculating stops required to cover given distance. Starships infomation provided by http://swapi.co/

### Language and IDE Information

* This project was built using Visual Studio 2017 and implemented in C#. The platform used was .NET Core with a .NET Core console application and some .NET Standard class libraries as achitecture description below.
* I used a Command/Handler pattern to do application actions. This pattern was implemented using MediatR (https://www.nuget.org/packages/MediatR/). Moreover Dependency Injection was supplied with Autofac (https://www.nuget.org/packages/Autofac) 
* Architectural design was based in Hexagonal Pattern (https://reflectoring.io/spring-hexagonal/) with the Application Layer accessing Infrastructure components.

### Application Structure

* ConsoleApp (Kneat.SW.ConsoleApp) => Provides a console application interface.
* Application (Kneat.SW.Application) => Provides commands and handles that do application actions.
* Infrastructure (Kneat.SW.Infrastructure) => Provides access to external services such as Databases, External Api's, etc.
* Domain (Kneat.SW.Domain) => Provides entity classes and common interfaces to be injected in other layers.
* Ioc (Kneat.SW.Ioc) => Provides IoC configuration and bootstraps to application startup.
* Tests (Kneat.SW.Domain.Tests and Kneat.SW.Ioc.Tests) => Provides unit tests to respective layers.

### Running

* Clone repository or download the source.
* Open solution file (KneatCodeChallenge.sln) in Visual Studio.
* Make sure ConsoleApp is the "startup project"
* Run project

### Program Options

After running program the options below will be shown:

1.  Show all starships => Use this option if you want load and show all information about every available starship.
2.  Find starship by id => Use this option if you want load and show all information about a specific starship. After choose this option, the starship id will be asked for you.
3.  Show stops required to resupply (all starships) => Use this option if you want calculate the number of stops required to resupply for all available starships to travel a given distance. After choose this option, the distance will be asked for you.
4.  Show stops required to resupply (specific starship) = > Use this option if you want calculate the number of stops required to resupply a specific starship to travel a given distance. After choose this option, the starship id and the distance will be asked for you.