using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;

namespace VisitorService.Application.Interfaces
{
    public interface IGetTodayApprovedVisitsHandler
    {
        Task<Result<IEnumerable<Visit>>> Handle();
    }
}
