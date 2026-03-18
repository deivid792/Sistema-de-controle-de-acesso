namespace VisitorService.Application.DTOS
{
    public class UpdateVisitStatusCommand
    {
        public Guid VisitId { get; set; }
        public string Status { get; set; } = "";
    }
}