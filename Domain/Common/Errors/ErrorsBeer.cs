﻿using Domain.Common.Errors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public sealed record BreweryBeerNotFound(int beerId, int breweryId) : NotFoundError, IError
    {
        public new string Message => $"Beer with specified id does not exist for the specified brewery. [beerId: {beerId}, breweryId: {breweryId}]";

        public new string Code => "BreweryBeer.NotFound";
    }

    public sealed record BeerNotFound(int beerId) : NotFoundError, IError
    {
        public new string Message => $"Beer with specified id does not exist (or has been canceled). [beerId: {beerId}]";

        public new string Code => "Beer.NotFound";
    }

    public sealed record WholesalerBeerNotFound(int beerId, int wholesalerId) : NotFoundError, IError
    {
        public new string Message => $"Beer with specified id does not exist for the specified wholesaler. [beerId: {beerId}, wholesalerId: {wholesalerId}]";

        public new string Code => "WholesalerBeer.NotFound";
    }


    public sealed record BreweryBeerConflict(string name, int breweryId) : ConflictError, IError
    {
        public new string Message => $"The Specified name is assigned to an existing beer produced by the brewery. [name: {name}, breweryId: {breweryId}]";

        public new string Code => "Beers.Conflict";
    }

    public sealed record BadBeerId(int beerId) : BadRequestError, IError
    {
        public new string Message => $"Beer with specified id does not exist. [beerId: {beerId}]";

        public new string Code => "Beer.BadRequest";
    }

    public sealed record BeerInsertionInternalError() : InternalError, IError
    {
        public new string Message => "Due to an internal error, it was impossible to add a new beer to the database";

        public new string Code => "Beer.InternalError";
    }

    public sealed record BeerNotSoldByWholesaler(int wholesalerId, int beerId) : NotFoundError, IError
    {
        public new string Message => $"Beer with specified id is not sold by the specified brewery. [beerId: {beerId}, wholesalerId: {wholesalerId}]";

        public new string Code => "WholesalerBeer.BadRequest";
    }

    public sealed record BeerQuantityUpdateInternalError() : InternalError, IError
    {
        public new string Message => "Due to an internal error, it was impossible to update the beer quantity";

        public new string Code => "InventoryBeer.InternalError";
    }
}

