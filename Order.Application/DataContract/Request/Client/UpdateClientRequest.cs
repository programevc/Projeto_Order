namespace Order.Application.DataContract.Request.Client
{
    public sealed class UpdateClientRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
    }
}
