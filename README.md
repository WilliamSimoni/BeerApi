# Archiitecture

The API is implemented using a layered architecture consisting of four levels:
- The domain layer defines core aspects of the domain, such as entities and errors. 
- The infrastructure layer is an abstraction between the domain and the service layer. 
- The service layer implements the business logic of the application.
- The controller layer orchestrates the received request to the proper service. 
Each layer communicates with the layer below using interfaces, guaranteeing the dependency inversion principle. 

## Domain Layer
The image below shows the database scheme used by the application. (Image to insert)


The beer table has an additional index with a unique constraint composed of the Name, the BreweryId, and the OutOfProductionDate.
The OutOfProductionDate is used to implement the soft deletion of a beer. 

When a beer is created, the OutOfProduction Date is set to Date.MinValue. 

When a beer is deleted, the business logic sets the InProduction flag to false and the OutOfProductionDate to the current date. 

A brewery can not add a new beer with the same name as an in-production beer since it will have the same OutOfProductionDate (equal to Date.MinValue).
On the other hand, it can add a new beer with the same name as a deleted beer since the OutOfProductionDate will be different. 

## Infrastructure Layer
The infrastructure layer implements the repository pattern creating an abstraction between the data layer and the service layer. 
For each table, there are two repositories:
- QueryRepository: allows querying the table with two base methods: GetAll and GetByCondition.
- CommandRepository: allows changing the entities with three base methods: Add, Remove and Update.

The UnitOfWork wraps in one object all the repositories and offers the additional saveAsync method.

## Service Layer
As for the infrastructure layer, the services are divided into query and command services (Except for the QuoteService, which has only queries). Query services offer methods to get data; Command services provide methods to change data. 

# Error Handling
The service layer can return to the controller layer different kinds of errors defined under the IError interface (defined in domain.common.errors).
To implement this, I used the OneOf package that allows defining methods with more than one returning type. 

A Global Error Handler is also used to send back to the client a 500 internal server error whenever an exception occurs. 
The global error handler is implemented as a controller. When there is an exception, a middleware redirects the request to the error endpoint, which the ErrorController handles. 
