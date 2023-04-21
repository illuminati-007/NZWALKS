using System.ComponentModel.DataAnnotations;

namespace FP_NZWALKS.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(33)]
        public string Name { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        [Range(0, 50)]
        public double LengthKm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
