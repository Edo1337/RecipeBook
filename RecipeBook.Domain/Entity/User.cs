using RecipeBook.Domain.Interfaces;

namespace RecipeBook
{
    public class User : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public List<Recipe> Recipes { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
