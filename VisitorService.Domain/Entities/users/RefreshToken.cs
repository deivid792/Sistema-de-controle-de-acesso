namespace VisitorService.Domain.Entities
{

    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = null!;
        public DateTime ExpiryDate { get; set; }

        // Relacionamento
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
