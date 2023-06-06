using Task4.Domain.Commons;
using Task4.Domain.Enums;

namespace Task4.Domain.Entities;
public class User : Auditable
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Role Type { get; set; }
}
