using System.ComponentModel.DataAnnotations;

namespace MaisonHub.Areas.HouseKeeping.Models.Dishwasher
{
    public class Create
    {
        public string UserId { get; set; } = null!;
        public DateTime Date { get; set; }
        public bool HasEmptied { get; set; }
        public bool HasCleanedFilter { get; set; }
    }
}