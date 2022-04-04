namespace MaisonHub.Areas.HouseKeeping.Models.Garbage
{
    public class Edit
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool HasPutNewBag { get; set; }
        public bool HasTakenDownBin { get; set; }
        public bool HasTakenDownRecycling { get; set; }
    }
}