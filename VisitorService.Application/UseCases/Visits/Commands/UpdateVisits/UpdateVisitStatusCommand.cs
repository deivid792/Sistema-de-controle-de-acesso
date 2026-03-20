namespace VisitorService.Application.UseCases.Visits.Commands
{
    public class UpdateVisitStatusCommand
    {
        public Guid VisitId { get; set; }
        public string Status { get; set; } = "";
    }
}