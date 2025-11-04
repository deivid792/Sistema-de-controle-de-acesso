using VisitorService.Domain.Shared.results;
using VisitorService.Domain.ValueObject;

namespace VisitorService.Application.Interfaces
{
    public interface IPasswordService
{
    // Compara senha em texto com o hash (Password VO)
    bool Verify(Password storedPassword, string passwordToCheck);

    // Cria Password VO a partir de senha em texto (hash dentro do VO ou retorna VO pronto)
    Result<Password> Hash(string plainPassword);
}
}