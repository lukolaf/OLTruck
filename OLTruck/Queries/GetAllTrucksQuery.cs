﻿using MediatR;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Queries
{
    public class GetAllTrucksQuery : IRequest<IEnumerable<TruckDto>>
    {
        public string? Filter { get; set; }
        public string? SortBy { get; set; }
    }
}
