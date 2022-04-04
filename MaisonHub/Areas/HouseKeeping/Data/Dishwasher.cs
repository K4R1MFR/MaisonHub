using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MaisonHub.Areas.HouseKeeping.Data
{
    public class Dishwasher
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        public IdentityUser? User { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public bool HasEmptied { get; set; }
        public bool HasCleanedFilter { get; set; }
    }
}