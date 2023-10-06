using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.PromotionServices.Dtos
{
    public class CreatePromotionDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PromotionCode { get; set; }
        [Required]
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}