namespace RecipeBook.Domain.Dto.Recipe
{
    public record CreateRecipeDto(string Name, string Description, long UserId);
}
