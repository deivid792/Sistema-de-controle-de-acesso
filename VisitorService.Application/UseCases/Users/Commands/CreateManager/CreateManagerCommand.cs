namespace VisitorService.Application.UseCases.Users.Commands.CreateManager;

public class CreateManagerCommand
{
    public string name { get; set; } = default!;
    public string Email { get; set;} = default!;
    public string Password { get; set; } = default!;
    public string? phone { get; set; } = default!;
}