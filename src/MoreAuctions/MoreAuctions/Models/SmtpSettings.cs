namespace MoreAuctions.Models
{
    public class SmtpSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string User { get; set; }
        public string Pwd { get; set; }
        public string SourceEmailAddress { get; set; }
        public string DestinationEmailAddress { get; set; }
    }
}