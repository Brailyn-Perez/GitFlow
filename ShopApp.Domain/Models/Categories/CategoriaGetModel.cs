

using ShopApp.Domain.Models.Categoria.BaseModel;

namespace ShopApp.Domain.Models.Categoria
{
    public record CategoriaGetModel : CategoriaModel
    {
        public DateTime creation_date { get; set; }
        public int creation_user { get; set; }
    }
}
 