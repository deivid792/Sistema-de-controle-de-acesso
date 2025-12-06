using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.Interfaces
{
    public interface IVisitCheckOutHandler
    {
        Task<Result<Visit>> Handle(VisitCheckDto dto);
    }
}