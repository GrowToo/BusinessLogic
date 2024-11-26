using System.Collections.Generic;
using DAL.Concrete;
using DTO;

namespace DAL.Interface
{
    public interface ICategoryDal
    {
        List<CategoryDto> GetCategories();  // Отримати всі категорії
        void AddCategory(CategoryDto categoryDto);  // Додати категорію
    }
}
