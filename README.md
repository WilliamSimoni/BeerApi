# BeerApi

## Architecture
The API uses a layered architecture with the following levels:
- Domain layer with entities definition, repository interfaces definition, and exceptions definition.
- Infrastructure layer with Repository implementation
- Service layer with services definition, DTOs definition, and mapping between DTOs and entities.
- Controller layer with controller implementation.
- API layer, which wires everything together.
