using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.Interfaces
{
    public interface IUpdateVisitStatusHandler
    {
        Task<Result<Visit>> Handle(UpdateVisitStatusDto dto, Guid gestorId);
    };
}