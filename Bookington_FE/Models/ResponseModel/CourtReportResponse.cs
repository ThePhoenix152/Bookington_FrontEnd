namespace Bookington_FE.Models.ResponseModel
{
    public class CourtReportResponse
    {
        public List<CourtReportModel> result { get; set; } = new List<CourtReportModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }

    public class CourtReportModel
    {
		public string Id { get; set; } = Guid.NewGuid().ToString();

		public string RefCourt { get; set; } = null!;

		public string ReporterId { get; set; } = null!;

		public string Content { get; set; } = null!;
	}
}
