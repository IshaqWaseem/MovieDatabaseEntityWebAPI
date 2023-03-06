namespace MovieDatabaseEntityWebAPI.Models.DTO.Franchise
{
    public class FranchisePutDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
