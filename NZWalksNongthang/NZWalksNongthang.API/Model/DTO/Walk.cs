﻿using NZWalksNongthang.API.Model.Domain;

namespace NZWalksNongthang.API.Model.DTO
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        //Navigation Property
        public Region Region { get; set; }
        public WalkDifficulty walkDifficulty { get; set; }
    }
}