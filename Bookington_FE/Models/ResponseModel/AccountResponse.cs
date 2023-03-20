﻿namespace Bookington_FE.Models.ResponseModel
{
    public class AccountResponse
    {
        public List<AccountModel> result { get; set; } = new List<AccountModel>();
        public int statusCode { get; set; }
        public bool isError { get; set; } = false;
        public string message { get; set; } = string.Empty;
    }
    public class AccountModel
    {
		public string Id { get; set; }

		public string RoleName { get; set; }

		public string Phone { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string FullName { get; set; }

		public DateTime CreateAt { get; set; }

		public bool IsActive { get; set; }

		public bool IsDeleted { get; set; }
	}
}
