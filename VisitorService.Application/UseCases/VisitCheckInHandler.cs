using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases
{
    public class VisitCheckInHandler : IVisitCheckInHandler
{
    private readonly IVisitRepository _visitRepo;

    public VisitCheckInHandler(IVisitRepository visitRepo)
    {
        _visitRepo = visitRepo;
    }

    public async Task<Result<Visit>> Handle(VisitCheckDto dto)
    {
        var visit = await _visitRepo.GetByIdAsync(dto.VisitId);

        if (visit == null)
            return Result<Visit>.Fail("Visita não encontrada.");

        visit.CheckInVisit();

        if (visit.HasErrors)
            return Result<Visit>.Fail(visit.Notification);

        await _visitRepo.UpdateAsync(visit);

        return Result<Visit>.Success(visit);
    }
}

}