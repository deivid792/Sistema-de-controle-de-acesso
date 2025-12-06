using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases
{
    public class GetTodayApprovedVisitsHandler : IGetTodayApprovedVisitsHandler
{
    private readonly IVisitRepository _visitRepo;

    public GetTodayApprovedVisitsHandler(IVisitRepository visitRepo)
    {
        _visitRepo = visitRepo;
    }

    public async Task<Result<IEnumerable<Visit>>> Handle()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        var visits = await _visitRepo.GetApprovedByDateAsync(today);

        return Result<IEnumerable<Visit>>.Success(visits);
    }
}

}