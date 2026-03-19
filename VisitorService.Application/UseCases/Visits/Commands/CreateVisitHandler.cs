using isitorService.Application.UseCases.Visits.Commands;
using VisitorService.Application.DTOS;
using VisitorService.Application.Interfaces;
using VisitorService.Application.Shared.results;
using VisitorService.Domain.Entities;
using VisitorService.Domain.Interfaces;

namespace VisitorService.Application.UseCases.Visits.Commands
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

        public async Task<Result<RegisterVisitResponseDto>> Handler(CreateVisitCommand dto, Guid UserId)
        {
            User? user = await _userRepository.GetByIdAsync(UserId);

            if (user == null)
                return Result<RegisterVisitResponseDto>.Fail("O usuário não existe");

            var Isthereaconflict = await _visitRepository.ExistsVisitInDateAndTime(dto.Date, dto.Time);
            if (Isthereaconflict)
                return Result<RegisterVisitResponseDto>.Fail("Esse horário já está reservado.");

            user.ScheduleVisit(dto.Date, dto.Time, dto.Reason, dto.Category);

            if (user.HasErrors)
                return Result<RegisterVisitResponseDto>.Fail(user.Errors);

            var visit = user.Visits.Last();

            await _userRepository.UpdateAsync(user);

            var response = new RegisterVisitResponseDto
            {
                UserId = visit.UserId,
                Date = visit.Date,
                Time = visit.Time,
                Reason = visit.Reason,
                Category = visit.Category,
                Status = visit.Status
            };

            return Result<RegisterVisitResponseDto>.Success(response);
        }
    }

}