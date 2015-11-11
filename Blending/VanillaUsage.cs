using System;
using System.ComponentModel.DataAnnotations;

namespace Blending
{
    public class VanillaUsage
    {
        public VanillaUsage(DateTimeOffset acquired)
        {
            Acquired = acquired;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Acquired { get; }
    }
}