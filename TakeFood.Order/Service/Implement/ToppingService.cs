using StoreService.Model.Entities.Order;
using StoreService.Model.Entities.Topping;
using StoreService.Model.Repository;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using TakeFood.StoreService.ViewModel.Dtos.Topping;

namespace TakeFood.StoreService.Service.Implement
{
    public class ToppingService : IToppingService
    {
        private readonly IMongoRepository<Topping> _ToppingRepository;

        public ToppingService(IMongoRepository<Topping> toppingRepository)
        {
            this._ToppingRepository = toppingRepository;
        }

        public async Task CreateTopping(string StoreID, CreateToppingDto topping)
        {
            var toppingNew = new Topping()
            {
                Name = topping.Name,
                Price = topping.Price,
                State = "Active",
                StoreID = StoreID,
            };
            await _ToppingRepository.InsertAsync(toppingNew);
        }

        public async Task DeleteTopping(string ID)
        {
            Topping topping = await _ToppingRepository.FindOneAsync(x => x.Id == ID);

            topping.State = "DeActive";
            await _ToppingRepository.UpdateAsync(topping);
        }

        public async Task<List<ToppingViewDto>> GetAllToppingByStoreID(string StoreID, string state)
        {
            List<Topping> toppings = new List<Topping>();
            if(state.Equals("") || state == null) toppings = (List<Topping>)await _ToppingRepository.FindAsync(x => x.StoreID == StoreID);
            else toppings = (List<Topping>)await _ToppingRepository.FindAsync(x => x.StoreID == StoreID && x.State == state);

            List<ToppingViewDto> listTopping = new();
            foreach(Topping topping in toppings)
            {
                ToppingViewDto temp = new ToppingViewDto()
                {
                    Name = topping.Name,
                    Price = topping.Price,
                    ID = topping.Id,
                    State = topping.State
                };
                listTopping.Add(temp);
            }

            return listTopping;
        }

        public async Task<ToppingViewDto> GetToppingByName(string name)
        {
            Topping topping = await _ToppingRepository.FindOneAsync(x => x.Name == name);
            return new ToppingViewDto()
            {
                Name = topping.Name,
                Price = topping.Price,
                ID = topping.Id,
                State=topping.State
            };
        }

        public async Task UpdateTopping(string ID, CreateToppingDto topping)
        {
            Topping toppingOld = await _ToppingRepository.FindOneAsync(x => x.Id == ID);
            toppingOld.Price = topping.Price;
            toppingOld.Name = topping.Name;

            await _ToppingRepository.UpdateAsync(toppingOld);
        }
    }
}
