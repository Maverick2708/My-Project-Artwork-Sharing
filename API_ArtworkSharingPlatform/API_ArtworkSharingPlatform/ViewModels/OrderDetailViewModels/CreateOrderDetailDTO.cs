namespace API_ArtworkSharingPlatform.ViewModels.OrderDetailViewModels
{
    public class CreateOrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public DateTime? DateOrder { get; set; }
        public int? BillOrderId { get; set; }
        public double? PriceOrder { get; set; }
        public int? ArtworkPId { get; set; }
        public int? Quanity { get; set; }
    }
}
