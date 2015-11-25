using System;
using System.ComponentModel.DataAnnotations;

namespace Facility.Web.Controllers
{
    public class ChocolateProduction
    {
        internal ChocolateProduction()
        {
        }

        public ChocolateProduction(int lotNumber)
        {
            Acquired = DateTime.UtcNow;
            Id = Guid.NewGuid();
            LotNumber = lotNumber;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime Acquired { get; set; }

        [Required]
        public int LotNumber { get; set; }
    }
}