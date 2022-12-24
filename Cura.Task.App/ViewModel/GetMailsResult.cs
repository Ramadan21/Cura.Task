namespace Cura.Task.App.ViewModel
{
    public class GetMailsResult : MainResponse
    {
        public List<GetMails> Result { get; set; }
    }
    public class MailList
    {
        public List<GetMails> Data { get; set; }
    }
}
