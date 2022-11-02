using TakeFood.OrderService.ViewModel.Dtos.Topping;

namespace TakeFood.OrderService.Service
{
    public interface IToppingService
    {
        Task CreateTopping(string StoreID, CreateToppingDto topping);
        Task UpdateTopping(string ID, CreateToppingDto topping);
        Task<ToppingViewDto> GetToppingByName(string name);
        Task<List<ToppingViewDto>> GetAllToppingByStoreID(string storeID, string state);
        Task DeleteTopping(string ID);
    }
}
