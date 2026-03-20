using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.UseCases.Visits.Commands
{
    public interface IUpdateVisitStatusHandler
    {
        Task<Result<Visit>> Handle(UpdateVisitStatusCommand dto, Guid gestorId);
    };
}