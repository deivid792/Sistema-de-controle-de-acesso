using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;

namespace VisitorService.Application.UseCases.Users.Commands
{
    public interface IRegisterVisitorHandler
    {
        Task<Result<AuthResultDto>> Handle(RegisterVisitorCommand comand);
    }
}