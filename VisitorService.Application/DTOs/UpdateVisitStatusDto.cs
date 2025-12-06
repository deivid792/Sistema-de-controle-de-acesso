namespace VisitorService.Application.DTOS
{
    public class UpdateVisitStatusDto
    {
        public Guid VisitId { get; set; }
        public string Status { get; set; } = "";
    }
}