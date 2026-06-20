namespace StoreApp.DTOs.Category;

public class CategoryDto
{
    // ADD THIS PROPERTY TO FIX THE 3 ERRORS
    public int Id { get; set; } 

    public string Name { get; set; } = "";
}

public class CreateCategoryDto
{
    public string Name { get; set; } = "";
}
