﻿namespace MovieDatabaseEntityWebAPI.Models.DTO.Franchise
{
    public class FranchiseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public int Franchise { get; set; }
        public List<int> Movies { get; set; } = null!;

    }
}
