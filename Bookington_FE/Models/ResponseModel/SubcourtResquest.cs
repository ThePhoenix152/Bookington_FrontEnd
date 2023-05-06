namespace Bookington_FE.Models.ResponseModel
{
    public class SubcourtResquest
    {
        public string parentCourtId { get; set; } 
        public string name { get; set; } 
        public string courtTypeId { get; set; } 
        public string isActive { get; set; } 
        public string isDeleted { get; set; }
        
    }
}
