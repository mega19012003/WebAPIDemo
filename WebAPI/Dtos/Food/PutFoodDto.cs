namespace WebAPI.Dtos.Food
{
    public class PutFoodDto
    {
        public Guid FoodId { get; set; }       
        public Guid MenuId { get; set; }         
        public string Name { get; set; }        
        public string Description { get; set; }  
        public double Price { get; set; }       
        public List<IFormFile> Img { get; set; } 
        public int? IndexFile { get; set; }
    }
}
