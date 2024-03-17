using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Domain.DTO
{
    public record RecipeDto(long Id, string Name, string Description, string DateCreated);
}
