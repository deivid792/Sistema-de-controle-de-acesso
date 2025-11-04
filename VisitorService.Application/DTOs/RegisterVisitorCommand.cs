namespace VisitorService.Application.DTOS
{
    public class RegisterVisitorCommand
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Company { get; set; }
    public string? Cnpj { get; set; }
}
}