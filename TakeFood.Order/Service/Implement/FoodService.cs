using Order.Model.Entities.Category;
using Order.Model.Entities.Food;
using Order.Model.Entities.Topping;
using Order.Model.Repository;
using TakeFood.OrderService.ViewModel.Dtos.Food;

namespace Order.Service.Implement
{
    public class FoodService : IFoodService
    {
        private readonly IMongoRepository<Food> _foodRepository;
        private readonly IMongoRepository<FoodTopping> _foodToppingRepository;
        private readonly IMongoRepository<Category> _categoryRepository;
        private readonly IMongoRepository<Topping> _toppingRepository;

        public FoodService(IMongoRepository<Food> foodRepository, IMongoRepository<FoodTopping> foodToppingRepository
                        , IMongoRepository<Category> categoryRepository, IMongoRepository<Topping> toppingRepository)
        {
            _foodRepository = foodRepository;
            _foodToppingRepository = foodToppingRepository;
            _categoryRepository = categoryRepository;
            _toppingRepository = toppingRepository;
        }

        public async Task CreateFood(string StoreID, CreateFoodDto food)
        {
            Food f = new()
            {
                Name = food.Name,
                Price = food.Price,
                StoreId = StoreID,
                ImgUrl = food.urlImage,
                Description = food.Descript,
                State = food.State
            };
            f.CategoriesID = new List<string>();
            f.CategoriesID.Add(food.CategoriesID);

            Food temp = await _foodRepository.InsertAsync(f);

            foreach (var i in food.ListTopping)
            {
                FoodTopping foodTopping = new FoodTopping()
                {
                    ToppingId = i.ID,
                    FoodId = temp.Id
                };
                await _foodToppingRepository.InsertAsync(foodTopping);
            }
        }

        public async Task DeleteFood(string FoodID)
        {
            Food food = await _foodRepository.FindOneAsync(x => x.Id == FoodID);

            food.State = false;
            await _foodRepository.UpdateAsync(food);
        }

        public async Task<List<FoodView>> GetAllFoodsByCategory(string CategoryID)
        {
            var ListFood = await _foodRepository.FindAsync(x => x.CategoriesID[0] == CategoryID);

            List<FoodView> ListFoodView = new();
            foreach (var i in ListFood)
            {
                FoodView FoodTemp = new()
                {
                    Name = i.Name,
                    Description = i.Description,
                    UrlImage = i.ImgUrl,
                    Price = i.Price,
                    State = i.State == true ? "Còn hàng" : "Hết hàng"
                };

                if (i.CategoriesID.Count > 0)
                {
                    FoodTemp.Category = (await _categoryRepository.FindOneAsync(x => x.Id == i.CategoriesID[0])).Name;
                }

                foreach (var topping in await _foodToppingRepository.FindAsync(x => x.FoodId == i.Id))
                {
                    FoodTemp.ListTopping.Add((await _toppingRepository.FindOneAsync(x => x.Id == topping.ToppingId)).Name);
                }
                ListFoodView.Add(FoodTemp);
            }

            return ListFoodView;
        }

        public async Task<List<FoodView>> GetAllFoodsByStoreID(string StoreID)
        {
            var listFood = await _foodRepository.FindAsync(x => x.StoreId == StoreID);

            List<FoodView> listFoodView = new();

            foreach (Food food in listFood)
            {
                FoodView FoodTemp = new()
                {
                    Name = food.Name,
                    Description = food.Description,
                    UrlImage = food.ImgUrl,
                    Price = food.Price,
                    State = food.State == true ? "Còn hàng" : "Hết hàng",
                    FoodId = food.Id
                };

                if (food.CategoriesID.Count > 0)
                {
                    FoodTemp.Category = (await _categoryRepository.FindOneAsync(x => x.Id == food.CategoriesID[0])).Name;
                }

                foreach (var i in await _foodToppingRepository.FindAsync(x => x.FoodId == food.Id))
                {
                    FoodTemp.ListTopping.Add((await _toppingRepository.FindOneAsync(x => x.Id == i.ToppingId)).Name);
                }
                listFoodView.Add(FoodTemp);
            }

            return listFoodView;
        }

        public async Task UpdateFood(string FoodID, CreateFoodDto foodUpdate)
        {
            Food food = await _foodRepository.FindOneAsync(x => x.Id == FoodID);
            food.Name = foodUpdate.Name;
            food.Price = foodUpdate.Price;
            food.Description = foodUpdate.Descript;
            food.ImgUrl = foodUpdate.urlImage;
            food.CategoriesID[0] = foodUpdate.CategoriesID;
            await _foodRepository.UpdateAsync(food);

            await _foodToppingRepository.RemoveManyAsync(x => x.FoodId == FoodID);

            foreach (var i in foodUpdate.ListTopping)
            {
                FoodTopping foodTopping = new FoodTopping()
                {
                    ToppingId = i.ID,
                    FoodId = FoodID
                };
                await _foodToppingRepository.InsertAsync(foodTopping);
            }
        }
    }
}
