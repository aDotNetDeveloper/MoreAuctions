namespace MoreAuctions.Models
{
    public interface IAuctionItem
    {
        int ID { get; set; }
        string Description { get; set; }
        string Title { get; set; }
        float StartPrice { get; set; }
    }
}