using System.ComponentModel;

namespace Task4.Service.DTOs;
public class UserForResultDto
{
    [DisplayName("First Name")]
    public string Name { get; set; }
    
    public string Email { get; set; }
}
