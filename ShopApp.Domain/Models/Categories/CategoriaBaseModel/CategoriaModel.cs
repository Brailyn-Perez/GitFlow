namespace ShopApp.Domain.Models.Categoria.BaseModel
{
    public abstract record CategoriaModel
    {
        public int categoryid { get; set; }
        public string categoryname { get; set; }
        public string description { get; set; }
    }
}
 