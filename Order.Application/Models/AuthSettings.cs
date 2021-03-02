namespace Order.Application.Models
{
    public class AuthSettings
    {
        public string Secret { get; set; }
        public int ExpireIn { get; set; }
    }
}
