namespace VisitorService.Application.DTOS
{
    public class LoginCommand
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}
}