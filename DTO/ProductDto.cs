namespace DTO
{
    public class ProductDto
    {
        public int Id { get; set; }            // Унікальний ідентифікатор продукту
        public string Name { get; set; }       // Назва продукту
        public decimal Price { get; set; }         // Ціна продукту
        public int CategoryId { get; set; }    // Ідентифікатор категорії
        public int Quantity { get; set; }      // Кількість продукту
        //public DateTime DateAdded { get; set; } // Дата додавання продукту
        public DateTime CreatedDate { get; set; }
    }
}
