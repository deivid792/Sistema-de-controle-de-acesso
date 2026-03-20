using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.UseCases.Visits.Commands
{
    public interface IVisitCheckOutHandler
    {
        Task<Result<Visit>> Handle(VisitCheckCommand dto, Guid securityId);
    }
}