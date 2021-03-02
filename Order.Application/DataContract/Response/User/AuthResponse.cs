namespace Order.Application.DataContract.Response.User
{
    public sealed class AuthResponse
    {
        public string Token { get; set; }
        public string Type { get; set; }
        public int ExpireIn { get; set; }
    }
}
