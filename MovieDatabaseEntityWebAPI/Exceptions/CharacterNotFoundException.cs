namespace MovieDatabaseEntityWebAPI.Exceptions
{
    public class CharacterNotFoundException : EntityNotFoundException
    {
        public CharacterNotFoundException() : base("Character not found with that Id") { }
    }
}
