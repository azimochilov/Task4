using System.ComponentModel.DataAnnotations;

namespace Task4.Service.DTOs;
public class UserForCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }


    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Please enter valid email")]
    public string Email { get; set; }
}
