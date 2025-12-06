using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;

namespace VisitorService.Application.Interfaces
{
    public interface IloginHandler
    {
        Task<Result<AuthResultDto>> Handle(LoginCommand command);

    }
}