using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagmentBLL.ViewModels.PlanViewModels
{
    public class UpdatePlanViewModel
    {
        [Required(ErrorMessage = "Plan Name is Required")]
        [StringLength(50,  ErrorMessage = "Plan Name must be less 50 charachters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = " Description is Required")]
        [StringLength(50,MinimumLength =5, ErrorMessage = "Description must be Between 5 and 200 charachters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price Name is Required")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "DurationDays Name is Required")]
        [Range(1, 365, ErrorMessage = "Duration Days must be between 1 and 365")]
        public int DurationDays { get; set; }
    }
}
