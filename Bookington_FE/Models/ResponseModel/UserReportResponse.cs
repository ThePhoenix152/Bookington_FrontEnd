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
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string RefUser { get; set; } = null!;

        public string ReporterId { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
