using System.ComponentModel.DataAnnotations;

namespace MoreAuctions.Models
{
    public class AuctionItem : IAuctionItem
    {
        public int ID { get; set; }
        public int AuctionID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Auction Starting Price")]
        [Range(minimum: 0, maximum: int.MaxValue)]
        public float StartPrice { get; set; }
    }
}
