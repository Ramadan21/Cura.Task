namespace Cura.Task.App.ViewModel
{
    public class GetMails
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string body { get; set; }
        public DateTime ReciveTime { get; set; }
        public bool HasAttachment { get; set; }
        public bool IsStared { get; set; }
        
    }
}
