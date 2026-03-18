using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases
{
    public class UpdateVisitStatusHandler : IUpdateVisitStatusHandler
{
    private readonly IVisitRepository _visitRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public UpdateVisitStatusHandler(
        IVisitRepository visitRepository,
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _visitRepository = visitRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Result<Visit>> Handle(UpdateVisitStatusCommand dto, Guid ManagerId)
    {
        var Manager = await _userRepository.GetByIdAsync(ManagerId);

        if (Manager == null)
            return Result<Visit>.Fail("Usuário não encontrado.");

        if (!Manager.Roles.Any(r => r.Name.Value == RoleType.Manager))
            return Result<Visit>.Fail("Apenas gestores podem aprovar ou rejeitar visitas.");

        var visit = await _visitRepository.GetByIdAsync(dto.VisitId);

        if (visit == null)
            return Result<Visit>.Fail("Visita não encontrada.");

        visit.UpdateStatus(dto.Status);

        if (visit.HasErrors)
            return Result<Visit>.Fail(visit.Errors);

        await _visitRepository.UpdateAsync(visit);

        await _emailService.SendAsync(
            visit.User.Email.Value!,
            "Atualização da sua visita",
            $"Sua visita foi marcada como '{visit.Status}'."
        );

        return Result<Visit>.Success(visit);
    }
}

}