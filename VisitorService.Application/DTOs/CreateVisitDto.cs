namespace VisitorService.Application.DTOS
{
    public class CreateVisitDto
    {
        public DateOnly Date { get; init; }
        public TimeOnly Time { get; init; }
        public string Reason { get; init; } = default!;
        public string Category { get; init; } = default!;
    }
}