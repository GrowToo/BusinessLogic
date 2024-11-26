using DAL.Concrete;
using DTO;
using System.Collections.Generic;

namespace BusinessLogic.Interface
{
    public interface IProductManager
    {
        void AddProduct(ProductDto product);
        void UpdateProduct(ProductDto product);
        void DeleteProduct(int productId);
        ProductDto GetProductById(int productId);
        IEnumerable<ProductDto> GetAllProducts();
        IEnumerable<ProductDto> SearchProducts(string keyword);
        IEnumerable<ProductDto> SortProducts(string sortBy);
    }
}
