﻿namespace Bookington_FE.Models.ResponseModel
{
    public class BookingResponse
    {
        public List<BookingModel> result { get; set; } = new List<BookingModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }


    public class BookingModel
    {
        public string Id { get; set; }

        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string daysInSchedule { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

    }
}
