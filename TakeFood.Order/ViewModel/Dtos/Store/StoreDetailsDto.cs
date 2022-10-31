namespace TakeFood.StoreService.ViewModel.Dtos.Store;

public class StoreDetailsDto
{
    public string StoreId { get; set; }
    public string StoreName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public double Distance { get; set; }
    public double Star { get; set; }
    public int NumOfOrder { get; set; }
    public int NumOfReview { get; set; }
    public string ImgUrl { get; set; }
    public List<FoodCardDto> Foods { get; set; }
}

public class FoodCardDto
{
    public string FoodId { get; set; }
    public string FoodName { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}
