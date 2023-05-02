namespace Bookington_FE.Models.RequestModel
{
    public class AuthLoginRequest
    {
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public class UpdateAccountRequest
    {
        public string fullName { get; set; } = string.Empty;
        public string dateOfBirth { get; set; } = string.Empty;
    }
    public class UpdateCourtRequest
    {
		public string OwnerId { get; set; } = string.Empty;

		public string DistrictId { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public TimeSpan OpenAt { get; set; } = TimeSpan.Zero;

		public TimeSpan CloseAt { get; set; } = TimeSpan.Zero;
	}

    public class UpdateRoleRequest
    {
        public string RoleId { get; set; }= string.Empty;
    }
}
