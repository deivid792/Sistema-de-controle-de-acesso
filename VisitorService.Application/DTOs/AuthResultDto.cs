namespace VisitorService.Application.DTOS
{
    public class AuthResultDto
{
    public string AccessToken { get; set; } = null!;
    public string RefeshToken { get; set; } = null!;
    public DateTime ExpiresAccessTokenIn { get; set; }
    public UserResponseDto User{ get; set; } = null!;
}
}