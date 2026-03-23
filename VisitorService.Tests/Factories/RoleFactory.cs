using VisitorService.Domain.Entities;
using VisitorService.Domain.Enums;
using VisitorService.Domain.ValueObjects;

namespace VisitorService.Tests.Factories;

public static class RoleFactory
{
    public static Role CreateCustom(RoleType type, string description = "Descrição de teste")
    {
        return Role.Create(
            RoleName.Create(type),
            description
        );
    }
    public static Role Manager() => 
        CreateCustom(RoleType.Manager, "Acesso total ao sistema e gestão de usuários.");

    public static Role Visitor() => 
        CreateCustom(RoleType.Visitor, "Perfil com permissões limitadas de visualização.");

    public static Role Security() => 
        CreateCustom(RoleType.Security, "Responsável pelo controle de fluxo e monitoramento.");
}