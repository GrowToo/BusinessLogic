using DTO;
using System.Collections.Generic;

namespace DAL.Interface
{
    public interface IProductDal
    {
        void AddProduct(ProductDto product);
        void Update(ProductDto product);
        void Delete(ProductDto product);
        ProductDto GetById(int id);
        IEnumerable<ProductDto> GetAll();
    }
}
