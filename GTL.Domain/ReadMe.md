# Domain Layer


Vores inderste lag, der ikke kender til nogle ydre lag. Indeholder Entities (domain objects), Domain exceptions, Logic og ValueObjects

Value objects er en udvidelse af primitive typer. 
Se https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects

Vi bruger IKKE data annotations i vores entity, fordi det er en persistency concern.

