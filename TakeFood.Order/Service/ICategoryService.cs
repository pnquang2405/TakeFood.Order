
using Order.ViewModel.Dtos.Category;

namespace Order.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(String id);
        Task CreateCategory(CategoryDto category);
        Task UpdateCategory(string id, CategoryDto category);
        Task DeleteCategory(String id);
    }
}
