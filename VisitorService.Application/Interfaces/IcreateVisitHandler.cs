using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.Interfaces
{
    public interface IcreateVisitHandler
    {
        Task<Result<Visit>> Handler(CreateVisitDto createVisitDto);
    }
}