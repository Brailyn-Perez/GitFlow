

using ShopApp.Domain.Models.Categoria.BaseModel;

namespace ShopApp.Domain.Models.Categoria
{
    public record CategoriaDeleteModel : CategoriaModel
    {
        public int delete_user { get; set; }
    }
}
 