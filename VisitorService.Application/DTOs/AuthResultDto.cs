namespace VisitorService.Application.DTOS
{
    public class AuthResultDto
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
}
}