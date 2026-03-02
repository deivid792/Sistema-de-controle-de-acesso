namespace VisitorService.Application.DTOS;

public class RegisterVisitResponseDto
{
        public Guid UserId { get;  set; }
        public DateOnly Date { get;  set; }
        public TimeOnly Time { get;  set; }
        public string? Reason { get;  set; } = default!;
        public string? Category { get;  set; } = default!;
        public string? Status { get;  set; } = default!;
}