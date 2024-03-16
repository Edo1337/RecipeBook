namespace RecipeBook.Domain.Interfaces
{
    public interface IAuditable
    {
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
