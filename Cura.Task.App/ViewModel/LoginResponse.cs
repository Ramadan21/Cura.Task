namespace Cura.Task.App.ViewModel
{
    public class LoginResponse : MainResponse
    {
        public TokenData Result { get; set; }
    }

    public class TokenData
    {
        public string data { get; set; }
    }
}
