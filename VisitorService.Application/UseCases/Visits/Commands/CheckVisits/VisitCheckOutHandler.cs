using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases.Visits.Commands
{
    public class VisitCheckOutHandler : IVisitCheckOutHandler
{
    private readonly IVisitRepository _visitRepo;
    private readonly IUserRepository _userRepository;

    public VisitCheckOutHandler(IVisitRepository visitRepo, IUserRepository userRepository)
    {
        _visitRepo = visitRepo;
        _userRepository = userRepository;
    }

    public async Task<Result<Visit>> Handle(VisitCheckCommand dto, Guid securityId)
    {
        var isSecurity = await _userRepository.IsUserInRoleAsync(securityId, RoleType.Manager);

        if (!isSecurity)
            return Result<Visit>.Fail("Apenas Seguranças podem aprovar ou rejeitar visitas.");
        var visit = await _visitRepo.GetByIdAsync(dto.VisitId);

        if (visit == null)
            return Result<Visit>.Fail("Visita não encontrada.");

        visit.CheckOutVisit();

        if (visit.HasErrors)
            return Result<Visit>.Fail(visit.Errors);

        await _visitRepo.UpdateAsync(visit);

        return Result<Visit>.Success(visit);
    }
}

}