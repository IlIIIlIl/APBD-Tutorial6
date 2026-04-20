using System.ComponentModel.DataAnnotations;

namespace Tutorial6.DTOs;

public class CreateAnimalDto
{
    //Validation
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
}