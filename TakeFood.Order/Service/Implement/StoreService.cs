using Order.Model.Entities.Address;
using Order.Model.Entities.Review;
using Order.Model.Entities.Store;
using Order.Model.Repository;
using System.Text.Json;
using TakeFood.OrderService.Service;
using TakeFood.OrderService.Utilities.Helper;
using TakeFood.OrderService.ViewModel.Dtos.Food;
using TakeFood.OrderService.ViewModel.Dtos.Image;
using TakeFood.OrderService.ViewModel.Dtos.Store;

namespace Order.Service.Implement;

public class StoreService : IStoreService
{
    private readonly IMongoRepository<Store> storeRepository;
    private readonly IMongoRepository<Address> addressRepository;
    private readonly IImageService imageService;
    private readonly IFoodService foodService;
    private readonly IMongoRepository<StoreCategory> storeCateRepository;
    private readonly IMongoRepository<Review> reviewRepository;
    private readonly IMongoRepository<Order.Model.Entities.Order.Order> orderRepository;
    public StoreService(IMongoRepository<Store> storeRepository, IMongoRepository<Address> addressRepository
        , IImageService imageService, IMongoRepository<StoreCategory> storeCateRepository, IFoodService foodService
        , IMongoRepository<Review> reviewRepository, IMongoRepository<Order.Model.Entities.Order.Order> orderRepository)
    {
        this.storeRepository = storeRepository;
        this.addressRepository = addressRepository;
        this.imageService = imageService;
        this.storeCateRepository = storeCateRepository;
        this.foodService = foodService;
        this.reviewRepository = reviewRepository;
        this.orderRepository = orderRepository;
    }

    public async Task CreateStore(string ownerID, CreateStoreDto store)
    {
        Address address = new Address()
        {
            Information = "Tỉnh/TP: " + store.StoreAddress.province + ", Quân/Huyện: " + store.StoreAddress.district + ", Xã/Phường: " + store.StoreAddress.town,
        };
        Store _store = new Store()
        {
            Name = store.StoreName,
            AddressId = (await addressRepository.InsertAsync(address)).Id,
            PhoneNumber = store.StorePhone,
            State = "Active",
            OwnerId = ownerID,
            SumStar = 0,
            NumReiview = 0,
            TaxId = store.TaxID,
        };

        foreach (var category in store.Categories)
        {
            StoreCategory storeCategory = new StoreCategory()
            {
                CategoryId = category.CategoryId,
                StoreId = _store.Id
            };
            await storeCateRepository.InsertAsync(storeCategory);
        }

        await storeRepository.InsertAsync(_store);
        await InsertImage(_store.Id, "6354d739d64447e2509cb9fb", store.urlStoreImage);
        await InsertImage(_store.Id, "6354d7e9d64447e2509cb9fc", store.urlKitchenImage);
        await InsertImage(_store.Id, "6354d802d64447e2509cb9fd", store.urlMenuImage);
        await InsertImage(_store.Id, "6354d80dd64447e2509cb9fe", store.urlFontCmndImage);
        await InsertImage(_store.Id, "6354d818d64447e2509cb9ff", store.urlBackCmndImage);
        await InsertImage(_store.Id, "6354d82cd64447e2509cba00", store.urlLicenseImage);
    }

    public List<Store> getAllStores()
    {
        return storeRepository.GetAll().ToList();
    }

    public async Task InertCrawlData()
    {
        List<Root> items;
        using (StreamReader r = new StreamReader("store.json"))
        {
            string json = r.ReadToEnd();
            items = JsonSerializer.Deserialize<List<Root>>(json)!;
        }
        foreach (var i in items)
        {
            var address = new Address()
            {
                Information = "From foody",
                Addrress = i.address,
                AddressType = "Store",
                Lat = i.lat,
                Lng = i.lng
            };

            await addressRepository.InsertAsync(address);
            var store = new Store()
            {
                Name = i.name,
                PhoneNumber = i.phoneNumber,
                State = "Active",
                SumStar = 0,
                NumReiview = 0,
                OwnerId = i.id.ToString(),
                TaxId = "0102859048",
                AddressId = address.Id,

            };
            await storeRepository.InsertAsync(store);
            foreach (var img in i.img)
            {
                await imageService.CreateImage(store.Id, "Store", new ImageDto()
                {
                    Url = img.value,
                });
            }

        }
        return;
    }
    public async Task InertMenuCrawlDataAsync()
    {
        List<Root2> items;
        using (StreamReader r = new StreamReader("menu.json"))
        {
            string json = r.ReadToEnd();
            items = JsonSerializer.Deserialize<List<Root2>>(json)!;

            foreach (var i in items)
            {
                var store = await storeRepository.FindOneAsync(x => x.OwnerId == i.id.ToString());

                await foodService.CreateFood(store.Id, new CreateFoodDto()
                {
                    Name = i.name,
                    Descript = i.description,
                    urlImage = i.photo,
                    State = true,
                    Price = i.price,
                    CategoriesID = "",
                    ListTopping = new List<ToppingCreateFoodDto>()
                }); ;

            }
            return;
        }
    }

    private async Task InsertImage(string storeID, string categoryID, string url)
    {
        ImageDto image = new ImageDto()
        {
            Url = url,
        };
        await imageService.CreateImage(storeID, categoryID, image);
    }

    public async Task<List<CardStoreDto>> GetStoreNearByAsync(GetStoreNearByDto getStoreNearByDto)
    {
        var box = MapCoordinates.GetBoundingBox(new MapPoint()
        {
            Latitude = getStoreNearByDto.Lat,
            Longitude = getStoreNearByDto.Lng
        }, getStoreNearByDto.RadiusOut);
        IList<Address> storeAddress;
        IEnumerable<string> ids;
        storeAddress = await addressRepository.FindAsync(x => x.AddressType == "Store" && box.MinPoint.Latitude <= x.Lat && x.Lat <= box.MaxPoint.Latitude && box.MinPoint.Longitude <= x.Lng && x.Lng <= box.MaxPoint.Longitude);
        if (getStoreNearByDto.RadiusIn != 0)
        {
            var inBox = MapCoordinates.GetBoundingBox(new MapPoint()
            {
                Latitude = getStoreNearByDto.Lat,
                Longitude = getStoreNearByDto.Lng
            }, getStoreNearByDto.RadiusIn);

            storeAddress = storeAddress.Where(x => !(inBox.MinPoint.Latitude <= x.Lat && x.Lat <= inBox.MaxPoint.Latitude && inBox.MinPoint.Longitude <= x.Lng && x.Lng <= inBox.MaxPoint.Longitude)).ToList();
        }
        ids = storeAddress.Select(y => y.Id);
        storeAddress = null;
        var stores = await storeRepository.FindAsync(x => ids.Contains(x.AddressId));
        ids = null;

        var rs = new List<CardStoreDto>();
        foreach (var store in stores)
        {
            var address = await addressRepository.FindByIdAsync(store.AddressId);
            double d = new Coordinates(getStoreNearByDto.Lat, getStoreNearByDto.Lng)
                            .DistanceTo(
                                new Coordinates(address.Lat, address.Lng),
                                UnitOfLength.Kilometers
                            );
            rs.Add(new CardStoreDto()
            {
                StoreName = store.Name,
                Star = store.SumStar / (store.NumReiview == 0 ? 1 : store.NumReiview),
                StoreId = store.Id,
                Address = address.Addrress,
                Distance = (double.IsNaN(d) || double.IsInfinity(d) ? 0 : d),
                NumOfReView = store.NumReiview,
                Image = await imageService.GetStoreSlug(storeId: store.Id)
            }); ;
        }

        return rs.OrderBy(x => x.Distance).ToList();
    }

    private async Task<List<CardStoreDto>> GetStoreNearByAsync(GetStoreNearByDto getStoreNearByDto, IList<Address> addresses)
    {
        var box = MapCoordinates.GetBoundingBox(new MapPoint()
        {
            Latitude = getStoreNearByDto.Lat,
            Longitude = getStoreNearByDto.Lng
        }, getStoreNearByDto.RadiusOut);
        IList<Address> storeAddress;
        IEnumerable<string> ids;
        storeAddress = addresses.Where(x => box.MinPoint.Latitude <= x.Lat && x.Lat <= box.MaxPoint.Latitude && box.MinPoint.Longitude <= x.Lng && x.Lng <= box.MaxPoint.Longitude).ToList();
        if (getStoreNearByDto.RadiusIn != 0)
        {
            var inBox = MapCoordinates.GetBoundingBox(new MapPoint()
            {
                Latitude = getStoreNearByDto.Lat,
                Longitude = getStoreNearByDto.Lng
            }, getStoreNearByDto.RadiusIn);

            storeAddress = storeAddress.Where(x => !(inBox.MinPoint.Latitude <= x.Lat && x.Lat <= inBox.MaxPoint.Latitude && inBox.MinPoint.Longitude <= x.Lng && x.Lng <= inBox.MaxPoint.Longitude)).ToList();
        }
        ids = storeAddress.Select(y => y.Id);
        storeAddress = null;
        var stores = await storeRepository.FindAsync(x => ids.Contains(x.AddressId));
        ids = null;

        var rs = new List<CardStoreDto>();
        foreach (var store in stores)
        {
            var address = await addressRepository.FindByIdAsync(store.AddressId);
            rs.Add(new CardStoreDto()
            {
                StoreName = store.Name,
                Star = store.SumStar / (store.NumReiview == 0 ? 1 : store.NumReiview),
                StoreId = store.Id,
                Address = address.Addrress,
                Distance = new Coordinates(48.672309, 15.695585)
                            .DistanceTo(
                                new Coordinates(48.237867, 16.389477),
                                UnitOfLength.Kilometers
                            ),
                NumOfReView = store.NumReiview,
                Image = await imageService.GetStoreSlug(storeId: store.Id)
            });
        }

        return rs.OrderBy(x => x.Distance).ToList();
    }

    public async Task<List<CardStoreDto>> FilterStoreNearByAsync(FilterStoreByCategoryId filterStoreByCategory)
    {
        var stores = await storeCateRepository.FindAsync(x => filterStoreByCategory.Equals(x.CategoryId));
        var storesId = stores.Select(x => x.StoreId);
        stores = null;
        var addresseId = (await storeRepository.FindAsync(x => storesId.Contains(x.Id))).Select(x => x.AddressId).Distinct().ToList();
        storesId = null;
        return await GetStoreNearByAsync(new GetStoreNearByDto()
        {
            Lat = filterStoreByCategory.Lat,
            Lng = filterStoreByCategory.Lng,
            RadiusIn = filterStoreByCategory.RadiusIn,
            RadiusOut = filterStoreByCategory.RadiusOut
        }, await addressRepository.FindAsync(x => addresseId.Contains(x.Id)));
    }

    public async Task<List<CardStoreDto>> FindStoreByNameAsync(string keyword, double lat, double lng, int start)
    {
        if (start <= 0)
        {
            return new List<CardStoreDto>();
        }
        var list = await GetStoreNearByAsync(new GetStoreNearByDto()
        {
            Lat = lat,
            Lng = lng,
            RadiusIn = 1 * (start - 1),
            RadiusOut = 2 * start
        }, await addressRepository.GetAllAsync());
        return list.FindAll(x => x.StoreName.ToLower().Normalize().Contains(keyword.ToLower().Normalize()));
    }

    public async Task<StoreDetailsDto> GetStoreDetailAsync(string storeId, double lat, double lng)
    {
        var store = await storeRepository.FindOneAsync(x => x.Id == storeId);
        if (store == null)
        {
            throw new Exception("Store's exist!");
        }
        var foods = await foodService.GetAllFoodsByStoreID(storeId);
        var order = await orderRepository.FindAsync(x => x.StoreId == storeId);
        var orderId = order.Select(x => x.Id);
        order = null;
        var numOfOrder = orderId.Count();
        orderId = null;
        var image = await imageService.GetStoreSlug(storeId: storeId);
        var address = await addressRepository.FindOneAsync(x => x.Id == store.AddressId);
        var foodsDetail = new List<FoodCardDto>();
        foreach (var i in foods)
        {
            foodsDetail.Add(new FoodCardDto()
            {
                FoodId = i.FoodId,
                FoodName = i.Name,
                ImageUrl = i.UrlImage,
                Price = i.Price
            });
        }
        var details = new StoreDetailsDto()
        {
            StoreId = storeId,
            StoreName = store.Name,
            Address = address.Addrress,
            PhoneNumber = store.PhoneNumber,
            Star = store.SumStar / (store.NumReiview == 0 ? 1 : store.NumReiview),
            Distance = new Coordinates(48.672309, 15.695585)
                            .DistanceTo(
                                new Coordinates(48.237867, 16.389477),
                                UnitOfLength.Kilometers
                            ),
            NumOfOrder = numOfOrder,
            NumOfReview = store.NumReiview,
            ImgUrl = image,
            Foods = foodsDetail
        };
        return details;
    }

    private class Img
    {
        public int width { get; set; }
        public string value { get; set; }
        public int height { get; set; }
    }

    private class Root
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public List<Img> img { get; set; }
        public List<string> category { get; set; }
    }

    private class Root2
    {
        public double id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string photo { get; set; }
    }
}
