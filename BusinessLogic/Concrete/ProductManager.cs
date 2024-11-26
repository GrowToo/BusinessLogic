using BusinessLogic.Interface;
using DAL.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Concrete
{
    public class ProductManager : IProductManager
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void AddProduct(ProductDto product)
        {
            // Перевірка на валідність даних
            if (product == null) throw new ArgumentNullException(nameof(product));

            // Додаємо продукт до бази даних через DAL
            _productDal.AddProduct(product);
        }

        public void UpdateProduct(ProductDto product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));

            var existingProduct = _productDal.GetById(product.Id);
            if (existingProduct == null) throw new InvalidOperationException("Product not found");

            // Оновлюємо продукт
            existingProduct.Name = product.Name;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Price = product.Price;
            existingProduct.Quantity = product.Quantity;
            _productDal.Update(existingProduct);
        }

        public void DeleteProduct(int productId)
        {
            var product = _productDal.GetById(productId);
            if (product == null) throw new InvalidOperationException("Product not found");

            _productDal.Delete(product);
        }

        public ProductDto GetProductById(int productId)
        {
            var product = _productDal.GetById(productId);
            if (product == null) throw new InvalidOperationException("Product not found");

            return product;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            return _productDal.GetAll();
        }

        public IEnumerable<ProductDto> SearchProducts(string keyword)
        {
            var products = _productDal.GetAll();
            return products.Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<ProductDto> SortProducts(string sortBy)
        {
            var products = _productDal.GetAll();
            return sortBy.ToLower() switch
            {
                "price" => products.OrderBy(p => p.Price),
                "name" => products.OrderBy(p => p.Name),
                _ => products
            };
        }
    }
}
