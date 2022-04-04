using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MaisonHub.Areas.HouseKeeping.Data
{
    public class Garbage
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
        [Required]
        public DateTime Date { get; set; }
        public bool HasPutNewBag { get; set; }
        public bool HasTakenDownBin { get; set; }
        public bool HasTakenDownRecycling { get; set; }
    }
}