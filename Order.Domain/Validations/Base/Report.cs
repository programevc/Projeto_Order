namespace Order.Domain.Validations.Base
{
    public class Report
    {
        public Report()
        {

        }

        public Report(string message)
        {
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
        public static Report Create(string message) => new Report(message);
    }
}
