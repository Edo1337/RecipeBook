namespace RecipeBook.Domain.Interfaces.Databases
{
    public interface IStateSaveChanges
    {
        Task<int> SaveChangesAsync();
    }
}
