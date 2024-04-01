﻿namespace RecipeBook
{
    public class Recipe : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public User User { get; set; }
        public long UserId { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
