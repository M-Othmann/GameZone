﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameFromViewModel
    {
        public string Name { get; set; } = string.Empty;


        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Display(Name = "Devices")]
        public List<int> SelectedDevices { get; set; } = new List<int>();
        public IEnumerable<SelectListItem> Devices {get; set;} = Enumerable.Empty<SelectListItem>();


        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public IFormFile Cover { get; set; } = default!;
    }
}