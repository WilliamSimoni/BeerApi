# Architecture

The API is implemented using a layered architecture consisting of four levels:
- The domain layer defines core aspects of the domain, such as entities and errors. 
- The infrastructure layer is an abstraction between the domain and the service layer. 
- The service layer implements the business logic of the application.
- The controller layer orchestrates the received request to the proper service. 

Each layer communicates with the layer below using interfaces, guaranteeing the dependency inversion principle. 

## Domain Layer
The image below shows the database diagram used by the application.

<img src="https://github.com/WilliamSimoni/BeerApi/blob/master/Images/Database.png" alt="Database logical diagram" title="Database logical diagram">

The beer table has an additional index with a unique constraint composed of the Name, the BreweryId, and the OutOfProductionDate.

The application implements soft deletion of beers (instead of hard deletion) to guarantee functionalities such as the possibility of knowing the information of a beer involved in a sale.

When a beer is created, the InProduction flag is set to false, and the OutOfProduction Date is set to Date.MinValue. 

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
As for the infrastructure layer, the services are divided into query and command services (Except for the QuoteService, which has only one query). Query services offer methods to get data; Command services provide methods to change data. 

Finally,  ServiceWrapper wraps in one object all the services.

## Controller Layer
Again two types of controllers: query and command. The client communicates to the controller complex objects using DTOs. The same happens in the communication between controllers and the client. For instance, to send the list of sales, instead of sending an IEnumerable of Sales, the server will transmit an IEnumerable of GetSaleDtos.

Also, the communication between the controller and the service layer uses DTOs.

# Error Handling
The service layer can return to the controller layer different kinds of errors defined under the IError interface (defined in domain.common.errors).
To implement this, I used the OneOf package that allows defining methods with more than one returning type. 

A Global Error Handler is also used to send back to the client a 500 internal server error whenever an exception occurs. 
The global error handler is implemented as a controller. When there is an exception, a middleware redirects the request to the error endpoint, which the ErrorController handles. 

# Unit Tests

I wrote several unit tests (138) to verify the correctness of the controller and the service layer. 
For each test, I used:
- The Moq library to generate and customize Mocks. In detail, for the controller layer tests, I mocked the service layer, while for the service layer tests, I mocked the infrastructure layer. 
- The FluentAssertion library to specify the expected test outcome naturally. 

# Other information
- The entities' data was stored in a SQL server database. To run the project, change the connection string in appsetting.json. 
- The app initializes and seeds the database when you first start it.
- To map DTOs to entities and vice versa, I used the Mapster library.
- Logs are written in a log file using the Serilog library.
- Error responses from the server follow the RFC standard (https://tools.ietf.org/html/rfc7807).

# API doc
In addition to the required functionalities, I implemented several endpoints to navigate the resources. The first part of this section will describe the necessary endpoints, while the second part will focus on the API's additional query endpoints. 
**You can find the definition of the DTOs in the contracts/DTOs directory**

## Required

### Get list of all the beers by brewery

- **URL**

  */api/breweries/{breweryId}/beers*
  
- **METHOD**

  `GET`

- **URL PARAMS**

  `breweryId = {Integer}`

- **Success Response**

  - **Code**: 200
  - **Content**: `IEnumerable of BeerDto`

- **Error Response**

  - **Code**: 404 NOT FOUND

- **Sample Call**

```
  curl -X 'GET' \
  'https://localhost:7224/api/breweries/1/beers' \
  -H 'accept: text/plain; ver=1.0'
```

### Add new beer

- **URL**

  */api/breweries/{breweryId}/beers*
  
- **METHOD**

  `POST`

- **URL PARAMS**

  `breweryId = {Integer}`

- **DATA PARAMS**

  - **Body data**: `ForCreationBeerDto`

- **Success Response**

  - **Code**: 201
  - **Content**: `CreatedBeerDto`

- **Error Response**

  - **Code**: 400 BAD REQUEST
  - **Code**: 404 NOT FOUND 

- **Sample Call**

```
curl -X 'POST' \
  'https://localhost:7224/api/breweries/1/beers' \
  -H 'accept: text/plain; ver=1.0' \
  -H 'Content-Type: application/json; ver=1.0' \
  -d '{
  "name": "birra test",
  "alcoholContent": 78,
  "sellingPriceToWholesalers": 6.01,
  "sellingPriceToClients": 3.01
}'
```

### Delete a beer

- **URL**

  */api/breweries/{breweryId}/beers/{beerId}*
  
- **METHOD**

  `DELETE`

- **URL PARAMS**

  `breweryId = {Integer}`
  
  `beerId = {Integer}`

- **Success Response**

  - **Code**: 204

- **Error Response**

  - **Code**: 404 NOT FOUND 

- **Sample Call**

```
curl -X 'DELETE' \
  'https://localhost:7224/api/breweries/1/beers/6' \
  -H 'accept: */*'
```

### Add a sale of an existing beer to an existing wholesaler

- **URL**

  */api/sales*
  
- **METHOD**

  `POST`

- **DATA PARAMS**

  - **Body data**: `ForCreationSaleDto`

- **Success Response**

  - **Code**: 201
  - **Content**: `CreatedSaleDto`

- **Error Response**

  - **Code**: 400 BAD REQUEST

- **Sample Call**

```
curl -X 'POST' \
  'https://localhost:7224/api/sales' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json; ver=1.0' \
  -d '{
  "wholesalerId": 1,
  "beerId": 1,
  "numberOfUnits": 100,
  "pricePerUnit": 3.99,
  "discount": 10
}'
```

### Update remaining quantity of a beer in his stock

- **URL**

  */api/wholesalers/{wholesalerId}/beers/{beerId}*
  
- **METHOD**

  `PATCH`

- **URL PARAMS**

  `wholesalerId = {Integer}`
  
  `beerId = {Integer}`
  
- **DATA PARAMS**

  - **Body data**: `ForUpdateInventoryBeerDto`

- **Success Response**

  - **Code**: 200
  - **Content**: `UpdatedInventoryBeerDto`

- **Error Response**

  - **Code**: 400 BAD REQUEST
  - **Code**: 404 NOT FOUND

- **Sample Call**

```
curl -X 'PATCH' \
  'https://localhost:7224/api/wholesalers/1/beers/1' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json; ver=1.0' \
  -d '{
  "quantity": 10
}'
```

### Request a quote from a wholesaler

- **URL**

  */api/quotes*
  
- **METHOD**

  `POST`
  
- **DATA PARAMS**

  - **Body data**: `QuoteRequestDto`

- **Success Response**

  - **Code**: 200
  - **Content**: `QuoteSummaryDto`

- **Error Response**

  - **Code**: 400 BAD REQUEST

- **Sample Call**

```
curl -X 'POST' \
  'https://localhost:7224/api/quotes' \
  -H 'accept: text/plain; ver=1.0' \
  -H 'Content-Type: application/json; ver=1.0' \
  -d '{
  "wholesalerId": 1,
  "beers": [
    {
      "beerId": 1,
      "quantity": 10
    }
  ]
}'
```

## Additional Endpoints
All the endpoints in this section use the GET method and do not require any request body. Each success response has a status code of 200, while each error has a status code of 404.

- **/api/breweries**: 
  -  **Description**: returns the list of all the breweries
  -  **Returns**: IEnumerable of BreweryDto
- **/api/breweries/{breweryId}**:
  -  **Description**: returns the brewery with id breweryId
  -  **Returns**: BreweryDto
- **/api/breweries/{breweryId}/beers/{beerId}**:
  -  **Description**: returns the beer with id beerId (if the brewery produces it with id breweryId).
  -  **Returns**: BeerDto



- **/api/sales**: 
  -  **Description**: returns the list of all the sales
  -  **Returns**: IEnumerable of GetSaleDto
- **/api/sales/{saleId}**: 
  -  **Description**: returns the sale with id saleId
  -  **Returns**: GetSaleDto
- **/api/sales/{saleId}/beer**: 
  -  **Description**: returns the beer associated with the sale with id saleId
  -  **Returns**: GetBeerFromSaleDto



- **/api/wholesalers**: 
  -  **Description**: returns the list of all the wholesalers
  -  **Returns**: IEnumerable of GetWholesalerDto
- **/api/wholesalers/{wholesalerId}**: 
  -  **Description**: returns the wholesaler with id wholesalerId
  -  **Returns**: GetWholesalerDto
- **/api/wholesalers/{wholesalerId}/beers**: 
  -  **Description**: returns the list of all the beers sold by the wholesaler with id wholesalerId
  -  **Returns**: IEnumerable of GetInventoryBeerDto
- **/api/wholesalers/{wholesalerId}/beers/{beerId}**: 
  -  **Description**: returns information about the beer with id beer id (if the wholesaler sells it) 
  -  **Returns**: GetInventoryBeerDto





