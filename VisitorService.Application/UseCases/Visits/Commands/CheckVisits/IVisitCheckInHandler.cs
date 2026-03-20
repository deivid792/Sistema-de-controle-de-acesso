using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.UseCases.Visits.Commands
{
    public interface IVisitCheckInHandler
    {
        Task<Result<Visit>> Handle(VisitCheckCommand dto, Guid securityId);
    }
}