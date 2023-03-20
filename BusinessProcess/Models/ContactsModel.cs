namespace BusinessProcess.Models
{
    public class ContactsModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Initials { get; set; }
        public string Firstname { get; set; }
        public string Surename { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string DoB { get; set; }
        public string Status { get; set; }
    }
}
