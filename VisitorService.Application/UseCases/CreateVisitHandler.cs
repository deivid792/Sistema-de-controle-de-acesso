using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases
{
    public class CreateVisitHandler : IcreateVisitHandler
    {
        private readonly IUserRepository _userRepository;

        private readonly IVisitRepository _visitRepository;

        public CreateVisitHandler(IUserRepository userRepository, IVisitRepository visitRepository)
        {
            _userRepository = userRepository;

            _visitRepository = visitRepository;
        }

        public async Task<Result<Visit>> Handler(CreateVisitDto dto)
        {
            User? user = await _userRepository.GetByIdAsync(dto.UserId);

            if (user == null)
                return Result<Visit>.Fail("O usuário não existe");

            var Isthereaconflict = await _visitRepository.ExistsVisitInDateAndTime(dto.Date, dto.Time);
            if (Isthereaconflict)
                return Result<Visit>.Fail("Esse horário já está reservado.");

            user.AddVisit(dto.Date, dto.Time, dto.Reason, dto.Category, "Pendente");

            if (user.HasErrors)
                return Result<Visit>.Fail(user.Errors);

            var visit = user.Visits.Last();

            await _userRepository.UpdateAsync(user);

            return Result<Visit>.Success(visit);
        }
    }

}