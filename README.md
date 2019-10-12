# SodaMachine
A demo ASP.NET Core Web API consumed by an Angular CLI v6 app 

.NET Core 2.2 WebAPI contains API controllers providing endpoints for operations to be performed on a Soda Machine. Operations are carried out by MachineCommand class in business layer and soda machine's state is persisted using SodaMachineDbService which is injected into API Controllers using ASP.NET Core Dependency Injection.
The database is an in-memory database seeded by hardcoded vaules for different sodas.

![Screenshot1](https://user-images.githubusercontent.com/25904133/66692933-92e7cd00-eca3-11e9-944d-97e6c1b75e7f.png)

The Angular ClientApp is a simpler implementation. The fetch-data component makes GET and POST requests to API endpoints and updates UI.

![Screenshot2](https://user-images.githubusercontent.com/25904133/66692942-b7dc4000-eca3-11e9-9a77-12b246027147.png)