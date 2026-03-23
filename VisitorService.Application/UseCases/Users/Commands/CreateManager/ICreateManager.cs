using VisitorService.Application.Shared.results;

namespace VisitorService.Application.UseCases.Users.Commands.CreateManager;

public interface ICreateManagerHandler
{
    Task<Result> Handle(CreateManagerCommand user, Guid IdManageer);
}