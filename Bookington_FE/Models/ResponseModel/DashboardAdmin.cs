namespace Bookington_FE.Models.ResponseModel
{
    public class DashboardAdmin
    {
        public List<BookingModel> result { get; set; } = new List<BookingModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }


    public class DashboardAModel
    {
        public int totalOrders { get; set; }

        public int paidOrders { get; set; }
        public int cancelOrder { get; set; }
        public int refundedOrders { get; set; }
        public int totalSales { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

    }
}
