using VisitorService.Application.DTOS;
using VisitorService.Application.Shared.results;

namespace VisitorService.Application.Interfaces
{
    public interface IRegisterVisitorHandler
    {
        Task<Result> Handle(RegisterVisitorCommand comand);
    }
}