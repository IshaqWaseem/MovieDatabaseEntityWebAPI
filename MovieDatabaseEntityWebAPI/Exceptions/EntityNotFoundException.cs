namespace MovieDatabaseEntityWebAPI.Exceptions
{
    /// <summary>
    /// Parent "not found" exception for entities. Lets us have one catch in 
    /// controller methods where there are multiple possbilities of missing entities.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string? message) : base(message)
        {
        }
    }

}
