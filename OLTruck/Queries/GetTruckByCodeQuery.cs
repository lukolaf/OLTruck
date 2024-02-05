using MediatR;
using OLTruck.Shared.TruckDto;

namespace OLTruck.Queries
{
    public class GetTruckByCodeQuery : IRequest<TruckDto>
    {
        public string Code { get; set; }
        public GetTruckByCodeQuery(string code)
        {
            Code = code;
        }
    }
}
