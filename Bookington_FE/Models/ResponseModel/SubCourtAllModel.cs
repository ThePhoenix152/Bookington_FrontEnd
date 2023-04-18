namespace Bookington_FE.Models.ResponseModel
{
	public class SubCourtAllModel
	{
		public List<SubCourtDetails> SubCourtDetails { get; set; } = new List<SubCourtDetails>();

	}
	public class SubCourtDetails
	{
		public SubcourtModel subcourtModel { get; set; } = new SubcourtModel();
        public Dictionary<string, List<SlotModel>> GroupSlotByTime { get; set; } = new Dictionary<string, List<SlotModel>>();
    }
}
