namespace BiblioTech.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Parameters { get; set; }
        public DateTime CallDateTime { get; set; }
        public string Response { get; set; }
    }
}
