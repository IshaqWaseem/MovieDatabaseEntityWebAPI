namespace MovieDatabaseEntityWebAPI.Exceptions
{
    public class FranchiseNotFoundException : EntityNotFoundException
    {
        public FranchiseNotFoundException(): base("Franchise not found with that Id"){ }
    }
}
