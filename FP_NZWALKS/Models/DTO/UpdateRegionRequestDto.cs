using System.ComponentModel.DataAnnotations;

namespace FP_NZWALKS.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [MinLength(2, ErrorMessage = "too short!")]
        [MaxLength(3, ErrorMessage = "too long!")]
        public string Code { get; set; }
        [Required]
        [MaxLength(33, ErrorMessage = "Maximum 33 char")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
