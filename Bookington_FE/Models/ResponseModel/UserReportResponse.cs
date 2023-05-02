namespace Bookington_FE.Models.ResponseModel
{
    public class UserReportResponse
    {
        public List<UserReportModel> result { get; set; } = new List<UserReportModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }

    public class UserReportModel
    {
        public string Id { get; set; }

        public string RefUser { get; set; }

        public string RefUserName { get; set; }

        public string ReporterId { get; set; }

        public string ReporterCourtName { get; set; }

        public string Content { get; set; }

        public bool IsResponded { get; set; }

        public bool IsBan { get; set; }
    }
}
