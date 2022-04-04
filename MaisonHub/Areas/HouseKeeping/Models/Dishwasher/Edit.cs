namespace MaisonHub.Areas.HouseKeeping.Models.Dishwasher
{
    public class Edit
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool HasEmptied { get; set; }
        public bool HasCleanedFilter { get; set; }
    }
}
