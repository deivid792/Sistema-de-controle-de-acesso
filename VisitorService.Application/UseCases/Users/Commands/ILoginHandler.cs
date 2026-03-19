using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;

namespace VisitorService.Application.UseCases.Users.Commands
{
    public interface IloginHandler
    {
        Task<Result<AuthResultDto>> Handle(LoginCommand command);

    }
}