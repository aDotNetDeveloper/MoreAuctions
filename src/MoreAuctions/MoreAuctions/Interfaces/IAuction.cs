using System;
using System.Collections.Generic;

namespace MoreAuctions.Models
{
    public interface IAuction<T> where T : IAuctionItem
    {
        int ID { get; set; }
        string Description { get; set; }
        DateTime Date { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}