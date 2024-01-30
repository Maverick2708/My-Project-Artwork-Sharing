namespace API_ArtworkSharingPlatform.ViewModels.OrderViewModels
{
    public class CreateOrderDTO
    {
        public int BillOrderId { get; set; }
        public int? UserId { get; set; }
        public double? TotalBill { get; set; }
    }
}
