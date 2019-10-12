# SodaMachine
A demo ASP.NET Core Web API consumed by an Angular CLI v6 app 

.NET Core 2.2 WebAPI contains API controllers providing endpoints for operations to be performed on a Soda Machine. Operations are carried out by MachineCommand class in business layer and soda machine's state is persisted using SodaMachineDbService which is injected into API Controllers using ASP.NET Core Dependency Injection.
The database is an in-memory database seeded by hardcoded vaules for different sodas.

Angular ClientApp is a simpler implementation. The fetch-data component makes GET and POST requests to API endpoints and updates UI.

![Screenshot1]()
