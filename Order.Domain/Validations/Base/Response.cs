using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Validations.Base
{
    public class Response
    {
        public Response()
        {
            Report = new List<Report>();
        }

        public Response(List<Report> reports)
        {
            Report = reports;
        }

        public Response(Report report) : this(new List<Report>() { report })
        {

        }

        public List<Report> Report { get; }


        public static Response<T> OK<T>(T data) => new Response<T>(data);
        public static Response OK() => new Response();
        public static Response Unprocessable(List<Report> reports) => new Response(reports);
        public static Response Unprocessable(Report report) => new Response(report);
    }

    public class Response<T> : Response
    {
        public Response()
        {

        }
        public Response(T data, List<Report> reports = null) : base(reports)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
