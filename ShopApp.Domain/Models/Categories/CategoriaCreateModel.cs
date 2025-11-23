
using ShopApp.Domain.Models.Categoria.BaseModel;

namespace ShopApp.Domain.Models.Categoria
{
    public record CategoriaCreateModel : CategoriaModel
    {
        public int creation_user { get; set; }
    }
}
