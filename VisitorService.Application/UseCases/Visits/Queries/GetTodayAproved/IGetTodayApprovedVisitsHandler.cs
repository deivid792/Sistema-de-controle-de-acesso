using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.UseCases.Visits.Queries
{
    public interface IGetTodayApprovedVisitsHandler
    {
        Task<Result<IEnumerable<Visit>>> Handle();
    }
}
