using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoreAuctions.Models
{
    public class Auction : IAuction<AuctionItem>
    {
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Auction Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public IEnumerable<AuctionItem> Items { get; set; }
    }
}
