﻿using KWalks.API.Models.Domain;

namespace KWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImgUrl { get; set; }

        // Walk Must have difficulty level and Region where it took place
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        // Navigation Properties
        //public Difficulty Difficulty { get; set; }
        //public Region Region { get; set; }
    }
}
