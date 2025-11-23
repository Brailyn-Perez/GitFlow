

using ShopApp.Domain.Models.Categoria.BaseModel;

namespace ShopApp.Domain.Models.Categoria
{
    public record CategoriaUpdateModel : CategoriaModel
    {
        public int modify_user { get; set; }
    }
}
