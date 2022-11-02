using Order.Model.Entities.Category;
using Order.Model.Repository;
using Order.ViewModel.Dtos.Category;

namespace Order.Service.Implement
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoRepository<Category> cateRepository;

        public CategoryService(IMongoRepository<Category> cateRepository)
        {
            this.cateRepository = cateRepository;
        }

        public async Task CreateCategory(CategoryDto category)
        {
            var categories = await cateRepository.FindAsync(x => x.Name == category.Name);
            if (categories.Count != 0) return;

            Category categoryNew = new Category()
            {
                Name = category.Name,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await cateRepository.InsertAsync(categoryNew);
        }

        public async Task DeleteCategory(String id)
        {
            await cateRepository.RemoveAsync(id);
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await cateRepository.GetAllAsync();

            List<CategoryDto> categoryList = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoryList.Add(new CategoryDto() { Name = category.Name, });
            }

            return categoryList; 
        }

        public async Task<CategoryDto?> GetCategoryById(string id)
        {
            Category category = await cateRepository.FindByIdAsync(id);
            if(category != null)
            {
                CategoryDto categoryDto = new CategoryDto()
                {
                    Name = category.Name
                };

                return categoryDto;
            }

            return null;
        }

        public async Task UpdateCategory(string id, CategoryDto categoryDto)
        {
            Category category = await cateRepository.FindOneAsync(x => x.Id == id);
            category.Name = categoryDto.Name;

            await cateRepository.UpdateAsync(category);
        }
    }
}
