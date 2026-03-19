using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases.Visits.Queries
{
    public class GetAllVisitsHandler : IGetAllVisitsHandler
{
    private readonly IVisitRepository _visitRepository;

    public GetAllVisitsHandler(IVisitRepository visitRepository)
    {
        _visitRepository = visitRepository;
    }

    public async Task<Result<IEnumerable<Visit>>> Handle()
    {
        var visits = await _visitRepository.GetAllAsync();
        return Result<IEnumerable<Visit>>.Success(visits);
    }
}
}