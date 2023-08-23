namespace BiblioTech.Domain.Entities
{
    public class AuditEntry
    {
        public int Id { get; set; }
        public DateTime AuditDate { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string AffectedColumn { get; set; }
    }
}
