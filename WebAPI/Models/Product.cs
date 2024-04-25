﻿using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Product
    {
        [Key]
        public required string Id { get; set; }

        [Required(ErrorMessage = "Name cannot be empty")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Price cannot be empty")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Currency cannot be empty")]
        public string Currency { get; set; } = null!;

        [Required(ErrorMessage = "RebateQuantity cannot be empty")]
        public int RebateQuantity { get; set; }

        [Required(ErrorMessage = "RebatePercent cannot be empty")]

        public int RebatePercent { get; set; }

        public string? UpsellProductId { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
