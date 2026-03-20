using isitorService.Application.UseCases.Visits.Commands;
using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;


namespace VisitorService.Application.UseCases.Visits.Commands
{
    public interface IcreateVisitHandler
    {
        Task<Result<RegisterVisitResponseDto>> Handler(CreateVisitCommand createVisitDto, Guid UserId);
    }
}